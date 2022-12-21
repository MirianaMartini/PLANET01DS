using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class damageManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject LavaEffect;
    public DamageVideo BloodEffect;
    private GameObject VulnerabilityEffect;
    private ParticleSystem BloodSplash;
    
    private ThirdPersonController _playerTPC;
    private bool _hitFlag = true;
    static bool _vulnerability = true;
        
    // Start is called before the first frame update
    void Start(){
        _playerTPC = Player.GetComponent<ThirdPersonController>();
        VulnerabilityEffect = Player.transform.GetChild(Player.transform.childCount - 1).GetChild(0).gameObject;
        BloodSplash = Player.transform.GetChild(4).GetChild(0).gameObject.GetComponent<ParticleSystem>();

        if (gameObject.tag == "Lava"){
            if(LavaEffect)
                LavaEffect.SetActive(false);
            if(VulnerabilityEffect)
                VulnerabilityEffect.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            if(gameObject.tag == "Lava"){
                if(_vulnerability){
                    _playerTPC.Damage("LavaHit", true, BloodEffect);
                    LavaEffect.SetActive(true);
                    BloodSplash.Play();
                    _playerTPC.PlaySound(3);
                    if(!_playerTPC._gameOver)
                        StartCoroutine(VulnerabilityAfter(3f));
                }
                else {
                    _playerTPC.Damage("LavaHit", false, BloodEffect);
                    LavaEffect.SetActive(true);
                    BloodSplash.Play();
                    _playerTPC.PlaySound(3);
                }
            }
            else if(gameObject.tag == "pistone"){
                GetComponent<Collider>().enabled = false;
                if(_hitFlag){
                    _hitFlag = false;
                    if(_vulnerability)
                        _playerTPC.Damage("Pistone", true, BloodEffect);
                    BloodSplash.Play();
                    StartCoroutine(FreeFlagAfter(1.5f));
                }
            }
            else if(gameObject.tag == "spuntone"){
                if(_hitFlag){
                    _hitFlag = false;
                    _playerTPC.Damage("Spuntone", true, BloodEffect);
                    BloodSplash.Play();
                    StartCoroutine(FreeFlagAfter(1.5f));
                }
            }
            if(gameObject.tag == "spina"){
                if(_vulnerability){
                    _playerTPC.Damage("Spine", true, BloodEffect);
                    BloodSplash.Play();
                    if (!_playerTPC._gameOver)
                        StartCoroutine(VulnerabilityAfter(3f));
                }
            }
        }
    }

    void OnTriggerStay(Collider other){
        if(other.tag == "Player"){
            if(gameObject.tag == "Lava"){
                if(_vulnerability){
                    _playerTPC.Damage("LavaHit", true, BloodEffect);
                    BloodSplash.Play();
                    LavaEffect.SetActive(true);
                    if(!_playerTPC._gameOver)
                        StartCoroutine(VulnerabilityAfter(3f));
                }
                else {
                    _playerTPC.Damage("LavaHit", false, BloodEffect);
                    LavaEffect.SetActive(true);
                }
            }
            else if(gameObject.tag == "spina"){
                if(_vulnerability){
                    _playerTPC.Damage("Spine", true, BloodEffect);
                    BloodSplash.Play();
                    if (!_playerTPC._gameOver)
                        StartCoroutine(VulnerabilityAfter(3f));
                }
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            if(gameObject.tag == "Lava"){
                _playerTPC.Damage("Lava", false, BloodEffect);
                LavaEffect.SetActive(false);
                _playerTPC.PlaySound(-1);
            }
            else if(gameObject.tag == "pistone"){
                GetComponent<Collider>().enabled = true;
            }
        }
    }

    private IEnumerator FreeFlagAfter(float time){
        yield return new WaitForSeconds(time);
        _hitFlag = true;
    }

    private IEnumerator VulnerabilityAfter(float time){
        Debug.Log("Invulnerable");
        _vulnerability = false;
        if(VulnerabilityEffect)
            VulnerabilityEffect.SetActive(true);

        yield return new WaitForSeconds(time);

        if(VulnerabilityEffect)
            VulnerabilityEffect.SetActive(false);
        _vulnerability = true;
        Debug.Log("Vulnerable");
    }

}
