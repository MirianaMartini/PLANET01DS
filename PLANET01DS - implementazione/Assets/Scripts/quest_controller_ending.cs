using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_controller_ending : MonoBehaviour
{
    [SerializeField] private GameObject[] qm;
    [SerializeField] private Material red;
    [SerializeField] private Color redColor;
    // Start is called before the first frame update
    void Start()
    {
        int i;
        qm[0].SetActive(true);
        //for (i = 0; i < qm.Length; i++)
        //{
        //qm[i].SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void disableQM(int i)
    {
        qm[i].SetActive(false);
    }

    public void enableQM(int i)
    {
        qm[i].SetActive(true);
    }

    public void enableRedQM(int i)
    {
        qm[i].GetComponent<Renderer>().material = red;
        qm[i].transform.GetChild(0).GetChild(1).GetComponent<ParticleSystem>().startColor = redColor;
        qm[i].transform.GetChild(0).GetChild(2).GetComponent<Light>().color = redColor;
        qm[i].SetActive(true);
    }
}