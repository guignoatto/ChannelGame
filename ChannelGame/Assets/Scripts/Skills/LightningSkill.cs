using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightningSkill : SkillBase
{
    [SerializeField] private EnemyDetectionField _enemyDetectionField;
    [SerializeField] private int _bounces = 10;
    public override void Initialize()
    {
        base.Initialize();
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
        
        var nearestEnemy = GetClosestEnemy(_enemyListTest);
        if (nearestEnemy == null)
            return;
        ProjectileBase pb = GetProjectile?.Invoke(_skillPreset.SkillType);
        
        pb.gameObject.SetActive(true);
        pb.NearestEnemy = nearestEnemy;
        pb.ProjectileDuration = _projectileDuration;
        pb.ProjectileSpeed = _projectileSpeed;
        pb.Damage = _damage;
        pb.Initialize(projectileParent, _skillPreset.SkillType);

        var lp = pb.gameObject.GetComponent<LightningProjectile>();
        lp.SetFirstTarget(_enemyListTest[Random.Range(0, _enemyListTest.Count - 1)].transform, _enemyDetectionField);
        lp.SetBounces(_bounces);
    }
}
