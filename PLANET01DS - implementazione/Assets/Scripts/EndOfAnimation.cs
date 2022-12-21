using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearMattoniCarrello()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
