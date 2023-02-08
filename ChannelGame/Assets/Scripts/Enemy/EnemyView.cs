using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Image _fillLife;
    [SerializeField] private GameObject _enemySprite;

    private float _spriteStartScale;

    public void Initialize()
    {
        _spriteStartScale = _enemySprite.transform.localScale.x;
    }
    public void UpdateHealthBar(float fillAmount)
    {
        _fillLife.fillAmount = fillAmount;
    }

    public void FlipSpriteRight()
    {
        if (_enemySprite.transform.localScale.x < 0)
            return;
        var newScale = _enemySprite.transform.localScale;
        newScale.x = -_spriteStartScale;

        _enemySprite.transform.localScale = newScale;
    }
    
    public void FlipSpriteLeft()
    {
        if (_enemySprite.transform.localScale.x > 0)
            return;
        var newScale = _enemySprite.transform.localScale;
        newScale.x = _spriteStartScale;

        _enemySprite.transform.localScale = newScale;
    }
}
