using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private SkillPickManager _skillPickManager;

    private void Start()
    {
        _skillPickManager.Initialize();
        
        _playerMovement.LevelUpEvent += OnLevelUpHandler;
    }

    private void OnLevelUpHandler()
    {
        _skillPickManager.OnLevelUpHandler();
    }
}