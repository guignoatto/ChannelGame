using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Skill360 : SkillBase
{
    [SerializeField] private List<Transform> _spawnList;
    protected override void Start()
    {
        if (projectileParentTransform is null)
            projectileParentTransform = transform;

        timer = 0;
        _skillSound = GetComponent<SkillSound>();
        _enemyListTest = FindObjectsOfType<EnemyBase>().ToList();
        _damage = _skillPreset.Damage;
        _cooldown = _skillPreset.Cooldown;
        _projectileSpeed = _skillPreset.ProjectileSpeed;
        _projectileDuration = _skillPreset.ProjectileDuration;
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