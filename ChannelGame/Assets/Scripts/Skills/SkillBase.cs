using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;

public class SkillBase : MonoBehaviour
{
    public SkillPreset _skillPreset;
    
    [SerializeField] protected List<EnemyBase> _enemyListTest;
    [Header("SkillAttributes")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _cooldown;
    [SerializeField] protected float _projectileSpeed;
    [SerializeField] protected float _projectileDuration;

    protected Transform projectileParenTransform = null;
    private SkillSound _skillSound;
    private float timer;

    private void Start()
    {
        if (projectileParenTransform is null)
            projectileParenTransform = transform;

        timer = 0;
        _skillSound = GetComponent<SkillSound>();
        _enemyListTest = FindObjectsOfType<EnemyBase>().ToList();
        _damage = _skillPreset.Damage;
        _cooldown = _skillPreset.Cooldown;
        _projectileSpeed = _skillPreset.ProjectileSpeed;
        _projectileDuration = _skillPreset.ProjectileDuration;
        Attack();
    }
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= _cooldown / Math.Pow(_cooldown, 2))
        {
            timer = 0;
            Attack();
        }
    }

    protected virtual ProjectileBase Attack()
    {
        var nearestEnemy = GetClosestEnemy(_enemyListTest);
        if (nearestEnemy == null)
            return null;
        ProjectileBase pb = Instantiate(_skillPreset.SkillProjectile, transform).GetComponent<ProjectileBase>();
        pb.NearestEnemy = nearestEnemy;
        pb.ProjectileDuration = _projectileDuration;
        pb.ProjectileSpeed = _projectileSpeed;
        pb.Damage = _damage;
        pb.Initialize(projectileParenTransform);
        return pb;
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
    
    public float Damage { get => _damage; set => _damage = value; }
    public float ProjectileDuration { get => _projectileDuration; set => _projectileDuration = value; }
    public float ProjectileSpeed { get => _projectileSpeed; set => _projectileSpeed = value; }
    public float Cooldown { get => _cooldown; set => _cooldown = value; }
}
