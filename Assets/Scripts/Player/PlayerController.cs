using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endScreen;
    private bool _canRun;
    private Vector3 _pos;

    public float speed = 1f;

    private void Awake() 
    {
        startToRun();
    }
    void Update()
    {
        if(!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.transform.tag == tagToCheckEnemy)
        {
           EndGame();
        }    
    }
    
    private void OnTriggerEnter(Collider collision) 
    {
        if(collision.transform.tag == tagToCheckEndLine)
        {
            EndGame();
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
}
