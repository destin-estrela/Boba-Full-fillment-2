using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dispenser : MonoBehaviour
{
    [SerializeField] GameObject dispensing;
    private List<GameObject> items;

    [SerializeField] Transform spawnPoint;
    [SerializeField] int maxNum;
    private int inTrigger;
    private bool making;

    private string dispType;
    private Dictionary<string, float> rotY = new Dictionary<string, float>();
    private Dictionary<string, float> rotZ = new Dictionary<string, float>();

    // Start is called before the first frame update
    void Start()
    {
        rotY.Add("Straw", 90);
        rotY.Add("Tapioca", 90);

        rotZ.Add("BobaCup", 90);
        rotZ.Add("Straw", 90);

        string[] types = { "BobaCup", "Straw", "Tapioca", "LJelly", "CJelly", "MJelly", "AJelly", "SJelly"};
        foreach (string type in types)
        {
            if(dispensing.GetComponent(type) != null)
            {
                dispType = type;
            }

            if (!rotY.ContainsKey(type))
            {
                rotY.Add(type, 0);
            }

            if (!rotZ.ContainsKey(type))
            {
                rotZ.Add(type, 0);
            }
        }

        items = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<items.Count; i++)
        {
            if(items[i] == null)
            {
                items.RemoveAt(i);
            }
        }

        if(items.Count == 0)
        {
            inTrigger = 0;
            making = false;
        }

        // Debug.Log(gameObject.name + " num: " + inTrigger.ToString());

        if (inTrigger == 0 && making == false)
        {
            makeItem();
        }
    }

    private void makeItem()
    {
        making = true;
        int i = items.Count;

        if (i < maxNum && inTrigger == 0)
        {
            items.Add(Instantiate(dispensing, new Vector3(0, 0, 0), Quaternion.Euler(0, rotY[dispType], rotZ[dispType])));
            items[i].transform.parent = transform;
            items[i].transform.position = spawnPoint.position;
        }
        // Vector3 itemPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent(dispType) != null)
        {
            inTrigger--;
        }

        if (other.gameObject.GetComponent<BobaCup>() != null)
        {
            other.gameObject.GetComponent<BobaCup>().canTake = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent(dispType) != null)
        {
            inTrigger++;
        }

        if (making) making = false;

        if(other.gameObject.GetComponent<BobaCup>() != null)
        {
            other.gameObject.GetComponent<BobaCup>().canTake = false;
        }
    }
}