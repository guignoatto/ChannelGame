using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class BaseSkillLevelConfig
{
   public string LevelDescription;
   public float DamageIncrease = 1;
   public float CooldownDecrease = 1;
   public float ProjectileSpeedIncrease = 1;
   public float ProjectileDurationIncrease = 1;
}
