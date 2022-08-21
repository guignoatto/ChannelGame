using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillPreset", menuName = "Skills/SkillPreset")]
public class SkillPreset : ScriptableObject
{
    public string SkillName;
    public GameObject SkillPrefab;
    public GameObject SkillProjectile;
    public ISkillType SkillType;
    [Header("Base Status")]
    public float Damage;
    public float Cooldown;
    public float ProjectileSpeed;
    public float ProjectileDuration;


}
