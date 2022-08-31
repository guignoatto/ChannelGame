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

        SetEventHandlers();
    }

    private void SetEventHandlers()
    {
        _playerExperience.LevelUpEvent += LevelUpEventHandler;
        _playerMovement.PlayerStop += PlayerStopHandler;
        _playerMovement.PlayerWalk += PlayerWalkHandler;
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
}
