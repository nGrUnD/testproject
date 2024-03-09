using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _color;
    [SerializeField] private float _rate;
    [SerializeField] private float _duration = 1f;
    private Color _colorBase;
    private Coroutine _coroutine;

    private void Start()
    {
        _colorBase = _spriteRenderer.color;
    }

    public void StartHit()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(ReactionRotine());
    }

    IEnumerator ReactionRotine()
    {
        float time = 0f;
        while (time < _duration)
        {
            _spriteRenderer.color = _color;
            yield return new WaitForSeconds(_rate);
            _spriteRenderer.color = _colorBase;
            yield return new WaitForSeconds(_rate);
            time += _rate * 2f;
        }
        _spriteRenderer.color = _colorBase;
        _coroutine = null;
    }
}
