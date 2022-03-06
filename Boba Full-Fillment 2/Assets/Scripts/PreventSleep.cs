using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventSleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.sleepThreshold = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
