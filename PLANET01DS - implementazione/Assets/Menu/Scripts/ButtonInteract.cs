using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ButtonInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public Sprite Highlighted;
    public Sprite NotHighlighted;
    public AudioClip ClickSound;
    public AudioClip HoverSound;
    

    private SessionManager _sessionManager;
    private RectTransform _button;
    private string _sessionID;
    private GameObject _canvas;
    private AudioSource _audioSource;
    [SerializeField] private Texture2D cursor;


    void Start(){
        _audioSource = gameObject.GetComponent<AudioSource>();
        _button = gameObject.GetComponent<Button>().GetComponent<RectTransform>();
        GameObject[] objs = SceneManager.GetSceneByBuildIndex(3).GetRootGameObjects();
        foreach(GameObject obj in objs){
            if(obj.name == "SessionManager")
                _sessionManager = obj.GetComponent<SessionManager>();
            if(obj.name == "PatchDetails - Canvas")
                _canvas = obj;
        }
    }

    public void OnPointerEnter(PointerEventData eventData){
        _button.GetComponent<Animator>().Play("OnHoverEnter");
        gameObject.GetComponent<Image>().sprite = Highlighted;
        PlayClip(HoverSound);
    }

    public void OnPointerExit (PointerEventData eventData){
        _button.GetComponent<Animator>().Play("OnHoverExit");
        gameObject.GetComponent<Image>().sprite = NotHighlighted;
    }

    public void ClickPatch(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        PlayClip(ClickSound);
        
        _sessionID = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

        if(gameObject.name == "Account Simon"){
            ShowSimonLog(_canvas);
            _canvas.SetActive(true);
        }
        else {
            Session ses = _sessionManager.GetSession(_sessionID);
            if(ses != null){
                PutInfoInGUI(ses, _canvas);
                _canvas.SetActive(true);
            }
        }
    }

    private void PutInfoInGUI(Session session, GameObject obj){
        obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = session.nameID;
        obj.transform.Find("Info").Find("0").GetComponent<TextMeshProUGUI>().text = session.creationDate;
        obj.transform.Find("Info").Find("1").GetComponent<TextMeshProUGUI>().text = session.lastEdit;
        obj.transform.Find("Info").Find("monologue").gameObject.SetActive(true);
        obj.transform.Find("Info").Find("txt").gameObject.SetActive(true);
        obj.transform.GetChild(3).gameObject.SetActive(true);
    }

    private void ShowSimonLog(GameObject obj){
        obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "S.cs";
        obj.transform.Find("Info").Find("0").GetComponent<TextMeshProUGUI>().text = "?????????????????";
        obj.transform.Find("Info").Find("1").GetComponent<TextMeshProUGUI>().text = "?????????????????";
        obj.transform.Find("Info").Find("monologue").gameObject.SetActive(false);
        obj.transform.Find("Info").Find("txt").gameObject.SetActive(false);
        obj.transform.GetChild(3).gameObject.SetActive(false);
    }

    private void PlayClip(AudioClip clip){
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
