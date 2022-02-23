using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grabbing : MonoBehaviour
{

    private bool grabObj = false;
    private RaycastHit hit;
    GameObject hitObj = null;
    public Transform grabPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5f, ~(1 << 8)))
            {
                Transform hitObjectTransform = hit.collider.gameObject.transform;
                
                if (hitObjectTransform.tag == "Grabbable" && grabObj == false)
                {
                    hitObj = hit.collider.gameObject;
                    hitObj.GetComponent<Rigidbody>().isKinematic = true;
                    grabObj = true;
                }
                if(hitObjectTransform.tag == "Clickable")
                {
                    hitObjectTransform.gameObject.GetComponent<WorldButton>().OnClick();
                }
            }
            if (grabObj && hitObj != null)
            {
                //Moving object with player, 2 units in front of him cause we want to see it.
                hitObj.transform.position = grabPoint.position;
            }
        }
        else
        {
            if(hitObj != null)
            {
                hitObj.GetComponent<Rigidbody>().isKinematic = false;
            }
            grabObj = false;
        }
    }
}
