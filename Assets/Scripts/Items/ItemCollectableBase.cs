using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{

    public string compareTag = "Player";
    public ParticleSystem coinParticleSystem;
    public float timeToHide = 1;
    public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void Awake() 
    {
        if(coinParticleSystem != null) coinParticleSystem.transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider collision) 
    {
        if(collision.transform.CompareTag(compareTag)) 
        {
            Collect();
        }
    }

    protected virtual void HideItens()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke("HideObject", timeToHide);
    }

    protected virtual void Collect() 
    {
        HideItens();
        OnCollect();
    }

    private void HideObject()
    {
      gameObject.SetActive(false);
    }

    protected virtual void OnCollect() 
    { 
        if(coinParticleSystem != null)
            coinParticleSystem.Play();
        if(audioSource != null) audioSource.Play();
        
        if(coinParticleSystem != null) 
        {
            coinParticleSystem.transform.parent = null;
            Destroy(coinParticleSystem.gameObject, 2f);
        }
    }


}
