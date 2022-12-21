using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class general_UI_controller : MonoBehaviour
{
    public stats_controller player;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < (int)(player.life/2) - 1; i++)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public bool lifeup( int val )
    {
        if (player.life + val <= 8)
        {
            if((player.life + val)%2 == 0)
            {
                transform.GetChild((int)((player.life + val) / 2)).GetChild(0).gameObject.SetActive(true);

            }
            else
            {
                transform.GetChild((int)((player.life + val) / 2) - 1).GetChild(0).gameObject.SetActive(true);
                transform.GetChild((int)((player.life + val) / 2)).GetChild(0).gameObject.SetActive(false);
            }
            transform.GetChild((int)((player.life + val) / 2));
            player.life = player.life + val;
            return true;
        }
        else
        {
            return false;
        }
    }*/

    public void addHeart(int pos, int half)
    {
        transform.GetChild(pos).GetChild(half).gameObject.SetActive(true);
    }

    public void deleteHeart(int pos, int half)
    {
        transform.GetChild(pos).GetChild(half).gameObject.SetActive(false);
    }

    public void cancelHeartSlot()
    {
        transform.GetChild(4).GetChild(2).gameObject.SetActive(true);
        transform.GetChild(4).GetChild(3).gameObject.SetActive(true);
    }
}
