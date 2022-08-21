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
    
    public void GetExperience(float xp)
    {
        _playerExperience.GetExperiencePoints(xp);
    }
    public void Initialize()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerExperience = GetComponent<PlayerExperience>();
        _playerMagneticField = GetComponentInChildren<PlayerMagneticField>();

        SetEventHandlers();
    }

    private void SetEventHandlers()
    {
        _playerExperience.LevelUpEvent += LevelUpEventHandler;
    }

    private void LevelUpEventHandler()
    {
        LevelUpEvent?.Invoke();
    }
}
