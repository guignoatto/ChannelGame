using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public Action<EnemyBase> onReturnToPool;
    [SerializeField] private int meleePoolCount;
    [SerializeField] private GameObject meleeEnemyPrefab;
    private List<EnemyBase> availableMeleeEnemies;

    public void Initialize()
    {
        availableMeleeEnemies = new List<EnemyBase>();
        for (int i = 0; i < meleePoolCount; i++)
        {
            EnemyBase newEnemy = Instantiate(meleeEnemyPrefab, transform).GetComponent<EnemyBase>();
            newEnemy.returnToPool += ReturnMeleeEnemyToPool;
            newEnemy.gameObject.SetActive(false);
            availableMeleeEnemies.Add(newEnemy);
        }
    }
    
    public EnemyBase GetEnemyMelee()
    {
        if (availableMeleeEnemies.Count < 1)
            return null;
        EnemyBase availableEnemy = availableMeleeEnemies[0];
        
        availableMeleeEnemies.RemoveAt(0);
        
        return availableEnemy;
    }

    private GameObject GetEnemyRanged()
    {
        return new GameObject();
    }
    private void ReturnMeleeEnemyToPool(EnemyBase enemy)
    {
        availableMeleeEnemies.Add(enemy);
        enemy.transform.parent = transform;
        enemy.gameObject.SetActive(false);
        onReturnToPool?.Invoke(enemy);
    }
}