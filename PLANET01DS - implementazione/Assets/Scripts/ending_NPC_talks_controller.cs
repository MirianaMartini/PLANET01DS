using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending_NPC_talks_controller : MonoBehaviour
{
    private typewriter speech;
    private bool ready = true;
    private Canvas_controller canvas;
    [SerializeField] private quest_controller_ending quest;
    [SerializeField] private stats_controller stats;

    [SerializeField] private audio_manager soundtrack_mng;
    [SerializeField] private audio_manager dialogue_mng;

    [SerializeField] private AudioSource audioSrc;
    private AudioClip voice;
    private AudioClip glitching_voice;

    private string[] sentences_0 = new string[5] { "Sorry for scaring you, but believe me, that thing. . . was not me!", //0-1
                                                        "Something made those words came out of me, who's Mari? Who was talking through me?", "I was so scared. Since the Big Change I was feeling weird, but now it feels like I'm fixed!", "I'm not scared anymore, I know for sure that everything is gone, I don't know how to explain this feeling. . .", //2-3-4-5
                                                        "Thank you foreigner, thank you my friend. This game is coming back" }; //0-4
                                                        
    private string[] sentences_1 = new string[2] { "Unbelievable, when I saw the king coming out of the castle I couldn't believe my eyes. You really made it!", "You, popped out of nowhere, you changed everything, Another Big Change I would say, but this time in better"}; //0-1

    private string[] sentences_2 = new string[4] { "I'm truly sorry, I know I said I wanted you to go away. . .\nI'm so dumb sometimes", "I hope you will forgive me, I was scared as my friends started glitching, I was so scared. . .", "But I was wrong! You managed to restore our life, our world, and I know I'll be able to behave as I was meant to behave", "This game, thanks to you, is coming back to life" };      //0-1-2-3

    private string[] sentences_3 = new string[6] { "That was weird! Did you see it? I started like dancing and talking, what the hell was I saying?", "Some Alex, some weird stuff! I think someone has played with my scripts, because I never ever heard those sentences", "Now it's fixed, but you know I'm a bit curious. Who's Alex? Nevermind, all I know is that it's not a problem anymore. It's gone!", "My brother is back, my brain is back, I'm ready to work!", //0
                                                        "Thank you, from the bottom of my heart. I will always remember the strange foreigner who came to our land and saved us", "Be safe" };  //0-1-2-3-4-5
                                                        
    private string[] sentences_4 = new string[5] { "We are free! Thank you my friend!", "This world is safe, but you are not, I can feel it. . .", "Be safe foreigner, keep going, keep fixing this game, but be cautious", "I hope you understood what I'm talking about. I will not say anything more", "Thank you"}; //0-1-2-3-4

    private string[] sentences_5 = new string[5] { "You came back to celebrate with us, you came back with a victory", "I know, I started glitching, and I started to say some angry words. . .", "I will tell you the truth. During the Big Change I started to feel different, I had like an. . .intention? I had, deep inside me, a new purpose", "That purpose was to help you, and now I can understand why. I had to make you listen to my glitching dialogue", "Now that shadow is gone, I feel free. But I'm sorry, sorry because I could have fought that intention! I could have, but I have been weak" };   // 1-4

    private string[] sentences_6 = new string[2] { "You are back! The king is back!\nAre you ok? Is the king ok? Does someone need me?", "I can't wait to start a new game! When will somebody start a new game?"};    //0-1

    private string[] sentences_7 = new string[4] { "I'm so embarassed, I shouldn't have told you to fuck off, I have been brutal!", "I started living inside that small house and I completely lost the ability to interface with someone else", "But something you did inside the castle completely changed me", "What have you done? Anyway, thank you! You will always be welcome!"}; //0-1-2-3

    private string[] sentences_8 = new string[4] { "You brought my father back, and he said I could came here to thank you", "You are a good person, Sir, and you are not a stranger anymore", "Even if my mother is not back, my life can start again with my father, the king, and it's all thank to you", "Thank you for your kindness, thank you for you bravery, thank you for everything.\nI'll never forget you" };   //0-1-2-3

    private string[] sentences_9 = new string[4] { "Do you recognize me? I'm the crazy one! The one inside his home, the one that was nuts!", "I think you fixed me! Well, As you can see my head is not on my shoulder, I really lost my head!", "But that's another story, another quest, the next real player will surely help me find it!", "Ahahahahah! I'm so happy to start again!"}; //0-1-2-3

    private string[] sentences_10 = new string[3] { "As you can see, the whole village is here to thank you, you brought hope and then you brought freedom", "My people will always be grateful. I will always be grateful", "I hope that nothing will make you be needed here anymore, so I say farewell, foreigner, we'll never forget what you did"}; //0-1-2

    private int[] counter = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] last_sentenceGroup_start = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] last_sentenceGroup_end = { 4, 1, 3, 5, 4, 4, 1, 3, 3, 3, 2 };

    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.parent.GetComponent<Canvas_controller>();
        speech = transform.GetChild(transform.childCount - 1).GetComponent<typewriter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (speech.isReady && speech.finished_talking)
        {
            ready = true;
            speech.finished_talking = true;
            audioSrc.Stop();
        }
        else if (ready)
        {
            ready = false;
        }
    }

    public void Next()
    {
        if (counter[ending_interactable.talkingNPC] < last_sentenceGroup_end[ending_interactable.talkingNPC])
        {
            if (ready)
            {
                audioSrc.Play();

                counter[ending_interactable.talkingNPC]++;
                speech.Clean();
                speech.ChangeCounter(counter[ending_interactable.talkingNPC]);
                switch (ending_interactable.talkingNPC)
                {
                    case 0:
                        speech.NewSpeech(sentences_0[counter[ending_interactable.talkingNPC]]);
                        break;
                    case 1:
                        speech.NewSpeech(sentences_1[counter[ending_interactable.talkingNPC]]);
                        break;
                    case 2:
                        speech.NewSpeech(sentences_2[counter[ending_interactable.talkingNPC]]);
                        break;
                    case 3:
                        speech.NewSpeech(sentences_3[counter[ending_interactable.talkingNPC]]);
                        break;
                    case 4:
                        speech.NewSpeech(sentences_4[counter[ending_interactable.talkingNPC]]);
                        break;
                    case 5:
                        speech.NewSpeech(sentences_5[counter[ending_interactable.talkingNPC]]);
                        break;
                    case 6:
                        speech.NewSpeech(sentences_6[counter[ending_interactable.talkingNPC]]);
                        break;
                    case 7:
                        speech.NewSpeech(sentences_7[counter[ending_interactable.talkingNPC]]);
                        break;
                    case 8:
                        speech.NewSpeech(sentences_8[counter[ending_interactable.talkingNPC]]);
                        break;
                    case 9:
                        speech.NewSpeech(sentences_9[counter[ending_interactable.talkingNPC]]);
                        break;
                    case 10:
                        speech.NewSpeech(sentences_10[counter[10]]);
                        break;
                }

            }
            else
            {
                audioSrc.Stop();
                speech.ShowFast();
            }
        }
        else
        {
            if (!ready)
            {
                audioSrc.Stop();
                speech.ShowFast();
            }
            else
            {
                dialogue_mng.ChangeToOther();
                canvas.closeCanvas(4);
                canvas.openCanvas(0);
                ending_interactable.isInteracting = false;
            }
        }
    }

    public void StartTalking(int NPC, AudioClip voiceNPC)
    {
        soundtrack_mng.ChangeToOther();
        voice = voiceNPC;
        audioSrc.clip = voice;
        audioSrc.Play();

        counter[NPC] = last_sentenceGroup_start[NPC];
        speech = transform.GetChild(transform.childCount - 1).GetComponent<typewriter>();

        speech.ChangeCounter(counter[NPC]);
        switch (NPC)
        {
            case 0:
                speech.NewSpeech(sentences_0[counter[NPC]]);
                break;
            case 1:
                speech.NewSpeech(sentences_1[counter[NPC]]);
                break;
            case 2:
                speech.NewSpeech(sentences_2[counter[NPC]]);
                break;
            case 3:
                speech.NewSpeech(sentences_3[counter[NPC]]);
                break;
            case 4:
                speech.NewSpeech(sentences_4[counter[NPC]]);
                break;
            case 5:
                speech.NewSpeech(sentences_5[counter[NPC]]);
                break;
            case 6:
                speech.NewSpeech(sentences_6[counter[ending_interactable.talkingNPC]]);
                break;
            case 7:
                speech.NewSpeech(sentences_7[counter[ending_interactable.talkingNPC]]);
                break;
            case 8:
                speech.NewSpeech(sentences_8[counter[ending_interactable.talkingNPC]]);
                break;
            case 9:
                speech.NewSpeech(sentences_9[counter[ending_interactable.talkingNPC]]);
                break;
            case 10:
                speech.NewSpeech(sentences_10[counter[10]]);
                break;
        }
    }

    public void updateCounters()
    {
        if (counter[0] == 0)
        {
            quest.enableQM(1);
            last_sentenceGroup_start[0] = 1;
            last_sentenceGroup_end[0] = 1;
        }
        else if (counter[0] == 7)
        {
            quest.enableQM(1);
            last_sentenceGroup_start[0] = 8;
            last_sentenceGroup_end[0] = 9;
        }
        else if (counter[6] == 4)
        {
            quest.enableQM(4);
            last_sentenceGroup_start[6] = 5;
            last_sentenceGroup_end[6] = 6;
        }
        else if (counter[5] == 5)
        {
            quest.enableQM(3);
            last_sentenceGroup_start[5] = 6;
            last_sentenceGroup_end[5] = 9;
        }
        else if (counter[5] == 9)
        {
            //metti ponte buono

            quest.enableQM(3);
            last_sentenceGroup_start[5] = 10;
            last_sentenceGroup_end[5] = 12;
        }
    }
}
