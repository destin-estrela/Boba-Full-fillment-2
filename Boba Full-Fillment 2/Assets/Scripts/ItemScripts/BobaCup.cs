using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaCup : MonoBehaviour
{
    public GameObject tea;
    public Animation teaAnimation;
    public GameObject topping;
    public GameObject straw;

    public GameObject[] ice;

    public bool milkPoured = false;
    public bool teaPoured = false;
    public Material milkyTea;

    public string teaType;
    public string toppingType = ""; // empty string denotes no toppingS
    public int sweetness = 0;
    public int iceLevel = 0;
    int maxSweetness = 4;
    public AudioSource pouringSound;
    public AudioSource strawSound;
    public ParticleSystem syrupAddedEffect;
    public ParticleSystem iceAddedEffect;

    public bool canTake = true;
    
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
        HandleSubmission(other);
        HandleIce(other);
        HandleSugar(other);
    }

    public void HandleSugar(Collider other)
    {
        if (other.tag == "Sugar" && sweetness < maxSweetness)
        {
            sweetness += 1;
            syrupAddedEffect.Play();
        }
    }

    public void HandleIce(Collider other)
    {
        if (other.tag == "IceCube" && iceLevel < ice.Length)
        {
            iceAddedEffect.Play();
            ice[iceLevel].SetActive(true);
            iceLevel++;
            Destroy(other.gameObject);
        }
    }

    public void HandleSubmission(Collider other)
    {
        if (other.tag == "Submission")
        {
            other.GetComponent<FulfillmentBox>().SubmitOrder(this);
        }
    }

    public void HandleMilk(Collider other)
    {
        if (other.tag == "MilkFlow")
        {
            pouringSound.Play();
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
            pouringSound.Play();
            tea.SetActive(true);
            teaPoured = true;

            // get the string indicating tea type from the tea flow game object
            teaType = other.GetComponent<DetailedTag>().customTag;
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
        if (collisionObj.tag == "Grabbable" && canTake)
        {
            if (collisionObj.GetComponent<Topping>() != null && !topping.activeInHierarchy) {
                
                toppingType = collision.gameObject.GetComponent<DetailedTag>().customTag;
                TransferMaterialToCup(collisionObj, topping);
                Destroy(collision.gameObject);
            }

            if (collisionObj.GetComponent<Straw>() != null && !straw.activeInHierarchy && tea.activeInHierarchy)
            {
                strawSound.Play();
                TransferMaterialToCup(collisionObj, straw);
                Destroy(collision.gameObject);
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
