using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private SkillPickManager _skillPickManager;
    [SerializeField] private WaveManager _waveManager;

    private void Awake()
    {
        _skillPickManager.Initialize();
        _playerController.Initialize();
        _waveManager.Initialize();

        _playerController.LevelUpEvent += OnLevelUpHandler;
        _skillPickManager.NewSkillInstantiated += NewSkillInstantiatedHandler;
    }

    private void OnLevelUpHandler()
    {
        _skillPickManager.OnLevelUpHandler();
    }

    private void NewSkillInstantiatedHandler(SkillBase newSkill)
    {
        _waveManager.RefreshEnemyList += newSkill.RefreshEnemyList;
    }
}