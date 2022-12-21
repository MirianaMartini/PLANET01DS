using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipIntroManager : MonoBehaviour
{
    public void SkipVideo(){
        SceneManager.LoadScene(1);
    }
}
