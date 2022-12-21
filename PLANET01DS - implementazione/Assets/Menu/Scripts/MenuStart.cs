using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DentedPixel;

public class MenuStart : MonoBehaviour
{
    public string SessionID = "";
    public TextMeshProUGUI _errorMessage;
    public GameObject _audioManager;
    public AudioClip _clickSound;
    public SessionManager _sessionManager;
    public LoadingBar _loadingBar;
    [SerializeField] private Texture2D cursor;

    private AudioSource _audioSource;
    private GameObject[] objs;

    void Start(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        objs = SceneManager.GetSceneByBuildIndex(3).GetRootGameObjects();
        foreach(GameObject obj in objs){
            if(obj.name == "PatchDetails - Canvas"){
                if(gameObject.name == "Canvas") //if the caller is "Canvas" and not "PatchDetails - Canvas"
                    obj.SetActive(false);
            }
        }
    }

    public void StartGame(){
        foreach(GameObject obj in objs){
            if(obj.name == "PatchDetails - Canvas"){
                SessionID = obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>()?.text;
                Debug.Log(SessionID);
            }
        }
        Session ses = _sessionManager.GetSession(SessionID);
        Debug.Log(ses);
        if(ses != null){
            _loadingBar.PlayLoadingBar(4);
            //StartCoroutine(_loadingBar.PlayLoadingBar(4));
            //SceneManager.LoadScene(4);
        }
        else
            Debug.Log("No player Selected");
    }

    public void Back(){
        //PlayClip(_clickSound);  NB: It doesn't work cause the scene gets destroyed
        SceneManager.LoadScene(1);
    }

    public void ClosePatchWindow(){
        PlayClip(_clickSound);
        GameObject[] objs = SceneManager.GetSceneByBuildIndex(3).GetRootGameObjects();
        foreach(GameObject obj in objs){
            if(obj.name == "PatchDetails - Canvas"){
                _errorMessage.gameObject.SetActive(false);
                obj.SetActive(false);
            }
        }
    }

    private void PlayClip(AudioClip clip){
        _audioSource = _audioManager.GetComponent<AudioSource>();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
