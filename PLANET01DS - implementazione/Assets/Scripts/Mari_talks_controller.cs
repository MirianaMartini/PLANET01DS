using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mari_talks_controller : MonoBehaviour
{
    private typewriter speech;
    [SerializeField] private stats_controller stats;
    [SerializeField] private quests_Controller quest;
    [SerializeField] private NPC_talks_controller NPCs;
    private Text white_text;
    private int counter = 0;
    private int white_counter = 0;
    private bool ready = true;
    private Canvas_controller canvas;
    private int last_sentenceGroup_start = 0;
    private int last_sentenceGroup_end = 5;
    private int white_start = 0;
    [SerializeField] private audio_manager soundtrack_mng;
    [SerializeField] private audio_manager dialogue_mng;
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip voice;
    [SerializeField] private AudioClip glitching_voice;
    //[SerializeField] private typewriter type;


    private string[] sentences = new string[7] {"cd planetoids\nsudo marisander planetoids.exe",
                                    "create patch -n jack\nenable jack",
                                    "interact cmd E\ninteract enable",
                                    "weapons\\sword cmd R_mouse\nweapons\\sword enable",
                                    "cd village\\npcs\nsudo npcs.txt",
                                    "hearts 3",
                                    "start patching"};

    private string[] white_sentences = new string[10] {"Welcome to Planet01ds, I'm Marisander, your guide through this journey.", "I will use my knowledge to give you life, weapons, to program your skills", //0-1
                                        "You are Jack, and I've created you as you are the only one able to help me, to help this game",    //2
                                        "I give you the ability to interact with this broken world: use 'E' key to interact with objects and NPCs", //3
                                        "To survive the threats of this first world, I give you a sword: use the right-mouse button to attack", //4
                                        "The first task I give you is to look for cues. I know you are confused, so am I", "Please, find the village of this rocky world, talk to the villagers, fulfill their quests, restore their memory, play this game as it was meant to be played", "Please, find the reason behind this mess",  //5-6-7
                                        "I give you three lives, I think it is enough. Be cautious, Jack.", //8
                                        "This game needs you, I need you to work. Please please tell me it will work..."};    //9
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.parent.GetComponent<Canvas_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (speech.isReady && speech.finished_talking)
        {
            audioSrc.Stop();
            ready = true;
            white_text.text = white_sentences[white_counter];
            speech.finished_talking = true;
        }
        else if (ready)
        {
            ready = false;
        }
    }

    public void Next()
    {
        //if (white_counter == 7) stats.lifeup(6);
        if (counter < last_sentenceGroup_end)
        {
            if (ready)
            {
                audioSrc.Play();
                if ((counter == 0 && white_counter != 1) || (counter == 4 && white_counter != 7))
                {
                    white_counter++;
                    white_text.text = white_sentences[white_counter];
                }
                else
                {
                    if(counter == 4) stats.lifeup(6);
                    white_counter++;
                    counter++;
                    speech.Clean();
                    white_text.text = " ";
                    speech.ChangeCounter(counter);
                    speech.NewSpeech(sentences[counter]);
                }
            }
            else
            {
                audioSrc.Stop();
                speech.ShowFast();
                white_text.text = white_sentences[white_counter];
            }
        }
        else
        {
            if (!ready)
            {
                audioSrc.Stop();
                speech.ShowFast();
                white_text.text = white_sentences[white_counter];
            }
            else
            {
                if (counter == 5)
                {
                    NPCs.updateCounters();
                    last_sentenceGroup_start = 6;
                    last_sentenceGroup_end = 6;
                    white_start = 9;
                }
                speech.Clean();
                white_text.text = " ";
                dialogue_mng.ChangeToOther();
                //audio_mng.changeSong(soundtrack);
                canvas.closeCanvas(2);
                interactable.isInteracting = false;
                canvas.openCanvas(0);
            }

        }
    }

    public void StartTalking()
    {
        //audio_mng.changeSong(soundtrack_dialogues);
        soundtrack_mng.ChangeToOther();
        audioSrc.clip = voice;
        audioSrc.Play();
        if (counter == 0)
        {
            quest.disableQM(0);
            speech = transform.GetChild(transform.childCount - 1).GetComponent<typewriter>();
            white_text = transform.GetChild(transform.childCount - 2).GetComponent<Text>();
        }
        counter = last_sentenceGroup_start;
        white_counter = white_start;
        speech.ChangeCounter(counter);
        speech.NewSpeech(sentences[counter]);
    }

}
