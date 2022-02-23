using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaStationController : MonoBehaviour
{
    public GameObject teaFlow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DispenseTea()
    {
        teaFlow.SetActive(true);
        Invoke("TurnOffTeaDispenser", 1f);
    }

    void TurnOffTeaDispenser()
    {
        teaFlow.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
