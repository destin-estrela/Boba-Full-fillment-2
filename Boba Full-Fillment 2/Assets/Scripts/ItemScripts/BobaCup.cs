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

    public string teaType;
    public List<string> toppings;
    // Start is called before the first frame update
    void Start()
    {
        toppings = new List<string>(5);
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

            // get the string indicating tea type from the tea flow game object
            teaType = tea.GetComponent<DetailedTag>().customTag;
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
                
                string toppingType = tea.GetComponent<DetailedTag>().customTag;
                if(toppings.Contains(toppingType))
                {
                    toppings.Add(toppingType);
                }
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
