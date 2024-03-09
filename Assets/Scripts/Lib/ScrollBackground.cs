using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 10f;
    private float _titleSize;
    private float _imagePosition = 0f;
    private void Start()
    {
        _titleSize = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        _imagePosition -= _scrollSpeed * Time.deltaTime;
        _imagePosition = Mathf.Repeat(_imagePosition, _titleSize);
        
        Vector3 position = transform.position;
        position.x = _imagePosition;
        transform.position = position;
    }
}
