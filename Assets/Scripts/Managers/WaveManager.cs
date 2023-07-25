using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{   
    public Action<List<EnemyBase>> RefreshEnemyList;

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private WaveConfig _waveConfig;
    [SerializeField] private float maxTime = 1200;

    private IEnumerator spawnEnemiesCoroutine;
    
    [SerializeField]private float timer;
    private int wave = 0;

    public void Initialize()
    {
        _enemySpawner.Initialize();
        _enemySpawner.RefreshEnemyList += RefreshEnemyListHandler;
        
        foreach (var tEnemyConfig in _waveConfig.SingleWaveConfig[wave].EnemyConfig)
        {
            StartCoroutine(SpawnEnemies(tEnemyConfig.EnemyType, tEnemyConfig.Frequency));
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > (maxTime / _waveConfig.SingleWaveConfig.Count) * (wave + 1))
        {
            StopAllCoroutines();

            wave += 1;
            foreach (var tEnemyConfig in _waveConfig.SingleWaveConfig[wave].EnemyConfig)
            {
                StartCoroutine(SpawnEnemies(tEnemyConfig.EnemyType, tEnemyConfig.Frequency));
            }

        }
    }
    private IEnumerator SpawnEnemies(IEnemyType enemyType, float frequency)
    {
        while (true)
        {
            _enemySpawner.InstantiateEnemy(enemyType);
            yield return new WaitForSeconds(frequency);
        }
    }

    public void RefreshEnemyListHandler(List<EnemyBase> enemiesList)
    {
        RefreshEnemyList?.Invoke(enemiesList);
    }
}