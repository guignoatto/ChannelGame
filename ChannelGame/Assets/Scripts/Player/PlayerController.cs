using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action LevelUpEvent;
    
    private PlayerMovement _playerMovement;
    private PlayerExperience _playerExperience;
    private PlayerMagneticField _playerMagneticField;
    private PlayerView _playerView;
    private PlayerHealth _playerHealth;
    private List<SkillBase> _mySkills;

    public void GetNewEnemyList(List<EnemyBase> newEnemyList)
    {
        
    }
    public void GetExperience(float xp)
    {
        _playerExperience.GetExperiencePoints(xp);
    }
    public void Initialize()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerExperience = GetComponent<PlayerExperience>();
        _playerMagneticField = GetComponentInChildren<PlayerMagneticField>();
        _playerView = GetComponent<PlayerView>();
        _playerHealth = GetComponent<PlayerHealth>();

        _playerHealth.Initialize();
        
        SetEventHandlers();
    }
    
    private void SetEventHandlers()
    {
        _playerExperience.LevelUpEvent += LevelUpEventHandler;
        _playerMovement.PlayerStop += PlayerStopHandler;
        _playerMovement.PlayerWalk += PlayerWalkHandler;
        _playerHealth.OnTakeDamage += TakeDamageHandler;
    }

    private void LevelUpEventHandler()
    {
        LevelUpEvent?.Invoke();
    }

    private void PlayerStopHandler()
    {
        _playerView.SetIdleAnimation();
    }

    private void PlayerWalkHandler()
    {
        _playerView.SetWalkingAnimation();
    }

    private void TakeDamageHandler(float health, float totalHealth)
    {
        _playerView.TakeDamageHandler(health, totalHealth);
    }
}
