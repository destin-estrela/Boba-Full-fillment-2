using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBowl : MonoBehaviour
{
    public GameObject chicken;
    // cooked or not on the chicken script (pass onto chicken bowl then to fulfillment).
    public bool bowlready = false; //have this in the chicken script

    public void OnTriggerEnter(Collider other)
    {
 
        HandleSubmission(other);
     
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObj = collision.gameObject;
        if (collisionObj.tag == "Grabbable")
        {

            if (collisionObj.GetComponent<chicken>() != null && !chicken.activeInHierarchy)
            {
                chicken.SetActive(true);
                Destroy(collision.gameObject);
                if (collisionObj.GetComponent<chicken>().ready == true)
                {
                    bowlready = true;
                }
            }
        }
    }

    public void HandleSubmission(Collider other)
    {
        if (other.tag == "Submission")
        {
            other.GetComponent<FulfillmentBox>().SubmitOrder(this);
        }
    }
}
