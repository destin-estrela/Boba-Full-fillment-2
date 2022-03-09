using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public int cashEarned = 0;
    int badReviews = 0;
    public int maxBadReviewsBeforeGameOver = 3;
    public int cashRequiredToWin = 20;
    public GameObject gameOverUI;
    public GameObject victoryUI;
    public TextMeshProUGUI cashEarnedText;
    public TextMeshProUGUI badReviewsText;
    public TextMeshProUGUI cashGoalText;
    OrderGenerator orderGenerator;

    public AudioSource timeRunningOutSound;
    public AudioSource orderFailedSound;
    // Start is called before the first frame update
    void Start()
    {
        orderGenerator = FindObjectOfType<OrderGenerator>();
        cashGoalText.text = $"Goal: ${cashRequiredToWin}";
    }
   
    // Update is called once per frame
    void Update()
    {
        foreach (Order order in orderGenerator.activeOrders)
        {
            if (order.timeRemaining <= 0f)
            {
                orderFailedSound.Play();
                badReviews += 1;
            }
            else if (order.timeRemaining < 9.874f && !timeRunningOutSound.isPlaying)  // length of time running out sound
            {
                timeRunningOutSound.time = (9.874f - order.timeRemaining);
                timeRunningOutSound.Play();
            }
        }
        orderGenerator.activeOrders.RemoveAll(o => o.timeRemaining <= 0f);


        if (badReviews >= maxBadReviewsBeforeGameOver)
        {
            gameOverUI.SetActive(true);
            StartCoroutine(StartOver());
        }
        else if (cashEarned >= cashRequiredToWin)
        {
            victoryUI.SetActive(true);
            StartCoroutine(StartOver());
        }

        cashEarnedText.text = $"Cash Earned: ${cashEarned}";
        badReviewsText.text = $"Bad Reviews: {badReviews}";
    }

    IEnumerator StartOver()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
}
