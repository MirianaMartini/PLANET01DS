using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse : MonoBehaviour
{
    private bool insideTrigger = false;

    // Update is called once per frame
    private void Update()
    {
        /*if(insideTrigger)
            transform.position += Vector3.down * 0.006f;*/
    }

    private void OnTriggerEnter(Collider other)
    {
        insideTrigger=true;
        StartCoroutine(Fall());
    }
    private void OnTriggerExit(Collider other)
    {
        insideTrigger = false;
    }

    IEnumerator Fall()
    {
        int i = 0;
        while (i < 60)
        {
            if (i % 2 == 0)
            {
                transform.localPosition = new Vector3(transform.localPosition.x + 0.0005f, transform.localPosition.y + 0.0005f, transform.localPosition.z);
                i++;
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x - 0.0005f, transform.localPosition.y - 0.0005f, transform.localPosition.z);
                i++;
            }
            yield return new WaitForFixedUpdate();
            
        }
        while (transform.localPosition.z> -0.012f && i==60)
        {
            transform.position += Vector3.down * 0.006f;
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

}
