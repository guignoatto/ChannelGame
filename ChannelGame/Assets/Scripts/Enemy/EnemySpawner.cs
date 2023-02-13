using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
   public Action<List<EnemyBase>> RefreshEnemyList;

   [Header("References")]
   [SerializeField] private EnemyPool _enemyPool;
   [SerializeField] private List<Transform> _spawnPoints;

   private List<EnemyBase> _enemyList;
   private float _timer = 0;

   public void Initialize()
   {
      _enemyPool.Initialize();
      _enemyList = new List<EnemyBase>();
      _enemyPool.onReturnToPool += RemoveEnemyFromList;
   }
   public void InstantiateEnemy(IEnemyType enemyType)
   {
      var newEnemy = _enemyPool.GetEnemy(enemyType);
      if (newEnemy is null)
         return;
      newEnemy.transform.parent = transform;
      newEnemy.transform.position = GetRandomSpawnPoint().position;
      
      newEnemy.gameObject.SetActive(true);
      _enemyList.Add(newEnemy);
      RefreshEnemyList?.Invoke(_enemyList);
   }

   private Transform GetRandomSpawnPoint()
   {
      return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
   }
   private void RemoveEnemyFromList(EnemyBase enemy)
   {
      _enemyList.Remove(enemy);
   }
}
