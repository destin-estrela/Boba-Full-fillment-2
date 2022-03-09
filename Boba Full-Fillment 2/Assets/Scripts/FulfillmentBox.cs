using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FulfillmentBox : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    Order currentOrder;
    public TextMeshProUGUI orderNumberText;
    OrderGenerator orderGenerator;
    int currOrderIndex;
    public AudioSource successSound;
    // Start is called before the first frame update
    void Start()
    {
        successSound = GameObject.Find("SuccessSound").GetComponent<AudioSource>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        orderGenerator = FindObjectOfType<OrderGenerator>();
    }

    public void SetCurrentOrderIndex(int index)
    {
        currOrderIndex = index;
        currentOrder = orderGenerator.activeOrders[index];
        orderNumberText.text = $"O#{currentOrder.orderId}";
    }

    bool DrinkOrderIsCorrect(BobaCup bobaCup, Order order)
    {
        if (order is DrinkOrder)
        {
            DrinkOrder drinkOrder = (DrinkOrder)order;
            return drinkOrder.tea == bobaCup.teaType
                && drinkOrder.topping == bobaCup.toppingType
                && drinkOrder.ice == bobaCup.iceLevel
                && drinkOrder.sweetness == bobaCup.sweetness
                && bobaCup.straw.activeInHierarchy;
        }
        Debug.Log("Tried to give food for a drink order! Shame!");
        return false; 
    }

    void OrderSuccess(GameObject orderGO, Order currentOrder)
    {
        successSound.Play();
        scoreKeeper.cashEarned += currentOrder.value;
        orderGenerator.activeOrders.RemoveAt(currOrderIndex);
        Destroy(orderGO);
        Debug.Log("Order successfully fulfilled!");
    }

    internal void SubmitOrder(BobaCup bobaCup)
    {
       if(DrinkOrderIsCorrect(bobaCup, currentOrder))
        {
            OrderSuccess(bobaCup.gameObject, currentOrder);
        }
       else
        {
            Debug.Log("Incorrect Order!");
        }
    }
}
