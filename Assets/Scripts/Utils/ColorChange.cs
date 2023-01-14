using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    public float duration = .2f;
    public MeshRenderer meshRenderer;
    public Color startColor = Color.white;

    private Color _correctColor;
    
    private void OnValidate() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start() 
    {
        _correctColor = meshRenderer.materials[0].GetColor("_Color");
        LerpColor();
    }

    private void LerpColor()
    {
        meshRenderer.materials[0].SetColor("_Color", startColor);
        meshRenderer.materials[0].DOColor(_correctColor, duration).SetDelay(.5f);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            LerpColor();
        }
    }
}
