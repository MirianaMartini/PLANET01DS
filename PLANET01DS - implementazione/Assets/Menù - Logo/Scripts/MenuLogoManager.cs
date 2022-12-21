using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogoManager : MonoBehaviour
{
    public GameObject Exit_Confirmation;
    public AudioSource AudioManager;
    public AudioClip ClickSound;
    [SerializeField] private Texture2D cursor;

    private GameObject[] objs;
    
    void Start(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        Exit_Confirmation.SetActive(false);
        objs = SceneManager.GetSceneByBuildIndex(1).GetRootGameObjects();
        foreach(GameObject obj in objs){
            if(obj.name == "CanvasControls"){
                obj.SetActive(false); 
            }
        }
    }   

    public void StartGame(){
        PlayClip(ClickSound); //TODO: fix cause the scene changes
        SceneManager.LoadScene(2);
    }

    public void Options(){
        PlayClip(ClickSound);
        foreach(GameObject obj in objs){
            if(obj.name == "CanvasControls"){
                obj.SetActive(true); 
            }
        }
    }

    public void CloseOptions(){
        PlayClip(ClickSound);
        foreach(GameObject obj in objs){
            if(obj.name == "CanvasControls"){
                obj.SetActive(false); 
            }
        }
    }

    public void Credits(){
        PlayClip(ClickSound);
        foreach(GameObject obj in objs){
            if(obj.name == "CanvasCredits"){
                obj.SetActive(true); 
            }
        }
    }

    public void CloseCredits(){
        PlayClip(ClickSound);
        foreach(GameObject obj in objs){
            if(obj.name == "CanvasCredits"){
                obj.SetActive(false); 
            }
        }
    }

    public void Exit(){
        PlayClip(ClickSound);
        Exit_Confirmation.SetActive(true);
        gameObject.SetActive(false);
    }

    private void PlayClip(AudioClip clip){
        AudioManager.clip = clip;
        AudioManager.Play();
    }

}
