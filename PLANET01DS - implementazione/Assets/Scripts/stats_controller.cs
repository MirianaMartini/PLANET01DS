using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class stats_controller : MonoBehaviour
{
    public GameObject UI_GameOver;
    [SerializeField] private int initial_life;
    [System.NonSerialized] public int life = 0;
    [System.NonSerialized] public int max_life = 8;
    [System.NonSerialized] public float stamina;
    [System.NonSerialized] public int heartsPickups = 0;
    [System.NonSerialized] public int bricks = 0;
    [System.NonSerialized] public int completed_bricks = 0;
    [System.NonSerialized] public bool UI_active = false;
    [System.NonSerialized] public int bossfight_bridge = 0;
    [System.NonSerialized] public int bossfight_file = 0;
    [SerializeField] private GameObject king;

    [SerializeField] private Collider[] hearts;
    [SerializeField] private Collider[] rocks;
    [SerializeField] private Collider portal;
    [SerializeField] private Collider[] brick;
    [SerializeField] private Collider button;
    public int game_scene = 0;

    // per i giochetti e finire prima le quests
    [SerializeField] private NPC_talks_controller NPCs;
    private int nextQuestCounter = 0;

    [SerializeField] private general_UI_controller UI;
    private int i;
    private int playerStep = 0;       //1 deve prendere i pickups       2 deve rompere le rocce nella grotta    3 deve entrare nel portale      4 deve rompere i mattoni    5 deve usare il pulsante
    // Start is called before the first frame update
   
    void Start()
    {
        lifeup(initial_life);
        UI_GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool lifeup(int hearts)
    { 
        if (life + hearts <= max_life + 1)              //se aggiungendo hearts cuori arrivo fino ad un mezzo cuore pi� del massimo allora posso prendere il lifeup
        {
            if(hearts%2 == 0)                           //se devo aggiungere un numero intero di cuori
            {
                if (life%2 == 0)                         //e ho gi� un numero intero di cuori
                {
                    for (i = (int)(life/2) + 1; i<= (int)((life + hearts)/2); i++)
                    {
                        UI.addHeart(i, 0);
                        UI.deleteHeart(i, 1);
                    }
                }
                else
                {
                    for (i = (int)(life / 2) + 1; i <= (int)((life + hearts) / 2); i++)
                    {
                        UI.addHeart(i, 0);
                        UI.deleteHeart(i, 1);
                    }
                    UI.addHeart((int)((life + hearts) / 2) + 1, 1);
                    UI.deleteHeart((int)((life + hearts) / 2) + 1, 0);
                }

            }
            else
            {
                if (life % 2 == 0)                         //e ho gi� un numero intero di cuori
                {
                    for (i = (int)(life / 2) + 1; i <= (int)((life + hearts) / 2); i++)
                    {
                        UI.addHeart(i, 0);
                        UI.deleteHeart(i, 1);
                    }
                    UI.addHeart((int)((life + hearts) / 2) + 1, 1);
                    UI.deleteHeart((int)((life + hearts) / 2) + 1, 0);

                    for (i = (int)(life / 2) + 1; i <= (int)((life + hearts) / 2); i++)
                    {
                        UI.addHeart(i, 0);
                        UI.deleteHeart(i, 1);
                    }
                }
                else
                {
                    for (i = (int)(life / 2) + 1; i <= (int)((life + hearts) / 2); i++)
                    {
                        UI.addHeart(i, 0);
                        UI.deleteHeart(i, 1);
                    }
                }
            }
            
            life += hearts;
            if (life > max_life) life = max_life;
            return true;
        }
        return false;
    }

    public bool lifeDown(int hearts)
    {
        if (life - hearts > 0)              //se togliendo hearts cuori arrivo fino ad un mezzo cuore allora posso perdere il cuore senza morire
        {
            if (hearts % 2 == 0)                           //se devo togliere un numero intero di cuori
            {
                if (life % 2 == 0)                         //e ho gi� un numero intero di cuori
                {
                    for (i = (int)(life / 2); i >= (int)((life - hearts) / 2) +1 ; i--)
                    {
                        UI.deleteHeart(i, 0);
                    }
                }
                else
                {
                    for (i = (int)(life / 2) + 1; i >= (int)((life - hearts) / 2) + 1; i--)
                    {
                        UI.deleteHeart(i, 0);
                        UI.deleteHeart(i, 1);
                    }
                    UI.addHeart((int)((life - hearts) / 2) + 1, 1);
                    UI.deleteHeart((int)((life - hearts) / 2) + 1, 0);
                }

            }
            else                                            //se devo togliere un numero dispari di cuori
            {
                if (life % 2 == 0)                         //e ho un numero intero di cuori
                {
                    for (i = (int)(life / 2) + 1; i >= (int)((life - hearts) / 2) + 1; i--)
                    {
                        UI.deleteHeart(i, 0);
                        UI.deleteHeart(i, 1);
                    }
                    UI.deleteHeart(i + 1, 0);
                    UI.addHeart(i+1, 1);
                   
                }
                else                                        //e ho un numero dispari di cuori
                {
                    for (i = (int)(life / 2) + 1; i >= (int)((life - hearts) / 2) + 1 ; i--)
                    {
                        UI.deleteHeart(i, 0);
                        UI.deleteHeart(i, 1);
                    }
                    UI.addHeart(i, 1);
                }
            }

            life -= hearts;
            return true;
        }
        else
        {
            UI.deleteHeart(1, 1);
            Debug.Log("die");
            return false;
        }
    }

    public void lifeFull()
    {
        for(i = 1; i<5; i++ )
        {
            UI.addHeart(i, 0);
        }
    }

    public void lifeLower()
    {
        UI.cancelHeartSlot();
    }

    public void setStep(int val)
    {
        playerStep = val;
        if(val == 1)
        {
            for(i = 0; i < hearts.Length; i++)
            {
                hearts[i].enabled = true;
            }
        }
        else if(val == 2)
        {
            for (i = 0; i < rocks.Length; i++)
            {
                rocks[i].enabled = true;
            }
        }
        else if (val == 3)
        {
            portal.enabled = true;
        }
        else if (val == 4)
        {
            for (i = 0; i < brick.Length; i++)
            {
                brick[i].enabled = true;
            }
        }
        else if (val == 5)
        {
            button.enabled = true;
        }

    }

    public void AwakeKing()
    {
        king.GetComponent<Animator>().SetBool("alive", true);
        //king.GetComponent<>
    }

    public void NextQuest()
    {
        if (game_scene == 0)    //gioco
        {
            if (playerStep <= 1)  //finisci la quest 1
            {
                Quest_1_completed();
                transform.GetChild(2).GetComponent<ThirdPersonController>().teleport(new Vector3(-29.3f, 0f, 105.9f));
            }
            else if (playerStep < 4)
            {
                if (nextQuestCounter < 2) //finisci la quest 2
                {
                    Quest_2_completed();
                    transform.GetChild(2).GetComponent<ThirdPersonController>().teleport(new Vector3(3.7f, 0f, 133.7f));
                }
                else //teletrasporto nella cava
                {
                    transform.GetChild(2).GetComponent<ThirdPersonController>().teleport(new Vector3(-41.24f, -7.75f, 2.35f));
                }
            }
            nextQuestCounter++;
        }
        else if (game_scene == 1)   //bossfight
        {
            transform.GetChild(2).GetComponent<ThirdPersonController>().teleport(new Vector3(-40.3f, 2.1f, 7.5f));
        }
    }
    public void Quest_1_completed()
    {
        NPCs.updateCounters();
    }

    public void Quest_2_completed()
    {
        NPCs.Quest_2_Completed();
    }
}
