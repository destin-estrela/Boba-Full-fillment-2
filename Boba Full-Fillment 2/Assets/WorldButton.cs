using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour
{

    public UnityEvent unityEvent;
    public Animation buttonClickAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick() 
    {
        if(buttonClickAnimation != null)
        {
            buttonClickAnimation.Play();
        }
        unityEvent.Invoke(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
