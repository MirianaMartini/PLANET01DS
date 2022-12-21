using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class typewriter : MonoBehaviour
{
    private string currentText = "";
    private float delay = 0.04f;
    public int counter = 0;
    [System.NonSerialized] public bool isReady = true;
    public bool stop = false;
    public bool finished_talking = false;
    // Start is called before the first frame update
    void Start()
    {
        isReady = true;
    }

    // Update is called once per frame
    public void NewSpeech(string speech)
    {
        isReady = false;
        StartCoroutine(ShowText(speech));
    }

    public void Clean()
    {
        this.GetComponent<Text>().text = "";
        currentText = "";
    }

    IEnumerator ShowText(string speech)
    {
        stop = false;
        isReady = false;
        int current_counter = counter;
        int i = 0;
        for (i = 0; i <= speech.Length; i++)
        {
            currentText = speech.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            if (counter != current_counter )
            {
                stop = false;
                this.GetComponent<Text>().text = speech;
                yield break;
            }
            else if (stop)
            {
                stop = false;
                this.GetComponent<Text>().text = speech;
                yield break;
            }
            yield return new WaitForSecondsRealtime(delay);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        if (counter == current_counter)
        {
            isReady = true;
            finished_talking = true;
        }
        yield return null;
    }
    public void ChangeCounter(int c)
    {
        counter = c;
    }

    public void ShowFast()
    {
        stop = true;
        StartCoroutine(waitABitBeforeReady());
    }

    IEnumerator waitABitBeforeReady()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        isReady = true;
        finished_talking = true;
        yield return null;
    }
}
