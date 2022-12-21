using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ending_mari_talks_controller : MonoBehaviour
{
    private typewriter speech;
    [SerializeField] private stats_controller stats;
    [SerializeField] private quest_controller_ending quest;
    [SerializeField] private ending_NPC_talks_controller NPCs;
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
    [SerializeField] private AudioClip soundtrack_dialogues;
    [SerializeField] private AudioClip soundtrack;
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip voice;
    [SerializeField] private AudioClip glitching_voice;
    //[SerializeField] private typewriter type;


    private string[] sentences = new string[7] {"end patching -planet01",
                                    "save monologue.txt",
                                    "initialize -planet02",
                                    "hearts full",
                                    "hearts --",
                                    "jack -ready",
                                    "cd planet02"};

    private string[] white_sentences = new string[10] {"Wow, you really did it! My hard work is paying off", "You saved this first planet, Jack. Your job here is done", //0-1
                                        "Some of the NPCs had a glitch, I know. I managed to write down what they said.", "I want to understand what is going on, what has happened to my game", "I need to know who's the one behind this mess.",    //2-3-4
                                        "But our job is not finished yet, we have many worlds to save. . . and not much time!", //5
                                        "I will restore all your life, you must be ready for the next world, it's my favourite world.", //6
                                        "",  //7
                                        "I'll be here, when you are ready come to me: we'll go fixing the next planet", //8
                                        "Let's go to Planet 02, the Empire of the great Bug"};    //9
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
                if(counter != 3)
                {
                    audioSrc.clip = voice;
                }
                else
                {
                    audioSrc.clip = glitching_voice;
                }
                audioSrc.Play();
                if ((counter == 0 && white_counter != 1) || (counter == 1 && white_counter != 4))
                {
                    white_counter++;
                    white_text.text = white_sentences[white_counter];
                }
                else
                {
                    if (counter == 2) stats.lifeFull();
                    if (counter == 3) stats.lifeLower();
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
                if (counter <= 5)
                {
                    last_sentenceGroup_start = 6;
                    last_sentenceGroup_end = 6;
                    white_start = 9;
                    quest.enableQM(0);
                }
                else
                {
                    SceneManager.LoadScene(7);
                }
                speech.Clean();
                white_text.text = " ";
                dialogue_mng.ChangeToOther();
                canvas.closeCanvas(2);
                ending_interactable.isInteracting = false;
                canvas.openCanvas(0);
            }

        }
    }

    public void StartTalking()
    {
        soundtrack_mng.ChangeToOther();
        audioSrc.clip = voice;
        audioSrc.Play();
        if (counter == 0 || counter == 5)
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
