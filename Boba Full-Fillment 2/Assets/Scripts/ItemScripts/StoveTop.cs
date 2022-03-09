using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveTop : MonoBehaviour
{
    // Start is called before the first frame update

    public bool on = false;
    public AudioSource stoveSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void buttonClicked()
    {
        on = !on;
        if(on)
        {
            stoveSound.Play();
        }
        else
        {
            stoveSound.Stop();
        }
    }
    //change color when on
}
