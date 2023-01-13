using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Image _fillLife;
    [SerializeField] 

    public void UpdateHealthBar(float fillAmount)
    {
        _fillLife.fillAmount = fillAmount;
    }
}
