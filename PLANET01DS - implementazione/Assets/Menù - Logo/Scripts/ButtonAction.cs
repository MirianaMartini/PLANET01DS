using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonAction : MonoBehaviour
{
    public AudioClip HoverSound;

    private AudioSource _audioSource;

    void Start(){
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
    
    public void Hoveound(PointerEventData eventData){
        Debug.Log("sssss");
        PlayClip(HoverSound);
    }

    private void PlayClip(AudioClip clip){
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
