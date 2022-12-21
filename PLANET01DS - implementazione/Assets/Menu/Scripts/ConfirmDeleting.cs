using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ConfirmDeleting : MonoBehaviour
{
    public GameObject ConfirmForm;
    public MenuStart MenuStart;
    public AudioClip ClickSound;
    
    private DeletePatch _deletePatch;
    private Button _no;
    private Button _yes;
    private AudioSource _audioSource;

    void Awake(){
        _deletePatch = gameObject.GetComponent<DeletePatch>();
        _no = ConfirmForm.transform.Find("No").GetComponent<Button>();
        _yes = ConfirmForm.transform.Find("Yes").GetComponent<Button>();
        _no.onClick.AddListener(HideForm);
        _yes.onClick.AddListener(_deletePatch.Delete);
    }

    public void ShowForm(){
        PlayClip(ClickSound);
        ConfirmForm.SetActive(true);
    }

    public void HideForm(){
        PlayClip(ClickSound);
        ConfirmForm.SetActive(false);
    }

    private void PlayClip(AudioClip clip){
        _audioSource = MenuStart.GetComponent<AudioSource>();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}