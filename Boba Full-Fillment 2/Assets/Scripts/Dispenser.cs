using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [SerializeField] GameObject dispensing;
    private List<GameObject> items;

    [SerializeField] Transform spawnPoint;
    [SerializeField] int maxNum;
    private int inTrigger;
    private bool making;

    // Start is called before the first frame update
    void Start()
    {
        items = new List<GameObject>();
        makeItem();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<items.Count; i++)
        {
            if(items[i] == null)
            {
                items.RemoveAt(i);
            }
        }

        // Debug.Log(gameObject.name + " num: " + inTrigger.ToString());

        if (inTrigger == 0 && making == false)
        {
            makeItem();
        }
    }

    private void makeItem()
    {
        making = true;
        int i = items.Count;

        if (i < maxNum && inTrigger == 0)
        {
            items.Add(Instantiate(dispensing, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 90)));
            items[i].transform.parent = transform;
            items[i].transform.position = spawnPoint.position;
        }
        // Vector3 itemPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
    }

    void OnTriggerExit(Collider other)
    {
        if (gameObject.name.Contains("Straw")  && other.gameObject.GetComponent<Straw>() != null)
        {
            inTrigger--;
        }
        else if(gameObject.name.Contains("Cup") && other.gameObject.GetComponent<BobaCup>() != null)
        {
            inTrigger--;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.name.Contains("Straw") && other.gameObject.GetComponent<Straw>() != null)
        {
            inTrigger++;
        }
        else if (gameObject.name.Contains("Cup") && other.gameObject.GetComponent<BobaCup>() != null)
        {
            inTrigger++;
        }
        if (making) making = false;
    }
}