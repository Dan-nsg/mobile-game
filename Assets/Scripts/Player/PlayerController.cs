using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plataformer2D.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endScreen;

    [Header("Text")]
    public TextMeshPro uiTextPowerUp;
    public GameObject coinCollector;


    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPosition;

    public bool invencible = true;

    public float speed = 1f;

    private void Start() 
    {
        _startPosition = transform.position;
        ResetSpeed();
    }
    private void Awake() 
    {
    }
    void Update()
    {
        if(!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.transform.tag == tagToCheckEnemy)
        {
           if(!invencible) EndGame();
        }    
    }
    
    private void OnTriggerEnter(Collider collision) 
    {
        if(collision.transform.tag == tagToCheckEndLine)
        {
            if(!invencible) EndGame();
        }
    }

    private void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }
    
    public void startToRun()
    {
        _canRun = true;
    }

    #region POWERUPS
    public void SetPowerUpText(string s) 
    {
        uiTextPowerUp.text = s;
    }
    public void PowerUpSpeed(float f)
    {
        _currentSpeed = f;
    }

    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void ResetSpeed(bool b = false)
    {
        _currentSpeed = speed;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
/*         var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);//.OnComplete(ResetHeight);
        Invoke(nameof(ResetHeight), duration); 
    }

    public void ResetHeight()
    {
        transform.DOMoveY(_startPosition.y, .1f);        
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }

    #endregion
}
