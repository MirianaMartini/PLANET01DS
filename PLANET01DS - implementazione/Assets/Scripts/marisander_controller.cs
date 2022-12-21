using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class marisander_controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Canvas interact;
    [SerializeField] private Transform UI;
    private GameObject talk;

    private int step = 0;

    void Start()
    {
        talk = UI.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            interact.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                talk.SetActive(true);
                Time.timeScale = 0.0f;
                if (step % 2 == 0)      
                {
                    talk.GetComponent<Mari_talks_controller>().StartTalking(step);

                    step++;
                }
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interact.enabled = false;
        }
    }
}
*/