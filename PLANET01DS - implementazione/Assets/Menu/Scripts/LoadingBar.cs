using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{  
    private GameObject _background;
    private GameObject _loadingBar;
    private GameObject _text;
    private Slider _slider;

    //Flags
    private bool _play = false;

    void Awake(){
        InitComponents();
    }

    void Start(){
        InitComponents();
    }

    void Update(){
        if(_play){
            if(_slider.value < 0.9f)
                _slider.value += 0.01f;
        }
    }

    // CALL THIS FUNCTION FROM OTHER SCRIPTS
    public void PlayLoadingBar(int sceneIndex){
        _play = true;
        LoadingBarAppears();
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);
        StartCoroutine(LoadingBarProgress(op));
    } 

    public IEnumerator LoadingBarProgress(AsyncOperation op){
        while (!op.isDone){
            if(op.progress >= 0.9f)
                _slider.value = op.progress;
            Debug.Log(op.progress);

            yield return null;
        }
    }

    private void InitComponents(){
        _background = gameObject.transform.GetChild(0).gameObject;
        _loadingBar = gameObject.transform.GetChild(1).gameObject;
        _text = gameObject.transform.GetChild(2).gameObject;
        _background.SetActive(false);
        _loadingBar.SetActive(false);
        _text.SetActive(false);

        _slider = _loadingBar.GetComponent<Slider>();
    }

    private void LoadingBarAppears(){
        _background.SetActive(true);
        _loadingBar.SetActive(true);
        _text.SetActive(true);
        _slider.value = 0.1f;
    }
}
