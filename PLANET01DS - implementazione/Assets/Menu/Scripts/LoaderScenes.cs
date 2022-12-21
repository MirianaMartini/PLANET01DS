using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScenes : MonoBehaviour
{
    public int videoTime;

    void Update(){
        StartCoroutine(WaitForVideo(videoTime));
    }

    IEnumerator WaitForVideo(int t){
        yield return new WaitForSeconds(t);
        LoadingLevel(1);
    }

    public void LoadingLevel(int sceneID){
        StartCoroutine(LoadAsynchrously(sceneID));
    }

    IEnumerator LoadAsynchrously(int sceneID){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while(!operation.isDone){
            Debug.Log(operation.progress);
            yield return null;
        }
    }

}
