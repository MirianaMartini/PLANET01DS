using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AddPatchButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
/*
    public Sprite Highlighted;
    public Sprite NotHighlighted;
*/
    public AddPatchPlayer AddPatchPlayer;
    public SessionManager SessionManager;
    public TextMeshProUGUI ErrorMessageSessions;
    public AudioClip HoverSound;

    private Button _button;
    private string _sessionID;
    private Color _color;
    private AudioSource _audioSource;


    void Awake(){
        _audioSource = gameObject.GetComponent<AudioSource>();
        _button = gameObject.GetComponent<Button>();
        _color = gameObject.GetComponent<Image>().color;
        _color = Color.white;
        _button.onClick.AddListener(AddPatchPlayer.ShowForm);
        _button.onClick.AddListener(CheckSessionsNumber);
        ErrorMessageSessions.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData){
        PlayClip(HoverSound);
        _color.a = 255f;
    }

    public void OnPointerExit (PointerEventData eventData){
        _color.a = 255f/2;
    }

    private void CheckSessionsNumber(){
        if(SessionManager.CountSessions() >= 5)
            StartCoroutine(ShowErrorMessage());
    }

    IEnumerator ShowErrorMessage(){
        ErrorMessageSessions.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        ErrorMessageSessions.gameObject.SetActive(false);
    }

    private void PlayClip(AudioClip clip){
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    
}
