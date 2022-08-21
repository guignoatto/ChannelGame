using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private SkillPickManager _skillPickManager;

    private void Start()
    {
        _skillPickManager.Initialize();
        _playerController.Initialize();

        _playerController.LevelUpEvent += OnLevelUpHandler;
    }

    private void OnLevelUpHandler()
    {
        _skillPickManager.OnLevelUpHandler();
    }
}