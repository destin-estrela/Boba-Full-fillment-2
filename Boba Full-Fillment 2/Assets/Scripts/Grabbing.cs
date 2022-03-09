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
    public AudioSource grabSound;

    private bool canClickButton = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private IEnumerator ButtonClickCooldown()
    {
        canClickButton = false;
        yield return new WaitForSeconds(1);
        canClickButton = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.fKey.isPressed)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5f, ~(1 << 8)))
            {
                Transform hitObjectTransform = hit.collider.gameObject.transform;
                
                if (hitObjectTransform.tag == "Grabbable" && grabObj == false)
                {
                    hitObj = hit.collider.gameObject;
                    hitObj.GetComponent<Rigidbody>().isKinematic = true;
                    grabObj = true;
                    grabSound.time = .19f;
                    grabSound.Play();
                }
                if(!grabObj && canClickButton && hitObjectTransform.tag == "Clickable")
                {
                    StartCoroutine(ButtonClickCooldown());
                    hitObjectTransform.gameObject.GetComponent<WorldButton>().OnClick();
                }
            }
            if (grabObj && hitObj != null)
            {
                //Moving object with player, 2 units in front of him cause we want to see it.
                hitObj.transform.position = grabPoint.position;
                ActionableObject attribute = hitObj.GetComponent<ActionableObject>();

                hitObj.transform.rotation = attribute == null ?
                    Quaternion.Euler(new Vector3(0, transform.rotation.y, transform.rotation.z))
                    : transform.rotation;
                if(Keyboard.current.spaceKey.wasPressedThisFrame)
                {

                    if(attribute != null)
                    {
                        attribute.UseItem();
                    }
                }
            }
        }
        else
        {
            if(hitObj != null)
            {
                var rigid = hitObj.GetComponent<Rigidbody>();
                if(rigid != null)
                {
                    rigid.isKinematic = false;
                }
            }
            grabObj = false;
        }
    }
}
