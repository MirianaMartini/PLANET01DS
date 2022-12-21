using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTextButton : MonoBehaviour
{
    public AudioClip HoverSound;
    public GameObject AudioManager;

    private GameObject text;
    private AudioSource _audioSource;

    void Start(){
        _audioSource = AudioManager.GetComponent<AudioSource>();
        //text = gameObject.transform.GetChild(0).gameObject;
        //text.SetActive(false);
    }

    public void onPointerEnter() {
        PlayClip(HoverSound);
        //text.SetActive(true);
    }

    public void onPointerExit() {
        //text.SetActive(false);
    }

    private void PlayClip(AudioClip clip){
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
