using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public Action LevelUpEvent;
    public int level = 1;

    [SerializeField]
    private LevelUpConfig _levelUpConfig;
    private float experiencePoints = 0;
    public void GetExperiencePoints(float xp)
    {
        experiencePoints += xp;
        if (_levelUpConfig.XpToLevelUp.Count < level)
            return;
        if (experiencePoints >= _levelUpConfig.XpToLevelUp[level -1])
        {
            LevelUpEvent?.Invoke();
            level += 1;
            experiencePoints = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 0;
            LevelUpEvent?.Invoke();
        }
    }


}
