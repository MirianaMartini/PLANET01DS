using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonHit : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private AudioSource HitSound;
    [SerializeField] private bool makesSound;

    private int i=0;

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag=="pistone"){
            effect.SetActive(true);
            if (makesSound)
            {
                HitSound.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="pistone"){
            effect.SetActive(false);
        }
    }

}
