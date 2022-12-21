using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_ferrovia_controller : MonoBehaviour
{
    public static bool chartOK = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CARRELLO")
        {
            chartOK = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CARRELLO")
        {
            chartOK = false;
        }
    }
}
