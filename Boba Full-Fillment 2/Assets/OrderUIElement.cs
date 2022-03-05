using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderUIElement : MonoBehaviour
{
    public TextMeshProUGUI orderNumberText;
    public TextMeshProUGUI orderDescriptionText;
    public Slider timeLimitSlider;
    float timeRemaining;
    float timeLimit;

    public void PopulateUI(Order order)
    {
        orderNumberText.text = $"O#: {order.orderId}";
        orderDescriptionText.text = order.displayName;
        timeLimit = order.timeLimit;
        timeRemaining = order.timeRemaining;
        timeLimitSlider.value = timeRemaining > 0 ? timeRemaining / timeLimit : 0;
    }
}
