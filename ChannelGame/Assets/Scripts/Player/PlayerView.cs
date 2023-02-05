using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Image = UnityEngine.UI.Image;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject _spriteObject;
    [SerializeField] private Image _fillLife;

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

    public void TakeDamageHandler(float health, float totalHealth)
    {
        UpdateHealthBar(health, totalHealth);
    }
    
    

    private void Start()
    {
        _spriteTransform = _spriteObject.GetComponent<Transform>();
        _animator = _spriteObject.GetComponent<Animator>();
        _fillLife.fillAmount = 1;
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
    
    private void UpdateHealthBar(float health, float totalHealth)
    {
        float fillAmount = health / totalHealth;
        _fillLife.fillAmount = fillAmount;
    }
}
