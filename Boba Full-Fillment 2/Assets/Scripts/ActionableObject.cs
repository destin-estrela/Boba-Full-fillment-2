using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionableObject : MonoBehaviour
{
    public UnityEvent itemAction; 
  
    public void UseItem()
    {
        itemAction.Invoke();
    }

}
