using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class interactable_bossfight : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [System.NonSerialized] public stats_controller stats;
    [SerializeField] private Canvas_controller canvas;
    private StarterAssetsInputs _input;
    [SerializeField] private rotation_ponte ponte1;
    [SerializeField] private rotation_ponte ponte2;
    [SerializeField] private rotation_ponte ponte3;
    [SerializeField] private NPC_talks_controller NPCs;
    [SerializeField] private AudioClip voice;
    private AudioSource door_locked;

    //[SerializeField] private AudioSource final;


    private bool insideTrigger = false;
    public static bool isInteracting = false;
    // Start is called before the first frame update
    void Start()
    {
        _input = player.GetComponent<StarterAssetsInputs>();
        stats = player.transform.parent.GetComponent<stats_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (insideTrigger)
        {
            if (_input.interact)
            {
                if (!stats.UI_active && !isInteracting) // = Input.GetKeyDown(KeyCode.E) for keyboard and "West Button" for Gamepad
                {
                    isInteracting = true;
                    Interact();
                }
                _input.interact = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _input.interact = false;
        if (other.tag == "Player")
        {
            insideTrigger = true;
            canvas.openCanvas(0);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canvas.closeCanvas(0);
            //canvasInteractable.SetActive(false);
            insideTrigger = false;
        }
        if (gameObject.name.Contains("grata"))
        {
            canvas.closeCanvas(7);
            isInteracting = false;
        }
        else if (gameObject.name.Contains("king"))
        {
            gameObject.GetComponent<Animator>().SetBool("talking", false);
        }
    }

    private void Interact()
    {
        if (gameObject.name.Contains("pulsante1") && stats.bossfight_bridge == 0)
        {
            isInteracting = false;
            if (ponte1.speed == 0)
            {
                ponte1.StartPonte();
            }
            else
            {
                if (ponte1.StopPonte())
                {
                    stats.bossfight_bridge++;
                    insideTrigger = false;
                    canvas.closeCanvas(0);
                    gameObject.SetActive(false);
                }
            }
        }
        else if (gameObject.name.Contains("pulsante2") && stats.bossfight_bridge == 1)
        {
            isInteracting = false;
            if (ponte2.speed == 0)
            {
                ponte2.StartPonte();
            }
            else
            {
                if (ponte2.StopPonte())
                {
                    insideTrigger = false;
                    canvas.closeCanvas(0);
                    stats.bossfight_bridge++;
                    gameObject.SetActive(false);
                }
            }
        }
        else if (gameObject.name.Contains("pulsante3") && stats.bossfight_bridge == 2)
        {
            isInteracting = false;
            if (ponte3.speed == 0)
            {
                ponte3.StartPonte();
            }
            else
            {
                if (ponte3.StopPonte())
                {
                    insideTrigger = false;
                    canvas.closeCanvas(0);
                    stats.bossfight_bridge++;
                    gameObject.SetActive(false);
                }
            }
        }
        else if (gameObject.name.Contains("grata"))
        {
            door_locked = GetComponent<AudioSource>();
            door_locked.Play();
            canvas.closeCanvas(0);
            canvas.openCanvas(7);
        }
        else if (gameObject.name.Contains("king"))
        {
            //final.Play();
            interactable.talkingNPC = 10;
            canvas.closeCanvas(0);
            canvas.openCanvas(4);
            NPCs.StartTalking(10, voice, voice);
            
            gameObject.GetComponent<Animator>().SetBool("talking", true);
        }
    }
}
