using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class DeletePatch : MonoBehaviour
{
    public SessionManager _sessionManager;
    public TextMeshProUGUI _errorMessage;
    public AudioClip ClickSound;
    
    private ConfirmDeleting _confirmDeleting;
    private string _sessionID;
    private GameObject _canvas;
    private AudioSource _audioSource;

    void Start(){
        _confirmDeleting = gameObject.GetComponent<ConfirmDeleting>();
        _confirmDeleting.HideForm();
    }

    public void DeleteButton(){
        PlayClip(ClickSound);
        GameObject[] objs = SceneManager.GetSceneByBuildIndex(3).GetRootGameObjects();
        foreach(GameObject obj in objs){
            if(obj.name == "PatchDetails - Canvas"){
                _canvas = obj;
                _sessionID = obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
                if (_sessionID == "S.cs"){
                    StartCoroutine(ShowErrorMessage());
                    return;
                }
                _confirmDeleting.ShowForm();
            }
        }
    }

    public void Delete(){
        _sessionManager.DeleteSession(_sessionID);
        _sessionManager.ReloadSessions();
        _confirmDeleting.HideForm();
        _canvas.SetActive(false);
    }

    IEnumerator ShowErrorMessage(){
        _errorMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _errorMessage.gameObject.SetActive(false);
    }

    private void PlayClip(AudioClip clip){
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.clip = clip;
        _audioSource.Play();
    }

}
