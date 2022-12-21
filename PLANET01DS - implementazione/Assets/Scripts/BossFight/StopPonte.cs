using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class StopPonte : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ponte;
    [SerializeField] private AudioSource error;
    [SerializeField] private AudioSource success;
    [SerializeField] private GameObject barre;
    [SerializeField] private GameObject canvas_message;
    [SerializeField] private GameObject canvas_error;
    [System.NonSerialized] public stats_controller stats;

    private StarterAssetsInputs _input;
    public Quaternion InitialRotation;
    private bool insideTrigger = false;
    private bool pressed = false;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        _input = player.GetComponent<StarterAssetsInputs>();
        stats = player.transform.parent.GetComponent<stats_controller>();

        //InitialRotation = ponte.transform.rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        if (insideTrigger && !pressed)
        {
            if (_input.interact)
            { 
                if (!stats.UI_active) // = Input.GetKeyDown(KeyCode.E) for keyboard and "West Button" for Gamepad
                {
                    angle = Quaternion.Angle(ponte.transform.rotation, InitialRotation);
                    if(angle <= 4){ // se è nell'intorno corretto
                        ponte.GetComponent<Rotation>().enabled = !ponte.GetComponent<Rotation>().enabled;
                        ponte.GetComponent<Collider>().enabled = !ponte.GetComponent<Collider>().enabled;
                        ponte.transform.rotation = InitialRotation;
                        GetComponent<Renderer>().material.color = Color.green;
                        barre.SetActive(false);
                        pressed=true;
                        canvas_message.SetActive(false);
                        success.Play();
                    }
                    else{// se non è nell'intorno corretto
                        error.Play();
                        GetComponent<Renderer>().material.color = Color.red;
                        canvas_error.SetActive(true);
                        StartCoroutine(message());
                    }        
                }
                _input.interact = false;
            }
        }
        
        if(!insideTrigger){
            _input.interact = false;
        }

        if(pressed){
            GetComponent<Renderer>().material.color = Color.green;
            canvas_error.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            insideTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !pressed)
        {
            canvas_message.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            insideTrigger = false;
            canvas_message.SetActive(false);
        }
    }

    IEnumerator message()
    {
        yield return new WaitForSeconds(2);
        canvas_error.SetActive(false);
        GetComponent<Renderer>().material.color = Color.white;
    }

}
