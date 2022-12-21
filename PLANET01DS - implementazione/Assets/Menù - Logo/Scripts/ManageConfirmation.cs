using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageConfirmation : MonoBehaviour
{
    public GameObject Exit_Confirmation;
    public GameObject menuButtons;

    public void PositiveAnswer(){
        Application.Quit();
        Debug.Log("Quit");
    }

    public void NegativeAnswer(){
        Exit_Confirmation.SetActive(false);
        menuButtons.SetActive(true);
    }

}
