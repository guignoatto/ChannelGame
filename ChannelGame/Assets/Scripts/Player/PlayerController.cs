using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerExperienceCollector
{
    public Action OnDie;
    public Action LevelUpEvent;
    
    private PlayerMovement _playerMovement;
    private PlayerExperience _playerExperience;
    private PlayerMagneticField _playerMagneticField;
    private PlayerView _playerView;
    private PlayerHealth _playerHealth;
    private PlayerSkillController _playerSkillController;

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
    public void GetExperience(float xp)
    {
        _playerExperience.GetExperiencePoints(xp);
    }
    
    private void SetEventHandlers()
    {
        _playerExperience.LevelUpEvent += LevelUpEventHandler;
        _playerMovement.PlayerStop += PlayerStopHandler;
        _playerMovement.PlayerWalk += PlayerWalkHandler;
        _playerHealth.OnTakeDamage += TakeDamageHandler;
        _playerHealth.OnPlayerDie += OnDieHandler;
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
    private void OnDieHandler()
    {
        OnDie?.Invoke();
    }
}
