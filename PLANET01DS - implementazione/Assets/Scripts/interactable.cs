using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StarterAssets;


public class interactable : MonoBehaviour
{
    [SerializeField] private Canvas_controller canvas;
    [SerializeField] private GameObject player;
    [SerializeField] private NPC_talks_controller NPCs;
    [System.NonSerialized] public stats_controller stats;
    //[SerializeField] private StarterAssets.ThirdPersonController player;

    private sign_controller sign;
    private StarterAssetsInputs _input;
    //private CharacterController _controller;
    private bool insideTrigger = false;
    private string[] cartello =
    {
        "QUARRY",
        "CASTLE",
        "VILLAGE",
        "CASTLE",
        "WOODS",
        "VILLAGE",
        "CAVE"
    };

    public static int talkingNPC;
    public static bool isInteracting = false;
    private bool duringCor = false;
    [SerializeField] private AudioClip voiceNPC;
    [SerializeField] private AudioClip voiceNPCglitched;

    // variabili per pulsante ferrovia
    [SerializeField] private Color init_materialColorPulsante;
    [SerializeField] private AudioClip pulsante_verde;
    [SerializeField] private AudioClip pulsante_rosso;
    [SerializeField] private AudioClip pulsante_premuto;


    // Start is called before the first frame update
    void Start()
    {
        _input = player.GetComponent<StarterAssetsInputs>();
        stats = player.transform.parent.GetComponent<stats_controller>();
        //_controller = player.GetComponent<CharacterController>();
        //canvasInteractable = canvas.transform.GetChild(0).gameObject;
        //canvasCartello = canvas.transform.GetChild(1).gameObject;
        //canvasMari = canvas.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    private void Update()
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
            if (gameObject.name.Contains("Cartello"))
            {
                canvas.openCanvas(1);
                canvas.gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = cartello[int.Parse(gameObject.name.Split(' ')[1])];
            }
            //canvasInteractable.SetActive(true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canvas.closeCanvas(0);
            //canvasInteractable.SetActive(false);
            insideTrigger = false;
        
            if (gameObject.name.Contains("NPC"))
            {
                gameObject.GetComponent<Animator>().SetBool("talking", false);
            }
            if (gameObject.name.Contains("Cartello"))
            {
                canvas.closeCanvas(1);
            }
        }
    }

    private void Interact()
    {
        if(gameObject.name.Contains("Cartello"))
        {
            canvas.closeCanvas(0);
            sign = gameObject.GetComponent<sign_controller>();
            if ( sign.path_showing )
            {
                sign.stopShow();
            }
            else
            {
                sign.show();
            }
            isInteracting = false;
        }
        else if (gameObject.name == "Scala 1")
        {
            if(SceneManager.GetActiveScene().name == "Ending")
                player.GetComponent<ThirdPersonController>().teleport(new Vector3(60.28f, -9.80f, 97.02f));
            else
                player.GetComponent<ThirdPersonController>().teleport(new Vector3(-41.24f, -7.75f, 2.35f));
            canvas.closeCanvas(0);
            insideTrigger = false;
            isInteracting = false;
        }
        else if (gameObject.name == "Scala 2")
        {
            if(SceneManager.GetActiveScene().name == "Ending")
                player.GetComponent<ThirdPersonController>().teleport(new Vector3(56.32f, -2.27f, 97.90f));
            else
                player.GetComponent<ThirdPersonController>().teleport(new Vector3(-37.77f, 0.2f, 4.102f));
            canvas.closeCanvas(0);
            insideTrigger = false;
            isInteracting = false;
        }
        else if (gameObject.name == "Marisandra")
        {
            canvas.closeCanvas(0);
            canvas.openCanvas(2);
            canvas.gameObject.transform.GetChild(2).GetComponent<Mari_talks_controller>().StartTalking();

        }
        else if(gameObject.name == "portale 1")
        {
            player.GetComponent<ThirdPersonController>().teleport(new Vector3(-1.7f, -94.4f, 46f));
            canvas.closeCanvas(0);
            insideTrigger = false;
            isInteracting = false;
        }
        else if(gameObject.name == "portale 2")
        {
            player.GetComponent<ThirdPersonController>().teleport(new Vector3(-138.2f, 0.2f, 81.87f));
            canvas.closeCanvas(0);
            insideTrigger = false;
            isInteracting = false;
        }
        else if (gameObject.name.Contains("NPC") || gameObject.name.Contains("casa"))
        {
            canvas.closeCanvas(0);
            canvas.openCanvas(4);
            if (gameObject.name.Contains("NPC0"))
            {
                talkingNPC = 0;

            }
            else if (gameObject.name.Contains("NPC1"))
            {
                talkingNPC = 1;
            }
            else if (gameObject.name.Contains("NPC2"))
            {
                talkingNPC = 2;
            }
            else if (gameObject.name.Contains("NPC3"))
            {
                talkingNPC = 3;
            }
            else if (gameObject.name.Contains("NPC4"))
            {
                talkingNPC = 4;
            }
            else if (gameObject.name.Contains("NPC5")){
                talkingNPC = 5;
            }
            else if (gameObject.name.Contains("NPC6"))
            {
                talkingNPC = 6;
            }
            else if (gameObject.name.Contains("casa1"))
            {
                talkingNPC = 7;
            }
            else if (gameObject.name.Contains("casa2"))
            {
                talkingNPC = 8;
            }
            else if (gameObject.name.Contains("casa3"))
            {
                talkingNPC = 9;
            }
            NPCs.StartTalking(talkingNPC, voiceNPC, voiceNPCglitched);
            if(gameObject.name.Contains("NPC")) gameObject.GetComponent<Animator>().SetBool("talking", true);
        }
        else if (gameObject.name.Contains("pulsante"))
        {
            GetComponent<AudioSource>().clip = pulsante_premuto;
            GetComponent<AudioSource>().Play();
            isInteracting = false;
            if (!duringCor)
            {
                StartCoroutine(stopChart());
            }
        }
    }

    IEnumerator stopChart()
    {
        duringCor = true;
        while (transform.GetChild(0).GetComponent<Animator>().speed > 0.1f)
        {
            transform.GetChild(0).GetComponent<Animator>().speed -= 0.1f;
            yield return new WaitForFixedUpdate();
        }
        transform.GetChild(0).GetComponent<Animator>().speed = 0f;
        GetComponent<mattoni_controller>().ChartStopped();
        if (trigger_ferrovia_controller.chartOK)
        {
            GetComponent<Renderer>().material.color = Color.green;
            GetComponent<AudioSource>().clip = pulsante_verde;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
            GetComponent<AudioSource>().clip = pulsante_rosso;
            GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(1f);
        GetComponent<Renderer>().material.color = init_materialColorPulsante;
        StartCoroutine(startChart());
        yield return null;
    }

    IEnumerator startChart()
    {
        while (transform.GetChild(0).GetComponent<Animator>().speed < 1f)
        {
            transform.GetChild(0).GetComponent<Animator>().speed += 0.1f;
            yield return new WaitForFixedUpdate();
        }
        transform.GetChild(0).GetComponent<Animator>().speed = 1f;
        duringCor = false;
        yield return null;
    }

}
