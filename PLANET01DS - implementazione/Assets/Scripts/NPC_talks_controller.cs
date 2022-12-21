using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC_talks_controller : MonoBehaviour
{
    private typewriter speech;
    private bool ready = true;
    private Canvas_controller canvas;
    [SerializeField] private GameObject NPCquest_0;
    [SerializeField] private GameObject NPCquest_1;
    [SerializeField] private GameObject NPCquest_1_brick;
    [SerializeField] private GameObject NPCquest_2;
    [SerializeField] private quests_Controller quest;
    [SerializeField] private quest_controller_bossfight questBf;
    [SerializeField] private stats_controller stats;
    [SerializeField] private GameObject bridge;
    [SerializeField] private BoxCollider carrello;


    [SerializeField] private audio_manager soundtrack_mng;
    [SerializeField] private audio_manager dialogue_mng;
    [SerializeField] private AudioSource audioSrc;
    private AudioClip voice;
    private AudioClip glitching_voice;

    private string[] sentences_0 = new string[21] { "Oh. . . a foreigner. . .", "Leave me alone...\nNobody cares about me", //0-1
                                                        "Still here?", ". . .", "Since the king got lost, this village has become a den of selfish people. . .", "So focused on themselves. . . when I lost all of my lives, during the Big Change, nobody tried to help", //2-3-4-5
                                                        "There are two life-ups, stuck inside two giant crystals. One is next to the quarry, one is next to the volcano.\nI'm too weak to get up and I do not have a sword to shatter the crystals", "Foreigner, you are my last hope. . .", //6-7
                                                        "Oh! You found the life-ups! You truly are a hero!", "Thank you foreigner, I will spread the good news to the village:\nsomebody has finally come to help us!", //8-9
                                                        "Hi Mari. Long time no see. . .", "How are you? Are you worried? Oh, don’t be!\nI have some words to tell you. Will you listen to me now?", "Since it's impossible to talk to you in person, you forced me to do this. I though 'what does she pay attention to?'\n'This motherfucking game'.", "So why not break it on the release date? I was sure you would have been here trying to patch it.","Let me guess. . .", "10 minutes after the release! Just 10 minutes and you discovered that this game was compromised, just like your career", "Who told you? Was it Alex? Of course it was him.", ". . .", "10 minutes and you are shattered, you and “your” majestic project, all shattered in tiny little pieces! But I have to admit it, I spent a lot of time preparing you this big surprise.", "I hope you and Alex like it.", "So now please, keep patching.\nAnd enjoy the monologue you have avoided listening to for too long." };    //10-11-12-13-14-15-16-17-18-19-19-20
    
    private string[] sentences_1 = new string[9] { "I've already seen you. . .", ". . .", "You. . . you are Him! Please stay away! Please spare me!", //0-1-2
                                                        "Probably I was wrong. . . maybe you are not Him. But I swear, you two look so similar", "I guess He would have never helped somebody, but. . . I still don't trust you",   //3-4
                                                        "Well. If you came here to fix things, fix the bridge to the castle! Prove you are not Him! Go to the quarry and talk to the bricklayer!", //5
                                                        "You did it, huh?\nYou really fixed the bridge", "Now it's even better than before, the bricklayer is an artist isn't it?", "I feel. . . better\nThank you, friend"}; //6-7-8

    private string[] sentences_2 = new string[12] { "Stay away, foreigner.\nThis village is lost",  //0
                                                        "Jake told us you helped him. . .\nbut I don't care, I don't trust you", "I trust me. Just me. Everybody is corrupted but me. After the Big Change some villagers started to act weid, someone got crazy. . .", ". . .someone got lost and never came back", "My advice? Never trust anybody, ANYBODY!",  //1-2-3-4
                                                        "I still think this village is lost, but now I have hope", "You gave me hope", "But have you seen Jake? Have you seen the brother of the bricklayer? What is happening to them?", "This village is just a glitch from the past. We are glitched. I can't trust myself either",     //5-6-7-8
                                                        "Just a glitch. . .\nJust a glitch. . .", "Wait. . . The glitching villagers. . . They all have been helped by you! You are not helping us! What have you done?", "Don't go to the castle! Go away! Leave! I knew I couldn't trust you, you are just like Him" };      //9-10-11
    
    private string[] sentences_3 = new string[21] { "I don't know you. . .", //0
                                                        "Rumors say you helped Jake, that old, grumpy hothead\nYou know, after the Great Change we couldn't stand each other, this world was filled with hate", "I forgot the words 'Thank you' even existed\nBut you restored my memory and gave me hope. I remembered someone I lost, someone I loved", "Foreigner, will you help me find my lost brother?",  //1-2-3
                                                        "For real? Thank you! I feel so miserable, I know this was my mission and for so long I forgot", "My brother, the bricklayer of the village, got lost before the Big Change, he said he was going to explore some cave. . .", "He never came back", "My heart is shattered.\nPlease, look for a cave, use your sword to destroy the obstacles you'll find\nFind my beloved brother!", //4-5-6-7
                                                        "You found him! You found my lost brother!", "Foreigner, I will never thank you enough. I thought I couldn't cry anymore, but my tears of joy say otherwise", "This village is not lost\nYou can save us, I will tell the villagers, you can find the lost king!", "Now that my brother is back you could ask him to help you build the bridge to reach the castle! Go to the quarry!", //8-9-10-11
                                                        "You really did it, huh? <<the first one will be a rocky world>>\nI remember that day, it was in summer 2015.", "You came up with all those weird ideas, like the lava river. I was not so sure about it. It is beautiful indeed.","Guess I have to give you credit for that.","I’m not gonna let you patch everything so easily.", "We both know that you’ll win in the very end, but my goal is not to break this game forever.", "I just want to take time. So you'll SLOWLY patch everything. Are you relieved?", "You shouldn't be. The whole world is playing your broken, creepy videogame, blaming YOU. You have raised expectations so much, and now?","Now you are sinking. As you deserve.\nYou know you deserve this, right?", "Please, keep going. I can't control my joy while I write this stuff. Believe me." }; //12-13-14-15-16-17
    
    private string[] sentences_4 = new string[9] { "Jake hates us. He is always alone", "He doesn't understand that we are all broken", "I wish I could help him, but I can't give him my life. I need it. The Big Change is coming back, I know", "And I must survive, survive again",   //0-1-2-3
                                                        "Wow, you gifted Jake with a full heart.\nYou could have kept it you know? Why give it to someone else?",   //4
                                                        "Why give it to someone else?\n. . .",  //5
                                                        "You also helped the bricklayer, asking nothing back. . .\nWhy do you want to save us? What do you gain?\n. . . Who are you?", //6
                                                        "I don't know your purpose, man. But I know that lady. We all used to worship her like a goddess and she was so kind with us", "She really was kind. Then, the Big Change. . . Now she's the other half of the devil"}; //7-8
    
    private string[] sentences_5 = new string[18] { "Help! Please, help me! I got stuck inside this cave, and my leg is broken!\nPlease, it hurts, help me go back to my village!", "My brother? I thought he didn't care about me anymore... I was wrong!", "Please, good man, find the doctor of the village, he is a very tall guy, I need help with my broken leg!",    //0-1-2
                                                        "You saved my life, my friend, and I want to help you back! I know you want to meet our King. Actually, I'm not sure our Majesty is still in the castle",   //3
                                                        "But it's worth a shot, let's rebuild the old bridge to the castle!", "Please, I'm still recovering, can you hit those rocks with your sword? We need to turn them into bricks!",   //4-5
                                                        "Great job! Now we have to bring them next to the castle. I will teach you something important, can you see that yellow button? It allows you to control the cart", "When you interact with a yellow button something in the environment gets set in motion or gets stopped", "To load the bricks in the cart you have have to stop it when it's on those green tracks!", "Load all our bricks, when you are finished, you'll find me next to the bridge to the castle!", // 6-7-8-9
                                                        "Thank you for your help, the bridge is back! Our world is slowly returning as it was created. And I have a feeling. . .", "I feel that the main problem of this planet is inside this old castle. Please, find the problem and destroy it", "Thank you again, my friend. Come back when you are done, we'll celebrate your victory together", //10-11-12
                                                         "I knew it! I knew you would have continued!\nI knew you would have dug through the system files.", "I knew you would have found me. I’m glad you did it! Please, keep going.", "I’ll let you slowly clean your project and restore it. I’m not a monster, you know that.","This time, my darling, you have my permission: please, destroy all of my work, start from here, form the very first planet.", "Find the corrupted file, Mari." };   // 13-14-15-16-17-18

    private string[] sentences_6 = new string[10] { "Since the Great Change I lost my job: nobody needs a doctor when life is so static!",   //0
                                                        "What? The bricklayer needs me? Of course I want to help! It's the first patient in a long time!", "Oh god. . . I forgot. . . I threw away all my gear, I was so furious after the Great Change. . . Do you think you can help?", //1-2
                                                        "Thank you, foreigner, I was sure I could rely on you!", "Please, find a portal and go through it\nYou have to reach the center of the maze, there you will find my briefcase",   //3-4
                                                        "My briefcase! Oh thank you foreigner, now I can be a real doctor again. I can help my friends!", "The bricklayer will be healed in no time, go tell his brother that everything's fixed!", //5-6
                                                        "The bricklayer couldn't stop thanking me, is this happiness again?",  //7
                                                        "I know you are going to find the King. If he needs a doctor, you know who to call", "Yes fool! ME!\nAre you kidding?"};    //8-9

    private string[] sentences_7 = new string[4] { "Who are you?! Stay away!", 
                                                        "Stop bothering me!", 
                                                        "GO AWAY! GO AWAY!",
                                                        "FUCK OFF FOREIGNER!"};

    private string[] sentences_8 = new string[17] { "Sorry. . . I can't. . .", "I don't want to open the dor, sir. . .", "Can you please go away, sir?", "My mother said I can't talk to anyone but my father", "Who are you? Why don't you go away? Are you a bad person?", //0-1-2-3-4
                                                        "You again? Oh. . .\nI don't want to get in trouble. . .", "If my father comes back you'll be in trouble too!", "Do you know my father?\nWell. . . who doesn't know my father. . .", "If. . . When he comes back you'll be in trouble, please Sir, go away!",  //5-6-7-8
                                                        "You again. . .\nI must tell you, I was hoping you would come back. . .", "I'm always so bored, all alone. . . But I can't have friends. Since my mother left I've been so alone", "I miss her too. . .", //9-10-11
                                                        "I heard you are going to leave soon, Sir. And I also heard that the village is thanking you for your help", "I think you are a good person, but I'm still too young, I have to listen to my mum", "You know she is the queen? Well for me she is. . . Not for the other villagers\nMy father is the king but my mother is not the queen, how can it be possible?", "I've never understood. . .\nThey say she's just a maid, the maid of the king. But she always covers my ears and tells me I'm a princess", "Sir, I know you are going to find my father. Please, find him. I know my mother will never come back, and I don't want to be alone anymore"};   //12-17

    private string[] sentences_9 = new string[9] { "Ahahahah! A visitor?! Again?!", 
                                                        "The last visitor was not so friendly! Ahahahahah!\nNOT AT ALL!!", "Ahahahahah!", "Not friendly at all! Ahahahahah!", //1-2-3
                                                        "I lost my head because of the last visitor! Ahahahahah!", "I don't even know if you are real or if I'm talking alone! Ahahahahah", "I could give you a quest and ask you to find my lost big big head!", "But wait! Oh-oh!\nI think it's still on my shoulders! Ahahahahah!", //4-5-6-7
                                                        "Ahahahahahah!"};
    private string[] sentences_10 = new string[10] { "You . . . you destroyed it! You destroyed the script!\nDo you know what that means?", "When that file was placed inside the very core of this castle everything suddenly changed", "The Big Change, the Great Change", "The villagers started acting weird, we lost our life, this whole game lost its purpose", //0-1-2-3
                                                        "The quests of the NPCs were unrecognizable, I couldn't understand what was going on...\nAnd then, it was my turn", "When I went back to the castle the bridge fell down behind me, destroyed. I guess you managed to restore it!", "The last thing I remember is me watching this file pulsing in front of my throne\nI wanted to hit it. . . I touched it. . . that's all I recall", "But now the file is gone! And I think the villagers' behavior is not corrupted anymore! I think this world is not corrupted anymore!", //0-1-2-3-4-5-6-7-8
                                                        "You saved us, you saved us! Please, come with me, let's go out of this castle, let's celebrate!",
                                                        "Let's go then!"};

    private int[] counter = { 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0};
    private int[] last_sentenceGroup_start = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] last_sentenceGroup_end =   { 0, 2, 0, 0, 3, 2, 0, 0, 4, 0, 8 };

    private string[] missions = new string[13] {"Find two life ups: one is next to the quarry, one is next to the volcano", //false 0
                                                    "Talk to the lone villager",
                                                    "Talk to the villagers to see if they trust you",
                                                    "Find the bricklayer inside a cave", //false 3
                                                    "Find the doctor of the village",
                                                    "Find a magic portal and reach the center of the maze to find the doctor's briefcase",
                                                    "Talk to the doctor",
                                                    "Talk to the bricklayer's brother",
                                                    "Go to the quarry and talk to the bricklayer",
                                                    "Hit the rocks with a sword to turn them into bricks", //false 9
                                                    "Push the yellow button to stop the cart when it's on the green tracks to load the bricks",
                                                    "Talk to the bricklayer next to the castle",
                                                    "Enter the castle"}; //false 12

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
        if (counter[interactable.talkingNPC] < last_sentenceGroup_end[interactable.talkingNPC])
        {
            if (ready)
            {
                audioSrc.Play();

                counter[interactable.talkingNPC]++;
                speech.Clean();
                speech.ChangeCounter(counter[interactable.talkingNPC]);
                switch (interactable.talkingNPC)
                {
                    case 0:
                        speech.NewSpeech(sentences_0[counter[interactable.talkingNPC]]);
                        break;
                    case 1:
                        speech.NewSpeech(sentences_1[counter[interactable.talkingNPC]]);
                        break;
                    case 2:
                        speech.NewSpeech(sentences_2[counter[interactable.talkingNPC]]);
                        break;
                    case 3:
                        speech.NewSpeech(sentences_3[counter[interactable.talkingNPC]]);
                        break;
                    case 4:
                        speech.NewSpeech(sentences_4[counter[interactable.talkingNPC]]);
                        break;
                    case 5:
                        speech.NewSpeech(sentences_5[counter[interactable.talkingNPC]]);
                        break;
                    case 6:
                        speech.NewSpeech(sentences_6[counter[interactable.talkingNPC]]);
                        break;
                    case 7:
                        speech.NewSpeech(sentences_7[counter[interactable.talkingNPC]]);
                        break;
                    case 8:
                        speech.NewSpeech(sentences_8[counter[interactable.talkingNPC]]);
                        break;
                    case 9:
                        speech.NewSpeech(sentences_9[counter[interactable.talkingNPC]]);
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
                if(interactable.talkingNPC == 0)
                {
                    if (counter[0] == 1)
                    {
                        quest.enableQM(1);
                    }
                    else if (counter[0] == 7)
                    {
                        stats.setStep(1);       //i cuori iniziano ad essere interagibili
                        canvas.newMission(missions[0], false);
                        last_sentenceGroup_start[0] = 6;
                        last_sentenceGroup_end[0] = 7;
                    }
                    else if (counter[0] == 9)               //prima missione completata: inizia il glitch e i dialoghi di tutti i personaggi si aggiornano
                    {
                        canvas.newMission(missions[2], true);
                        quest.enableRedQM(1);
                        quest.enableQM(2);

                        NPCquest_0.GetComponent<Animator>().SetBool("glitching", true);
                        last_sentenceGroup_start[0] = 10;
                        last_sentenceGroup_end[0] = 20;
                        last_sentenceGroup_start[1] = 3;
                        last_sentenceGroup_end[1] = 4;
                        last_sentenceGroup_start[2] = 1;
                        last_sentenceGroup_end[2] = 4;
                        last_sentenceGroup_start[3] = 1;
                        last_sentenceGroup_end[3] = 3;
                        last_sentenceGroup_start[4] = 4;
                        last_sentenceGroup_end[4] = 4;
                        last_sentenceGroup_start[7] = 1;
                        last_sentenceGroup_end[7] = 1;
                        last_sentenceGroup_start[8] = 5;
                        last_sentenceGroup_end[8] = 8;
                        last_sentenceGroup_start[9] = 1;
                        last_sentenceGroup_end[9] = 3;
                    }
                }
                else if(interactable.talkingNPC == 3)
                {
                    if(counter[3] == 3)
                    {
                        quest.enableQM(2);
                    }
                    else if(counter[3] == 7)
                    {
                        stats.setStep(2);       //le rocce diventano interagibili
                        canvas.newMission(missions[3], false);
                        quest.disableQM(2);
                        last_sentenceGroup_start[3] = 7;
                        last_sentenceGroup_end[3] = 7;
                        quest.enableQM(3);
                    }
                    else if(counter[3] == 11)               //seconda missione completata: inizia il glitch e i dialoghi si aggiornano
                    {
                        canvas.newMission(missions[8], true);
                        quest.enableRedQM(2);
                        quest.enableQM(3);
                        NPCquest_1.GetComponent<Animator>().SetBool("glitching", true);
                        NPCquest_1_brick.GetComponent<Animator>().SetBool("heal", true);
                        NPCquest_1_brick.transform.localPosition = new Vector3(44.67f, -6.4f, 43.14f);
                        last_sentenceGroup_start[1] = 5;
                        last_sentenceGroup_end[1] = 5;
                        last_sentenceGroup_start[2] = 5;
                        last_sentenceGroup_end[2] = 8;
                        last_sentenceGroup_start[3] = 12;
                        last_sentenceGroup_end[3] = 20;
                        last_sentenceGroup_start[4] = 6;
                        last_sentenceGroup_end[4] = 6;
                        last_sentenceGroup_start[5] = 3;
                        last_sentenceGroup_end[5] = 5;
                        last_sentenceGroup_start[6] = 7;
                        last_sentenceGroup_end[6] = 7;
                        last_sentenceGroup_start[7] = 2;
                        last_sentenceGroup_end[7] = 2;
                        last_sentenceGroup_start[8] = 9;
                        last_sentenceGroup_end[8] = 11;
                        last_sentenceGroup_start[9] = 4;
                        last_sentenceGroup_end[9] = 7;
                    }
                }
                else if (interactable.talkingNPC == 5)
                {
                    if (counter[5] == 2)
                    {
                        canvas.newMission(missions[4], true);
                        last_sentenceGroup_start[5] = 2;
                        last_sentenceGroup_end[5] = 2;
                    }
                    else if(counter[5] == 5)
                    {
                        stats.setStep(4);       //i mattoni diventano interagibili
                        canvas.newMission(missions[9], false);
                        last_sentenceGroup_start[5] = 5;
                        last_sentenceGroup_end[5] = 5;
                    }
                    else if(counter[5] == 9)
                    {
                        carrello.enabled = true;
                        stats.setStep(5);       //il pulsante diventa interagibile
                        canvas.newMission(missions[10], true);
                        last_sentenceGroup_start[5] = 7;
                        last_sentenceGroup_end[5] = 9;
                    }
                    else if(counter[5] == 12)                   //terza missione completata        
                    {
                        canvas.newMission(missions[12], false);
                        carrello.enabled = false;
                        quest.enableRedQM(3);
                        NPCquest_1_brick.GetComponent<Animator>().SetBool("glitching", true);
                        last_sentenceGroup_start[1] = 6;
                        last_sentenceGroup_end[1] = 8;
                        last_sentenceGroup_start[2] = 9;
                        last_sentenceGroup_end[2] = 11;
                        last_sentenceGroup_start[4] = 7;
                        last_sentenceGroup_end[4] = 8;
                        last_sentenceGroup_start[5] = 13;
                        last_sentenceGroup_end[5] = 17;
                        last_sentenceGroup_start[6] = 8;
                        last_sentenceGroup_end[6] = 9;
                        last_sentenceGroup_start[7] = 3;
                        last_sentenceGroup_end[7] = 3;
                        last_sentenceGroup_start[8] = 12;
                        last_sentenceGroup_end[8] = 16;
                        last_sentenceGroup_start[9] = 8;
                        last_sentenceGroup_end[9] = 8;
                    }
                    else if (counter[5] > 12)
                    {
                        bridge.GetComponent<BoxCollider>().isTrigger = true;
                    }
                }
                else if(interactable.talkingNPC == 6)
                {
                    if(counter[6] == 2)
                    {
                        quest.enableQM(4);
                    }
                    else if(counter[6] == 4)
                    {
                        stats.setStep(3);       //i portali diventano interagibili
                        canvas.newMission(missions[5], true);
                        last_sentenceGroup_start[6] = 5;
                        last_sentenceGroup_end[6] = 6;
                    }
                    else if(counter[6] == 6)
                    {
                        canvas.newMission(missions[7], true);
                        last_sentenceGroup_start[6] = 6;
                        last_sentenceGroup_end[6] = 6;
                        quest.enableQM(2);
                        last_sentenceGroup_start[3] = 8;
                        last_sentenceGroup_end[3] = 11;
                    }
                }
                else if(interactable.talkingNPC == 10)
                {
                    if(counter[10] == 8)
                    {
                        questBf.enableQM();
                        interactable_bossfight.isInteracting = false;
                    }
                    else if (counter[10] == 9)
                    {
                        SceneManager.LoadScene(6);
                    }
                }
                dialogue_mng.ChangeToOther();
                canvas.closeCanvas(4);
                canvas.openCanvas(0);
                interactable.isInteracting = false;
            }
        }
    }

    public void StartTalking(int NPC, AudioClip voiceNPC, AudioClip voiceGlitch)
    {
        soundtrack_mng.ChangeToOther();
        voice = voiceNPC;

        if (interactable.talkingNPC == 0)
        {
            if (counter[0] >= 9) voice = voiceGlitch;
            if(counter[0] == 0 || counter[0] == 1 || counter[0] == 7 || counter[0] == 9)
            {
                quest.disableQM(1);
            }
            if (counter[0] == 1)
            {
                last_sentenceGroup_start[0] = 2;
                last_sentenceGroup_end[0] = 7;
            }
            if(counter[0] == 7 && stats.heartsPickups == 2)
            {
                stats.lifeDown(2);
            }
        }
        else if(interactable.talkingNPC == 3)
        {
            if (counter[3] >= 11) voice = voiceGlitch;
            if (counter[3] == 0 || counter[3] == 3 || counter[3] == 7 || counter[3] == 11)
            {
                quest.disableQM(2);
            }
            if (counter[3] == 3)
            {
                last_sentenceGroup_start[3] = 4;
                last_sentenceGroup_end[3] = 7;
            }
            
        }
        else if(interactable.talkingNPC == 4)
        {
            if(counter[4] == 4)
            {
                last_sentenceGroup_start[4] = 5;
                last_sentenceGroup_end[4] = 5;
            }
        }
        else if(interactable.talkingNPC == 5)
        {
            if (counter[5] >= 12) voice = voiceGlitch;
            if(counter[5] == 0 || counter[5] == 2 || counter[5] == 5 || counter[5] == 9 ||  counter[5] == 12)
            {
                quest.disableQM(3);
            }
            if(counter[5] == 0)
            {
                quest.enableQM(4);
                last_sentenceGroup_start[6] = 1;
                last_sentenceGroup_end[6] = 2;
            }
        }
        else if(interactable.talkingNPC == 6)
        {
            if(counter[6] == 0 || counter[6] == 2 || counter[6] == 4)
            {
                quest.disableQM(4);
            }
            if(counter[6] == 2)
            {
                last_sentenceGroup_start[6] = 3;
                last_sentenceGroup_end[6] = 4;
            }
        }
        else if(interactable.talkingNPC == 10)
        {
            questBf.disableQM();
            if(counter[10] == 8)
            {
                last_sentenceGroup_start[10] = 9;
                last_sentenceGroup_end[10] = 9;
            }
        }
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
                speech.NewSpeech(sentences_6[counter[interactable.talkingNPC]]);
                break;
            case 7:
                speech.NewSpeech(sentences_7[counter[interactable.talkingNPC]]);
                break;
            case 8:
                speech.NewSpeech(sentences_8[counter[interactable.talkingNPC]]);
                break;
            case 9:
                speech.NewSpeech(sentences_9[counter[interactable.talkingNPC]]);
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
        else if (counter[0] == 7 )
        {
            canvas.newMission(missions[1], true);
            quest.enableQM(1);
            last_sentenceGroup_start[0] = 8;
            last_sentenceGroup_end[0] = 9;
        }
        else if(counter[6] == 4)
        {
            canvas.newMission(missions[6], true);
            quest.enableQM(4);
            last_sentenceGroup_start[6] = 5;
            last_sentenceGroup_end[6] = 6;
        }
        else if(counter[5] == 5)
        {
            quest.enableQM(3);
            last_sentenceGroup_start[5] = 6;
            last_sentenceGroup_end[5] = 9;
        }
        else if(counter[5] == 9)
        {
            //metti ponte buono
            canvas.newMission(missions[11], true);
            bridge.transform.GetChild(0).gameObject.SetActive(false);
            bridge.transform.GetChild(1).gameObject.SetActive(false);
            bridge.transform.GetChild(2).gameObject.SetActive(true);
            NPCquest_1_brick.transform.localPosition = new Vector3(6.2f, -0.33f, -45f);
            quest.enableQM(3);
            last_sentenceGroup_start[5] = 10;
            last_sentenceGroup_end[5] = 12;
        }
        
        //mainNPC.GetComponent<Animator>().SetBool("glitching", true);
    }

    public void Quest_2_Completed()
    {
        canvas.newMission(missions[8], true);
        quest.enableRedQM(2);
        quest.enableQM(3);
        NPCquest_1.GetComponent<Animator>().SetBool("glitching", true);
        NPCquest_1_brick.GetComponent<Animator>().SetBool("heal", true);
        NPCquest_1_brick.transform.localPosition = new Vector3(44.67f, -6.4f, 43.14f);
        last_sentenceGroup_start[1] = 5;
        last_sentenceGroup_end[1] = 5;
        last_sentenceGroup_start[2] = 5;
        last_sentenceGroup_end[2] = 8;
        last_sentenceGroup_start[3] = 12;
        last_sentenceGroup_end[3] = 20;
        last_sentenceGroup_start[4] = 6;
        last_sentenceGroup_end[4] = 6;
        last_sentenceGroup_start[5] = 3;
        last_sentenceGroup_end[5] = 5;
        last_sentenceGroup_start[6] = 7;
        last_sentenceGroup_end[6] = 7;
        last_sentenceGroup_start[7] = 2;
        last_sentenceGroup_end[7] = 2;
        last_sentenceGroup_start[8] = 9;
        last_sentenceGroup_end[8] = 11;
        last_sentenceGroup_start[9] = 4;
        last_sentenceGroup_end[9] = 7;
    }
}
