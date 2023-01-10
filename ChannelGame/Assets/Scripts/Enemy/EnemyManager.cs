using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
   public Action<List<EnemyBase>> RefreshEnemyList;

   [Header("References")]
   [SerializeField] private EnemyPool _enemyPool;
   [SerializeField] private List<Transform> _spawnPoints;
   [Header("Config")] 
   [SerializeField]
   private float _spawnCooldown;

   [SerializeField] private int _maxEnemies = 4;

   private List<EnemyBase> _enemyList;
   private float _timer = 0;

   public void Initialize()
   {
      _enemyPool.Initialize();
      _enemyList = new List<EnemyBase>();
      _enemyPool.onReturnToPool += RemoveEnemyFromList;
   }
   public void InstantiateMeleeEnemy()
   {
      var newEnemy = _enemyPool.GetEnemyMelee();
      if (newEnemy is null)
         return;
      newEnemy.transform.parent = this.transform;
      newEnemy.transform.position = GetRandomSpawnPoint().position;
      
      newEnemy.gameObject.SetActive(true);
      _enemyList.Add(newEnemy);
      RefreshEnemyList?.Invoke(_enemyList);
   }

   private Transform GetRandomSpawnPoint()
   {
      return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
   }
   private void Update()
   {
      _timer += Time.deltaTime;
      if (_timer >= _spawnCooldown && _enemyList.Count < _maxEnemies)
      {
         InstantiateMeleeEnemy();
         _timer = 0;
      }
   }

   private void RemoveEnemyFromList(EnemyBase enemy)
   {
      _enemyList.Remove(enemy);
   }
}
