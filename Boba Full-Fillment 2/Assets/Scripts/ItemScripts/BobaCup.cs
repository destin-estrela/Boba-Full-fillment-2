using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaCup : MonoBehaviour
{
    public GameObject tea;
    public Animation teaAnimation;
    public GameObject topping;
    public GameObject straw;

    public bool milkPoured = false;
    public bool teaPoured = false;
    public Material milkyTea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        HandleMilk(other);
        HandleTea(other);
    }

    public void HandleMilk(Collider other)
    {
        if (other.tag == "MilkFlow")
        {
            tea.SetActive(true);
            milkPoured = true;
            if (!teaPoured)
            {
                tea.GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
            }
            else
            {
                tea.GetComponent<MeshRenderer>().material = milkyTea;
            }
        }
    }

    public void HandleTea(Collider other)
    {
        if (other.tag == "Tea")
        {
            tea.SetActive(true);
            teaPoured = true;
            if (!milkPoured)
            {
                tea.GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
            }
            else
            {
                tea.GetComponent<MeshRenderer>().material = milkyTea;
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