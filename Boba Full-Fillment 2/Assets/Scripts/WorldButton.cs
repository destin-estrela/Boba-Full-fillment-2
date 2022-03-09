using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour
{

    public UnityEvent unityEvent;
    public Animation buttonClickAnimation;
    public Animator buttonClickAnimator;
    public AudioSource buttonSound;
    // Start is called before the first frame update
    void Start()
    {
        if(buttonClickAnimator)
        {
            buttonClickAnimator.StopPlayback();
        }
    }

    public void OnClick() 
    {
        if(buttonClickAnimation != null)
        {
            buttonSound.Play();
            buttonClickAnimation.Play();
        }
        if(buttonClickAnimator!= null)
        {
            buttonSound.Play();
            buttonClickAnimator.Play("Base Layer.pumping_syrup", 0, 0f);
        }
        unityEvent.Invoke(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
