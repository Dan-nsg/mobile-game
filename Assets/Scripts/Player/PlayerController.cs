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

    private float _baseSpeedToAnimation = 7;

    [Header("Animation")]
    public AnimatorManager animatorManager;
    [SerializeField] private BounceHelper _bounceHelper;

    [Header("VFX")]
    public ParticleSystem vfxDeath;

    [Header("Map Limit")]
    public float limit = 4;
    public Vector2 limitVector = new Vector2(-4, 4);


    private void Start() 
    {
        _startPosition = transform.position;
        ResetSpeed();
    }

    public void Bounce()
    {   
        if(_bounceHelper != null)
            _bounceHelper.Bounce();
    }

    private void Awake() 
    {
        base.Awake();
    }
    void Update()
    {
        if(!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        if(_pos.x < limitVector.x) _pos.x = limitVector.x;
        else if(_pos.x > limitVector.y) _pos.x = limitVector.y;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.transform.tag == tagToCheckEnemy)
        {
           if(!invencible)
           {    
                MoveBack();
                EndGame(AnimatorManager.AnimationType.DEAD);
           }
        }    
    }
    
    private void OnTriggerEnter(Collider collision) 
    {
        if(collision.transform.tag == tagToCheckEndLine)
        {
            if(!invencible) EndGame();
        }
    }

    private void MoveBack()
    {
        transform.DOMoveZ(-1f, .3f).SetRelative();
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
        if(vfxDeath != null) vfxDeath.Play();
    }
    
    public void startToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
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
