using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mattoni_controller : MonoBehaviour
{

    private Transform[] mattoni = new Transform[7];
    //private BoxCollider targetPosition;
    //private Transform chart;
    private Transform mattoni_carrello;
    private ParticleSystem nuvoletta;
    [SerializeField] private NPC_talks_controller NPC;
    [SerializeField] private stats_controller stats;
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        //targetPosition = transform.GetChild(0).GetChild(3).GetComponent<BoxCollider>();
        //chart = transform.GetChild(0).GetChild(2);
        mattoni_carrello = transform.GetChild(0).GetChild(1);
        nuvoletta = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<ParticleSystem>();
        for (int j = 1; j<8; j++)
        {
            mattoni[j-1] = transform.parent.GetChild(j).GetChild(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChartStopped()
    {
        if (trigger_ferrovia_controller.chartOK)
        {
            if (i < 7)
            {
                mattoni[i].parent.GetChild(2).GetComponent<ParticleSystem>().Play();
                mattoni[i].gameObject.SetActive(false);
                nuvoletta.Play();
                mattoni_carrello.gameObject.SetActive(true);
                stats.completed_bricks++;
            }
            i++;
            if (i == 7)
            {
                NPC.updateCounters();
            }

        }
    }


}
