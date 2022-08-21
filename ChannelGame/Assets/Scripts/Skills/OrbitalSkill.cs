using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalSkill : SkillBase
{
    [SerializeField]
    private Transform spawnSkill;

    private void Awake()
    {
        projectileParenTransform = spawnSkill;
    }
    protected override ProjectileBase Attack()
    {
        var nearestEnemy = GetClosestEnemy(_enemyListTest);
        if (nearestEnemy == null)
            return null;
        ProjectileBase pb = Instantiate(_skillPreset.SkillProjectile, spawnSkill, false).GetComponent<ProjectileBase>();
        pb.NearestEnemy = nearestEnemy;
        pb.ProjectileDuration = _projectileDuration;
        pb.ProjectileSpeed = _projectileSpeed;
        pb.Damage = _damage;
        pb.Initialize(projectileParenTransform);
        return pb;
    }

    protected override void Update()
    {
        base.Update();
        transform.Rotate(0f, 0f, _projectileSpeed * Time.deltaTime, Space.Self);
    }
}
