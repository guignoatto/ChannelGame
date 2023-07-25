using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class SkillBase : MonoBehaviour
{
    public Action<List<EnemyBase>> OnRefreshEnemies;
    public Func<ISkillType, ProjectileBase> GetProjectile;
    public SkillPreset SkillPreset;
    public int SkillLevel = 0;

    protected EnemyDetectionField _enemyDetectionField;
    
    [SerializeField] protected List<EnemyBase> _enemyList;
    [Header("SkillAttributes")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _cooldown;
    [SerializeField] protected float _projectileSpeed;
    [SerializeField] protected float _projectileDuration;

    protected Transform projectileParentTransform = null;
    protected float timer;
    protected SkillSound _skillSound;

    public virtual void RefreshEnemyList(List<EnemyBase> newEnemyList)
    {
        _enemyList = newEnemyList;
    }
    public virtual void Initialize()
    {
        if (projectileParentTransform == null)
            projectileParentTransform = transform;

        timer = 0;
        _skillSound = GetComponent<SkillSound>();
        _enemyList = FindObjectsOfType<EnemyBase>().ToList();
        _damage = SkillPreset.Damage;
        _cooldown = SkillPreset.Cooldown;
        _projectileSpeed = SkillPreset.ProjectileSpeed;
        _projectileDuration = SkillPreset.ProjectileDuration;
        Attack(projectileParentTransform);
    }

    public virtual void LevelUp()
    {
    }
    public void SetEnemyDetectionField(EnemyDetectionField enemyDetectionField)
    {
        _enemyDetectionField = enemyDetectionField;
    }
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= _cooldown / Math.Pow(_cooldown, 2))
        {
            timer = 0;
            Attack(projectileParentTransform);
        }
    }

    protected virtual void Attack(Transform projectileParent = null)
    {
        if (projectileParent == null)
            projectileParent = transform;
        
        var nearestEnemy = GetClosestEnemy(_enemyList);
        if (nearestEnemy == null)
            return;
        ProjectileBase pb = GetProjectile?.Invoke(SkillPreset.SkillType);
        pb.NearestEnemy = nearestEnemy;
        pb.ProjectileDuration = _projectileDuration;
        pb.ProjectileSpeed = _projectileSpeed;
        pb.Damage = _damage;
        pb.Initialize(projectileParent, SkillPreset.SkillType);
    }
    protected Transform GetClosestEnemy(List<EnemyBase> enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (EnemyBase t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist && t.gameObject.activeSelf)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }
}
