using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageVideo : MonoBehaviour
{
    public bool Loop = false;
    public float Speed = 1f;
    public Sprite[] Frames;

    private int _counter = 1;
    private bool _flag = true;
    private bool _play = false;

    // Start is called before the first frame update
    void Awake(){
        GetComponent<RawImage>().texture = Frames[0].texture;
    }

    void Start(){
        GetComponent<RawImage>().texture = Frames[0].texture;
    }

    // Update is called once per frame
    void Update()
    {
        if(_play){
            if(_counter < Frames.Length){
                if(_flag){
                    _flag = false;
                    StartCoroutine(ShowFrame(Frames[_counter++]));
                }
            }
            else {
                if(Loop)
                    _counter=0;
            }
        }
    }

    private IEnumerator ShowFrame(Sprite frame){
        GetComponent<RawImage>().texture = frame.texture;
        yield return new WaitForSeconds(0.03f/Speed);
        _flag = true;        
    }

    public void Play(){
        _counter=0;
        _play = true;
    }

    public void Stop(){
        _counter=0;
        _play = false;
    }
}
