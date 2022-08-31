using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject _spriteObject;

    private Animator _animator;
    private Transform _spriteTransform;

    public void SetIdleAnimation()
    {
        _animator.SetTrigger("idle");
    }
    public void SetWalkingAnimation()
    {
        _animator.SetTrigger("walk");
    }

    private void Start()
    {
        _spriteTransform = _spriteObject.GetComponent<Transform>();
        _animator = _spriteObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_spriteTransform.localScale.x > 0)
            {
                Vector3 newScale = _spriteTransform.localScale;
                newScale.x *= -1;
                _spriteTransform.localScale = newScale;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_spriteTransform.localScale.x < 0)
            {
                Vector3 newScale = _spriteTransform.localScale;
                newScale.x *= -1;
                _spriteTransform.localScale = newScale;
            }
        }
    }
}
