using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderBoxManager : MonoBehaviour
{
    public GameObject[] fulfillmentBoxes;
    OrderGenerator orderGenerator;
    // Start is called before the first frame update
    void Start()
    {
        orderGenerator = FindObjectOfType<OrderGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        var orders = orderGenerator.activeOrders;
        for(int i = 0; i < fulfillmentBoxes.Length; i++)
        {
            var fullfillBox = fulfillmentBoxes[i];


            if (i < orders.Count)
            {
                fullfillBox.SetActive(true);
                fullfillBox.GetComponentInChildren<FulfillmentBox>().SetCurrentOrderIndex(i);
            }
            else
            {
                fullfillBox.SetActive(false);
            }
        }
    }
}
