using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    public OrderGenerator orderGenerator;
    public GameObject orderUIPrefab;

    GameObject[] orderElements;
    // Start is called before the first frame update
    void Start()
    {
        float height = -10f;
        orderElements = new GameObject[OrderGenerator.MaxOrderItems];
        for (int i = 0; i < OrderGenerator.MaxOrderItems; i++)
        {
            GameObject uiElement = Instantiate(orderUIPrefab, transform);
            var rectTransform = uiElement.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, height);
            height -= rectTransform.rect.height + 10f;
            orderElements[i] = uiElement;
            uiElement.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var orders = orderGenerator.activeOrders;
        for (int i = 0; i < orderElements.Length; i++)
        {
            var orderUI = orderElements[i];
            var orderUIElem = orderUI.GetComponent<OrderUIElement>();

            if (i < orders.Count)
            {
                Order order = orders[i];
                orderUI.SetActive(true);
                orderUIElem.PopulateUI(order);             
            }
            else 
            {
                orderUI.SetActive(false);
            }
        }
    }
}
