using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickable : MonoBehaviour
{
    private Vector3 initialScale;
    [SerializeField] private stats_controller stats;
    [SerializeField] private int numHearts;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pickup()
    {
        
        if (tag == "LIFE")
        {
            if(stats.lifeup( numHearts ))
            {
                gameObject.GetComponent<Animator>().SetBool("pickup", true);
            }
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("pickup", true);
        }
    }

}
