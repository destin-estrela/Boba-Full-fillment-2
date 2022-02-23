using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaCup : MonoBehaviour
{
    public GameObject tea;
    public Animation teaAnimation;
    public GameObject topping;
    public GameObject straw;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tea")
        {
            if(!tea.activeInHierarchy)
            {
                tea.GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
                tea.SetActive(true);
                teaAnimation.Play();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObj = collision.gameObject;
        if (collisionObj.tag == "Grabbable")
        {
            if (collisionObj.GetComponent<Topping>() != null && !topping.activeInHierarchy) {
                TransferMaterialToCup(collisionObj, topping);
            }

            if (collisionObj.GetComponent<Straw>() != null && !straw.activeInHierarchy && tea.activeInHierarchy)
            {
                TransferMaterialToCup(collisionObj, straw);
            }
        }
    }

    private void TransferMaterialToCup(GameObject original, GameObject objectOnCup)
    {
        objectOnCup.GetComponent<MeshRenderer>().material = original.GetComponent<MeshRenderer>().material;
        objectOnCup.SetActive(true);
        Destroy(original);
    }
}
