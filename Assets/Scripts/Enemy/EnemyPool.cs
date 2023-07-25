using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public Action<EnemyBase> onReturnToPool;
    [Header("Melee")]
    [SerializeField] private GameObject meleeEnemyPrefab;
    [SerializeField] private int meleePoolCount;
    [Header("Ranged")]
    [SerializeField] private GameObject rangedEnemyPrefab;
    [SerializeField] private int rangedPoolCount;
    private List<EnemyBase> availableMeleeEnemies;
    private List<EnemyBase> availableRangedEnemies;

    public void Initialize()
    {
        availableMeleeEnemies = new List<EnemyBase>();
        availableRangedEnemies = new List<EnemyBase>();
        for (int i = 0; i < meleePoolCount; i++)
        {
            EnemyBase newEnemy = Instantiate(meleeEnemyPrefab, transform).GetComponent<EnemyBase>();
            newEnemy.returnToPool += ReturnMeleeEnemyToPool;
            newEnemy.gameObject.SetActive(false);
            availableMeleeEnemies.Add(newEnemy);
        }
        
        for (int i = 0; i < rangedPoolCount; i++)
        {
            EnemyBase newEnemy = Instantiate(rangedEnemyPrefab, transform).GetComponent<EnemyBase>();
            newEnemy.returnToPool += ReturnRangedEnemyToPool;
            newEnemy.gameObject.SetActive(false);
            availableRangedEnemies.Add(newEnemy);
        }
    }
    public EnemyBase GetEnemy(IEnemyType enemyType)
    {
        List<EnemyBase> enemies = GetListByType(enemyType);
        if (enemies.Count < 1)
            return null;
        EnemyBase availableEnemy = enemies[0];
        
        enemies.RemoveAt(0);
        
        return availableEnemy;
    }
    private void ReturnMeleeEnemyToPool(EnemyBase enemy)
    {
        availableMeleeEnemies.Add(enemy);
        enemy.transform.parent = transform;
        enemy.gameObject.SetActive(false);
        onReturnToPool?.Invoke(enemy);
    }
    private void ReturnRangedEnemyToPool(EnemyBase enemy)
    {
        availableRangedEnemies.Add(enemy);
        enemy.transform.parent = transform;
        enemy.gameObject.SetActive(false);
        onReturnToPool?.Invoke(enemy);
    }
    private List<EnemyBase> GetListByType(IEnemyType enemyType)
    {
        switch (enemyType)
        {
            case IEnemyType.MELEE:
                return availableMeleeEnemies;
            case IEnemyType.RANGED:
                return availableRangedEnemies;
            default:
                return availableMeleeEnemies;
        }
    }
}