using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Skill360 : SkillBase
{
    [SerializeField] private List<Transform> _spawnList;
    public override void Initialize()
    {
        if (projectileParentTransform is null)
            projectileParentTransform = transform;

        timer = 0;
        _skillSound = GetComponent<SkillSound>();
        _enemyList = FindObjectsOfType<EnemyBase>().ToList();
        _damage = SkillPreset.Damage;
        _cooldown = SkillPreset.Cooldown;
        _projectileSpeed = SkillPreset.ProjectileSpeed;
        _projectileDuration = SkillPreset.ProjectileDuration;
        Attack360();
    }

    protected override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= _cooldown / Math.Pow(_cooldown, 2))
        {
            Attack360();
        }
    }

    private void Attack360()
    {
        foreach (var spawn in _spawnList)
        {
            timer = 0;
            Attack(spawn);
        }
    }
}