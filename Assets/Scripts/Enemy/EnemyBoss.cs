using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    [SerializeField] private float EnrageHpPercentage;
    [SerializeField] private float EnrageSpeedMultiplier;
    [Header("Area Attack")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float _attackCooldown = 5;
    [SerializeField] private int _projectileCount = 8;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private int _projectileDamage;
    [SerializeField] private float _projectileLifetime;
    

    public Transform firePoint;
    private bool hasEnraged = false;
    private Animator BossAnimator;
    private float normalspeed; 
    private float timer = 0f;
    //public AK.Wwise.Event EnrageSound;

    protected override void Start()
    {
        normalspeed = _speed;
        base.Start();
        BossAnimator = GetComponentInChildren<Animator>();
        /*StartCoroutine(AreaAttackCooldown());*/

    }
    protected override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        
        if (!hasEnraged)
        {
            CheckEnrage();
            
        }
        else if (timer >= _attackCooldown) {
            Debug.Log("Attack");
            BossAnimator.SetTrigger("AreaAttack");
            timer = 0f;
            /*AreaAttack();*/
        }

    }

    private void CheckEnrage()
    {
        if (life <= maxLife * (EnrageHpPercentage / 100))
        {
            Debug.Log("50%");
            BossAnimator.SetBool("IsEnraged", true);
            _speed = normalspeed * EnrageSpeedMultiplier;
            //EnrageSound.Post(gameObject);
            //GetComponent<Animator>().SetBool("IsEnraged", true);
            hasEnraged = true;
            StartCoroutine(AreaAttackCooldown());

        }
    }

    private void AreaAttack() 
    {
/*        float angleIncrement = 360f / _projectileCount;

        for (int i = 0; i < _projectileCount; i++)
        {
            float angle = i * angleIncrement;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = projectile.transform.up * _projectileSpeed;
            Destroy(projectile, _projectileLifetime);
        }*/
        var projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity).GetComponent<EnemyAroundSkill>();


        projectile.Initialize(_projectileDamage, _projectileSpeed, _projectileCount, _projectileLifetime);

      
    }
    private IEnumerator AreaAttackCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackCooldown);
            AreaAttack();
        }
    }
}
