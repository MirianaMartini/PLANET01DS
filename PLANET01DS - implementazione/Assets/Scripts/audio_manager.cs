using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_manager : MonoBehaviour
{

    private AudioSource src;
    [SerializeField] private audio_manager dialogue;
    private bool stopCor = false;
    private bool corRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        src = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToOther()
    {
        StartCoroutine(ChangeToOtherWithFade());
        
    }

    /*public void ChangeToMe()
    {
        StartCoroutine(ChangeToMeWithFade());
    }*/

    public void StartSong()
    {
        StartCoroutine(RestartWithFade());
    }

    public void changeSong(AudioClip clip)
    {
        
        StartCoroutine(ChangeWithFade(clip));
        
    }



    IEnumerator ChangeWithFade(AudioClip clip)
    {
        if(corRunning == true)
        {
            stopCor = true;
            yield return new WaitForEndOfFrame();
        }
        corRunning = true;
        while(src.volume > 0)
        {
            src.volume = src.volume - 0.01f;
            if (stopCor)
            {
                stopCor = false;
                corRunning = false;
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
        src.clip = clip;
        src.Play();
        while (src.volume < 1)
        {
            src.volume = src.volume + 0.003f;
            yield return new WaitForEndOfFrame();
            if (stopCor)
            {
                stopCor = false;
                corRunning = false;
                yield break;
            }
        }
        corRunning = false;
        yield return null;
    }
    

    IEnumerator ChangeToOtherWithFade()
    {
        if (corRunning == true)
        {
            stopCor = true;
            yield return new WaitForEndOfFrame();
        }
        corRunning = true;
        while (src.volume > 0)
        {
            src.volume = src.volume - 0.01f;
            if (stopCor)
            {
                stopCor = false;
                corRunning = false;
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
        src.Pause();
        corRunning = false;
        dialogue.StartSong();
        yield return null;
    }


    IEnumerator RestartWithFade()
    {
        if (corRunning == true)
        {
            stopCor = true;
            yield return new WaitForEndOfFrame();
        }
        corRunning = true;
        src.volume = 0f;
        src.Play();
        while (src.volume < 1)
        {
            src.volume = src.volume + 0.005f;
            yield return new WaitForEndOfFrame();
            if (stopCor)
            {
                stopCor = false;
                corRunning = false;
                yield break;
            }
        }
        corRunning = false;
        yield return null;
    }
}
