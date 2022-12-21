using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class trigger_bridge_controller : MonoBehaviour
{
    public LoadingBar _loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _loadingBar.PlayLoadingBar(5);
            //SceneManager.LoadScene(5);
        }
    }
}
