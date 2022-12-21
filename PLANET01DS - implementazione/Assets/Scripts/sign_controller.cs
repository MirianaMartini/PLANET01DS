using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sign_controller : MonoBehaviour
{
    [SerializeField] private GameObject path;
    public bool path_showing = false;
    private int i = 0;
    private int childNum = 0;
    private Color tint;
    private bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        childNum = path.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show()
    {
        stopAllShows();
        path_showing = true;
        StartCoroutine(showPathStep(0));
    }

    public void stopShow()
    {
        if (path_showing)
        {
            path_showing = false;
            stop = true;
        }
    }

    private void stopAllShows()
    {
        for(i=0; i<7; i++)
        {
            if (transform.parent.GetChild(i).GetComponent<sign_controller>().path_showing)
            {
                transform.parent.GetChild(i).GetComponent<sign_controller>().stopShow();
            }
        }
    }

    IEnumerator showPathStep(int child)
    {
        float alpha = 0f;
        tint = path.transform.GetChild(child).GetComponent<Renderer>().material.color;
        
        while (alpha < 1)
        {
            alpha += 0.01f;
            tint.a = alpha;
            path.transform.GetChild(child).GetComponent<Renderer>().material.SetColor("_BaseColor", tint);
            if (stop)
            {
                tint.a = 0f;
                path.transform.GetChild(child).GetComponent<Renderer>().material.SetColor("_BaseColor", tint);
                stop = false;
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
        if (child != childNum - 1)
        {
            StartCoroutine(showPathStep(child + 1));
        }
        else
        {
            StartCoroutine(showPathStep(0));
        }
        while (alpha > 0)
        {
            alpha -= 0.01f;
            tint.a = alpha;
            path.transform.GetChild(child).GetComponent<Renderer>().material.SetColor("_BaseColor", tint);
            if (stop)
            {
                tint.a = 0f;
                path.transform.GetChild(child).GetComponent<Renderer>().material.SetColor("_BaseColor", tint);
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }

    }

    private void showNext(int child)
    {

    }

}
