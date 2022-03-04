using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : MonoBehaviour
{
    public GameObject milkFlow;
    public Animation pourAnimation;

    public void PourMilkSequence()
    {
        pourAnimation.Play();
    }
}
