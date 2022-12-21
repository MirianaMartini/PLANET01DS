using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class SwordInteractable : MonoBehaviour
{
    [SerializeField] private Canvas_controller canvas;
    [SerializeField] private GameObject player;
    [SerializeField] private BoxCollider king;
    [SerializeField] private NPC_talks_controller NPCs;
    [SerializeField] private quest_controller_bossfight questBf;

    [SerializeField] private GameObject BloccoPistoni1;
    [SerializeField] private GameObject BloccoPistoni2;
    [SerializeField] private GameObject SpuntoniRotanti;

    [SerializeField] private audio_manager bossfight;
    [SerializeField] private AudioClip ending_song;

    private stats_controller stats;
    private StarterAssetsInputs _input;
    //private CharacterController _controller;
    private bool insideTrigger = false;
    private bool _animFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        _input = player.GetComponent<StarterAssetsInputs>();
        stats = player.transform.parent.GetComponent<stats_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (insideTrigger && gameObject.tag == "Interactable")
        {
            GameObject sword = player.GetComponent<ThirdPersonController>().Sword;
            if (_input.sword>0f && !stats.UI_active && sword.activeSelf && _animFlag) // = "Right Mouse" and "Right Trigger" for Gamepad
            {
                _animFlag = false;
                StartCoroutine(InteractWait());
            }  
            if(!sword.activeSelf){
                _animFlag = true;
            }
        }
    }

    public IEnumerator InteractWait()
    {
        yield return new WaitForSeconds(0.5f);
        Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(gameObject);
        if(other.tag == "Player")
        {
            if (gameObject.tag=="Interactable")
                canvas.openCanvas(5);
            insideTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canvas.closeCanvas(5);
            //canvasInteractable.SetActive(false);
            insideTrigger = false;
        }
    }

    private void Interact()
    {
        if (gameObject.name.Contains("mattoni"))
        {
            player.GetComponent<ThirdPersonController>().PlaySound(0); //play interactable sound
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).GetComponent<ParticleSystem>().Play();
            transform.gameObject.GetComponent<BoxCollider>().enabled = false;
            canvas.closeCanvas(5);
            insideTrigger = false;
            stats.bricks++;
            if(stats.bricks == 7)
            {
                NPCs.updateCounters();
            }
        }
        else if (gameObject.name.Contains("HEART"))
        {
            if (GetComponent<shatterable>().hitNum < GetComponent<shatterable>().hitsToShatter - 1)
            {
                player.GetComponent<ThirdPersonController>().PlaySound(0); //play hit sound
                GetComponent<shatterable>().hitNum++;
            }
            else
            {
                player.GetComponent<ThirdPersonController>().PlaySound(0); //play hit sound
                GetComponent<shatterable>().hitNum++;
                player.GetComponent<ThirdPersonController>().PlaySound(1); //play interactable sound
                transform.GetChild(2).GetComponent<pickable>().pickup();
                transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                stats.heartsPickups++;
                if (stats.heartsPickups == 2)
                {
                    NPCs.updateCounters();
                }
                canvas.closeCanvas(5);
                insideTrigger = false;
            }
        }
        else if (gameObject.name.Contains("VALIGIA"))
        {
            if (GetComponent<shatterable>().hitNum < GetComponent<shatterable>().hitsToShatter -1)
            {
                player.GetComponent<ThirdPersonController>().PlaySound(0); //play hit sound
                GetComponent<shatterable>().hitNum++;
            }
            else
            {
                player.GetComponent<ThirdPersonController>().PlaySound(0); //play hit sound
                GetComponent<shatterable>().hitNum++;
                player.GetComponent<ThirdPersonController>().PlaySound(1); //play interactable sound
                transform.GetChild(2).GetComponent<pickable>().pickup();
                transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                NPCs.updateCounters();
                /*if (stats.heartsPickups == 2)
                {
                    NPCs.updateCounters();
                }*/
                canvas.closeCanvas(5);
                insideTrigger = false;
            }
        }
        else if (gameObject.name.Contains("ROCCIA"))
        {
            if (GetComponent<shatterable>().hitNum < GetComponent<shatterable>().hitsToShatter)
            {
                player.GetComponent<ThirdPersonController>().PlaySound(0); //play hit sound
                GetComponent<shatterable>().hitNum++;
            }
            if (GetComponent<shatterable>().hitNum == GetComponent<shatterable>().hitsToShatter)
            {
                player.GetComponent<ThirdPersonController>().PlaySound(1); //play interactable sound
                transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                canvas.closeCanvas(5);
                insideTrigger = false;
            }
        }
        else if (gameObject.name.Contains("c#"))
        {
            stats.bossfight_file++;
            player.GetComponent<ThirdPersonController>().PlaySound(0);
            if (stats.bossfight_file == 3)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(7).gameObject.SetActive(true);
            }
            else if(stats.bossfight_file == 6)
            {
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                transform.GetChild(4).gameObject.SetActive(true);
                transform.GetChild(7).gameObject.SetActive(false);
                transform.GetChild(8).gameObject.SetActive(true);


                transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                king.enabled = true;
                questBf.enableQM();
                canvas.closeCanvas(5);
                insideTrigger = false;
                stats.AwakeKing();

                BloccoPistoni1.GetComponent<Animator>().enabled=false;
                BloccoPistoni2.GetComponent<Animator>().enabled=false;
                SpuntoniRotanti.transform.GetChild(1).GetComponent<RotationSpuntoni>().enabled=false;
                SpuntoniRotanti.transform.GetChild(2).GetComponent<RotationSpuntoni>().enabled=false;
                SpuntoniRotanti.transform.GetChild(3).GetComponent<RotationSpuntoni>().enabled=false;
                SpuntoniRotanti.transform.GetChild(4).GetComponent<RotationSpuntoni>().enabled=false;
                SpuntoniRotanti.transform.GetChild(5).GetComponent<RotationSpuntoni>().enabled=false;
                SpuntoniRotanti.transform.GetChild(6).GetComponent<RotationSpuntoni>().enabled=false;
                SpuntoniRotanti.transform.GetChild(7).GetComponent<RotationSpuntoni>().enabled=false;
                SpuntoniRotanti.transform.GetChild(8).GetComponent<RotationSpuntoni>().enabled=false;
                bossfight.changeSong(ending_song);


            }
        }
    }





}
