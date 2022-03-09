using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    public bool isHot = false;
 
    private void OnCollisionStay(Collision collision)
    {
        GameObject collisionObj = collision.gameObject;
        if (collisionObj.tag == "Stove")
        {
            var turnedOn = collisionObj.GetComponent<StoveTop>().on;
            if (turnedOn == true)
            {
                Debug.Log("PanTouchingStove");
                isHot = true;
            }
            else
            {
                isHot = false; 
            }
      
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isHot = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
