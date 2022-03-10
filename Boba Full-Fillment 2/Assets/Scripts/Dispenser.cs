using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [SerializeField] GameObject dispensing;
    [SerializeField] int numItems;
    private GameObject[] items;

    [SerializeField] float spacing;
    [SerializeField] Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        items = new GameObject[numItems];

        for(int i=0; i< numItems; i++)
        {
            makeItem(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numItems; i++)
        {
            if(items[i] == null)
            {
                makeItem(i);
            }
        }
    }

    private void makeItem(int i)
    {
        items[i] = Instantiate(dispensing, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 90));
        items[i].transform.parent = transform;

        Vector3 itemPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
        itemPos += new Vector3(dispensing.transform.localScale.x, 0, spacing * i);
        items[i].transform.position = itemPos;
    }
}