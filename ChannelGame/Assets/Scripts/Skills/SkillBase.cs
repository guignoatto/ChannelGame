using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;

public class SkillBase : MonoBehaviour
{
    [SerializeField] private List<EnemyBase> _enemyListTest;
    [SerializeField] protected float _damage;
    [Range(1f,15f)]
    [SerializeField] protected float _skillCooldown;
    [SerializeField] private float _skillDuration;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] protected GameObject _projectile;

    private SkillSound _skillSound;
    private float timer;

    private void Start()
    {
        timer = 0;
        _skillSound = GetComponent<SkillSound>();
        _enemyListTest = FindObjectsOfType<EnemyBase>().ToList();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= _skillCooldown / Math.Pow(_skillCooldown, 2))
        {
            timer = 0;
            Attack();
        }
    }

    protected virtual void Attack()
    {
        var nearestEnemy = GetClosestEnemy(_enemyListTest);
        if (nearestEnemy == null)
            return;
        ProjectileBase pb = Instantiate(_projectile, transform).GetComponent<ProjectileBase>();
        pb.NearestEnemy = nearestEnemy;
        pb.ProjectileDuration = _skillDuration;
        pb.ProjectileSpeed = _projectileSpeed;
        pb.Damage = _damage;
        _skillSound.PlaySoundEffect(_skillSound.SkillAudioClip,1,2f);
    }
    Transform GetClosestEnemy(List<EnemyBase> enemies)
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
