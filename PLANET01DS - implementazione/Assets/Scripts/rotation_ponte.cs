using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_ponte : MonoBehaviour
{

    public float speed = 20;
    private Quaternion initialRot;
    private Color initialCol;
    [SerializeField] private Renderer pulsante;
    [SerializeField] private GameObject grata;
    [SerializeField] private Color redcol;
    [SerializeField] private Color greencol;
    [SerializeField] private AudioSource success;
    [SerializeField] private AudioSource error;

    private bool stopColCoroutine = false;
    // Start is called before the first frame update
    void Start()
    {
        initialRot = transform.rotation;
        GetComponent<MeshCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.forward, speed * Time.deltaTime);
    }

    public void StartPonte()
    {
        stopColCoroutine = true;
        StartCoroutine(StartPonteRotation());
        
    }

    public bool StopPonte()
    {
        stopColCoroutine = false;
        speed = 0f;
        if(gameObject.transform.localRotation.y < 0.05f && gameObject.transform.localRotation.y > -0.05f)
        {
            transform.rotation = initialRot;
            success.Play();
            StartCoroutine(colorPulsante(Color.green));
            grata.SetActive(false);
            GetComponent<MeshCollider>().enabled = true;
            return true;
        }
        else
        {
            error.Play();
            StartCoroutine(RestartPonte());
            StartCoroutine(colorPulsante(redcol));
        }
        
        return false;
    }

    IEnumerator StartPonteRotation()
    {
        GetComponent<MeshCollider>().enabled = false;
        while (speed < 10)
        {
            speed = speed + 0.5f;
            yield return new WaitForEndOfFrame();
        }
        while (speed < 20)
        {
            speed = speed + 1f;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    IEnumerator colorPulsante(Color col)
    {
        int i = 0;
        initialCol = pulsante.material.color;
        pulsante.material.color = col;

        while (i < 150)
        {
            if (stopColCoroutine)
            {
                pulsante.material.color = initialCol;
                yield break;
            }
            i++;
            yield return new WaitForEndOfFrame();
        }

        pulsante.material.color = initialCol;

        yield return null;
    }

    IEnumerator RestartPonte()
    {
        yield return new WaitForSecondsRealtime(2);
        StartCoroutine(StartPonteRotation());
        yield return null;
    }

}
