using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_controller_bossfight : MonoBehaviour
{
    [SerializeField] private GameObject qm;
    // Start is called before the first frame update
    void Start()
    {
        qm.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableQM()
    {
        qm.SetActive(false);
    }

    public void enableQM()
    {
        qm.SetActive(true);
    }
}
