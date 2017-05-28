using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class ButtonScript : MonoBehaviour, ITouchable {

    // add force to star
    public Rigidbody2D rigidbodyToControll; 
    public float forceMultiply = 1.5f; 

    //fade in / fade out
    public float startAlpha = 0; // pocatecni hodnota alpha kanalu
    public float endAlpha = 1f; // konecna hodnota alpha kanalu
    public float fadeDuration = 2f; // doba prechodu
    public bool show = true; // fade in ?
    

    private float _startTime;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _startTime = Time.time;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        var color = _spriteRenderer.color;
        color.a = startAlpha;
        _spriteRenderer.color = color;
    }

    void Update()
    {
        if(_spriteRenderer.color.a != endAlpha && show)
        {
            float t = (Time.time - _startTime) / fadeDuration;
            _spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(startAlpha, endAlpha, t));
        }
        else if(_spriteRenderer.color.a != startAlpha && !show)
        {
            float t = (Time.time - _startTime) / fadeDuration;
            _spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(endAlpha, startAlpha, t));
        }
    }

    void ITouchable.OnTouch()
    {
        var inverColorEffect = Camera.main.GetComponent<InvertColorEffect>();
        if (inverColorEffect == null)
            return;

        inverColorEffect.enabled = !inverColorEffect.enabled;
    }
}
