using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddPatchPlayer : MonoBehaviour
{
    
    public TMP_InputField _inputField;
    public TextMeshProUGUI _errorMessage;
    public TextMeshProUGUI _errorMessageSize;
    public GameObject _newPlayerPatch;
    public Transform _content;
    public SessionManager _sessionManager;
    public GameObject _audioManager;
    public AudioClip _clickSound;

    private string PlayerName;
    private AudioSource _audioSource;

    void Awake(){
        gameObject.SetActive(false);
        _errorMessage.gameObject.SetActive(false);
    }
    
    public void ShowForm(){
        PlayClip(_clickSound);
        if(_sessionManager.CountSessions() < 5){
            gameObject.SetActive(true);
            _content.gameObject.SetActive(false);
        }
    }

    public void Cancel(){
        PlayClip(_clickSound);
        _errorMessage.gameObject.SetActive(false);
        _errorMessageSize.gameObject.SetActive(false);
        _inputField.Select();
        _inputField.text = "";
        gameObject.SetActive(false);
        if(!_content.gameObject.activeSelf) _content.gameObject.SetActive(true);
    }

    public void SetName(string s){
        PlayerName = s;
    }

    public void Save(){
        PlayClip(_clickSound);
        _errorMessage.gameObject.SetActive(false);
        _errorMessageSize.gameObject.SetActive(false);

        if(PlayerName == null){
            //_errorMessageSize.gameObject.SetActive(true);
            return;
        }

        if(CheckName()){
            //if session doesn't exist in Session.json
            if(!_sessionManager.ExistSession(PlayerName + ".cs") && PlayerName != "Simon"){
                _content.gameObject.SetActive(true);

                //Add to file
                _sessionManager.AddSession(PlayerName + ".cs");

                CreateItemGUI(PlayerName, true);

                //Hide Form
                Cancel();
            }
            else {
                _inputField.Select();
                _inputField.text = "";

                //Show error message
                //_errorMessage.gameObject.SetActive(true);
            }    
        } 
        else {
            _inputField.Select();
            _inputField.text = "";

            //Show error message
           // _errorMessageSize.gameObject.SetActive(true);
        }
    }

    public void CreateItemGUI(string s, bool b){ //b indicates if I'm creating the item (true), or I'm reading it from the JSON (false)
        //Create an Object of type "Player Patch"
        GameObject obj = Instantiate(_newPlayerPatch);
        
        //Set text
        if(b) obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(s + ".cs");
        else obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(s);

        //Add to hierarchy
        obj.transform.SetParent(_content);
        obj.transform.SetSiblingIndex(1);
    }

    public void DeleteItemsGUI(){
        for (int i = 1; i < _content.childCount-1; i++) {
            Destroy(_content.GetChild(i).gameObject, 0);
        }
    }

    private bool CheckName(){
        while(PlayerName.Contains("  ")){
            PlayerName = PlayerName.Replace("  ", " ");
        }
        while(PlayerName.Contains(".cs")){
            PlayerName = PlayerName.Replace(".cs", "");
        }
        if (PlayerName.EndsWith(" "))
            PlayerName = PlayerName.Remove(PlayerName.Length-1);
        if (PlayerName.StartsWith(" "))
            PlayerName = PlayerName.Remove(0, 1);
        
        if(PlayerName.Length == 0)// || PlayerName.Length > 8) 
            return false;
        return true;
    }

    private void PlayClip(AudioClip clip){
        _audioSource =_audioManager.GetComponent<AudioSource>();
        _audioSource.clip = clip;
        _audioSource.Play();
    }

}
