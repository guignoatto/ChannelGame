using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Image _fillLife;

    public void UpdateHealthBar(float fillAmount)
    {
        _fillLife.fillAmount = fillAmount;
    }
}
