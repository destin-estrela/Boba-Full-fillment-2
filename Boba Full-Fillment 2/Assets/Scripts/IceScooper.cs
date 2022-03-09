using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceScooper : MonoBehaviour
{
    public GameObject[] iceCubes;
    public int activeIceCubes;
    public GameObject iceCube;
    public Transform iceSpawnPoint;

    public AudioSource scoopingIce;
    public AudioSource dispensingIce;
    // Start is called before the first frame update
    void Start()
    {
        activeIceCubes = 0;
    }

    // Update is called once per frame
    void Update()
    {
       for(int i = 0; i < iceCubes.Length; i++)
        {
            if (i >= iceCubes.Length - activeIceCubes)
            {
                iceCubes[i].SetActive(true);
            }
            else
            {
                iceCubes[i].SetActive(false);
            }
        }
    }

    public void FillScooper()
    {
        if(activeIceCubes < iceCubes.Length)
        {
            scoopingIce.Play();
        }
        activeIceCubes = iceCubes.Length;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "IceRefill")
        {
            FillScooper();
        }
    }

    public void PourIce()
    {
        if(activeIceCubes>=1)
        {
            dispensingIce.Play();
            activeIceCubes -= 1;
            Instantiate(iceCube, iceSpawnPoint.position, transform.rotation);
        }
    }
}
