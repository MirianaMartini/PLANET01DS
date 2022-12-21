using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using UnityEngine.SceneManagement;

public class Bar : MonoBehaviour
{
    public GameObject bar;
    public int time;
    public GameObject message;

    // Start is called before the first frame update
    void Start()
    {
        AnimatedBar();
    }

    public void AnimatedBar()
    {
        LeanTween.scaleX(bar, 1, time).setOnComplete(AnimatedBarEnd);
        message.SetActive(true);
    }

    void AnimatedBarEnd(){
        message.SetActive(false);
        SceneManager.LoadScene(3);
    }
}
