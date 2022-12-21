using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_event_c : MonoBehaviour
{
    private AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBeat()
    {
        src.Play();
    }
}
