using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chicken : MonoBehaviour
{
    // Start is called before the first frame update
    public Material uncooked;
    public Material cooked;
    public Material burnt;
    public AudioSource cookingSound;

    public bool ready = false;

    //public GameObject tea;

    public float cookTime;
    public float burntTime;
    private float elapsedTime = 0.0f;

    private void OnCollisionStay(Collision collision)
    {
        GameObject collisionObj = collision.gameObject;
        var detailedTag = collisionObj.GetComponent<DetailedTag>();

        if (detailedTag != null && detailedTag.customTag == "Pan")
        {
            var chickenTouch = collisionObj.GetComponent<Pan>().isHot;
            if (chickenTouch == true)
            {
                if(!cookingSound.isPlaying)
                {
                    cookingSound.Play();
                }
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= cookTime)
                {
                    //change color of chicken
                    GetComponent<MeshRenderer>().material = cooked;
                    ready = true;


                }
                if (elapsedTime >= burntTime)
                {
                    GetComponent<MeshRenderer>().material = burnt;
                    ready = false;
                }
            }
            else
            {
                cookingSound.Stop();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        cookingSound.Stop();
    }

    void Start()
    {
        GetComponent<MeshRenderer>().material = uncooked;
        var m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.sleepThreshold = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
