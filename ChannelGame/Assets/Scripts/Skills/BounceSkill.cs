using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BounceSkill : SkillBase
{
    [SerializeField] private int _bounces = 1;

    public override void LevelUp()
    {
        var cast = (SkillsLevelUpConfigBounce)SkillPreset.skillLevelConfigBase;
        var levelConfig = cast.BounceSkillLevelConfigList[SkillLevel];
        if(SkillLevel < cast.BounceSkillLevelConfigList.Count - 1)
            SkillLevel += 1;

        _damage *= levelConfig.DamageIncrease;
        _cooldown *= levelConfig.CooldownDecrease;
        _projectileSpeed *= levelConfig.ProjectileSpeedIncrease ;
        _projectileDuration *= levelConfig.ProjectileDurationIncrease;
        _bounces += levelConfig.bounceIncrease;
    }

    public override void RefreshEnemyList(List<EnemyBase> newEnemyList)
    {
        base.RefreshEnemyList(newEnemyList);
        OnRefreshEnemies?.Invoke(newEnemyList);
    }

    protected override void Attack(Transform projectileParent = null)
    {
        if (projectileParent == null)
            projectileParent = transform;

        var nearestEnemy = GetClosestEnemy(_enemyList);
        if (nearestEnemy == null)
            return;
        ProjectileBase pb = GetProjectile?.Invoke(SkillPreset.SkillType);

        pb.gameObject.SetActive(true);
        pb.NearestEnemy = nearestEnemy;
        pb.ProjectileDuration = _projectileDuration;
        pb.ProjectileSpeed = _projectileSpeed;
        pb.Damage = _damage;
        pb.Initialize(projectileParent, SkillPreset.SkillType);

        var lp = pb.gameObject.GetComponent<BounceProjectile>();
        lp.SetFirstTarget(_enemyList[Random.Range(0, _enemyList.Count - 1)].transform, _enemyDetectionField);
        lp.SetBounces(_bounces);
    }
}