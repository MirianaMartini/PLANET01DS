using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using UnityEngine.SceneManagement;

public class Canvas_controller : MonoBehaviour
{
    [SerializeField] private StarterAssets.ThirdPersonController player;
    [SerializeField] private audio_manager soundtrack_mng;
    [SerializeField] private audio_manager pause_mng;
    [SerializeField] private stats_controller stats;
    private bool viewingMission = false;

    private StarterAssetsInputs _input;
    //[SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _input = player.GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_input.pause)
        {
            soundtrack_mng.ChangeToOther();
            openCanvas(transform.childCount - 1);
            Time.timeScale = 0f;
            _input.pause = false;
        }
        if (_input.mission)
        {
            if (!viewingMission)
            {
                viewingMission = true;
                soundtrack_mng.ChangeToOther();
                openCanvas(transform.childCount - 2);
                Time.timeScale = 0f;
                _input.mission = false;
            }
            else
            {
                _input.mission = false;
                Resume();
            }
        }
        if (_input.next_quest)
        {
            stats.NextQuest();
            _input.next_quest = false;
        }
    }

    public void Resume()
    {
        pause_mng.ChangeToOther();
        Time.timeScale = 1f;
        closeCanvas(transform.childCount - 1);
        closeCanvas(transform.childCount - 2);
        if(viewingMission) viewingMission = false;
    }

    public void closeCanvas(int child)
    {
        transform.GetChild(child).gameObject.SetActive(false);
        player.UI_setActive(false);
        //player.UI_active = false;
        Cursor.visible = false;
    }

    public void openCanvas(int child)
    {
        transform.GetChild(child).gameObject.SetActive(true);
        if(child == 2 || child == 4 || child == transform.childCount-1 || child == transform.childCount - 2)
        {
            //player.UI_active = true;
            player.UI_setActive(true);
            Cursor.visible = true;
        }
    }

    public void moveCanvas(int child, Vector3 pos, Quaternion rot)
    {
        transform.GetChild(child).transform.position = pos;
        transform.GetChild(child).transform.rotation = rot;
    }

    public bool isOpen(int child)
    {
        return transform.GetChild(child).gameObject.activeSelf;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void newMission(string text, bool update)
    {
        transform.GetChild(transform.childCount - 2).GetChild(2).GetComponent<Text>().text = text;
        if (update)
        {
            transform.GetChild(transform.childCount - 3).GetComponent<UImission_controller>().UpdateMission();
        }
        else
        {
            transform.GetChild(transform.childCount - 3).GetComponent<UImission_controller>().NewMission();
        }
    }
}
