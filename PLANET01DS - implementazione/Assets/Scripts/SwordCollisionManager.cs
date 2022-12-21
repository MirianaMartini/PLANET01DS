using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollisionManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){

    }

    void OnTriggerEnter(Collider other) {
        //Debug.Log(other);
        if(other.tag != "Slash" && other.tag != "Player"){
            Debug.Log("HIT");
        }
    }
}
