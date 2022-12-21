using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    [SerializeField] private GameObject pulsante_prec;
    [SerializeField] private GameObject pulsante_succ;

    private bool flag = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !flag)
        {
            pulsante_prec.GetComponent<StopPonte>().enabled = !pulsante_prec.GetComponent<StopPonte>().enabled;
            pulsante_succ.GetComponent<StopPonte>().enabled = !pulsante_succ.GetComponent<StopPonte>().enabled;
            flag=true;
        }
    }
}
