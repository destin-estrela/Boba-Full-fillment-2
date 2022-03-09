using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    public float startTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        var audiosource = GetComponent<AudioSource>();
        audiosource.time = startTime;
        audiosource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
