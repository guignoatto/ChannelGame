using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private List<WaveConfig> _waveConfig;
    [SerializeField] private float maxTime = 1200;

    private IEnumerator spawnEnemiesCoroutine;
    
    private float timer;
    private int wave = 1;

    private void Initialize()
    {
        spawnEnemiesCoroutine = SpawnEnemies(_waveConfig[wave].frequency);
        StartCoroutine(spawnEnemiesCoroutine);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > (maxTime / _waveConfig.Count) * wave)
        {
            wave += 1;
            
            StopCoroutine(spawnEnemiesCoroutine);
            
            spawnEnemiesCoroutine = SpawnEnemies(_waveConfig[wave].frequency);
            StartCoroutine(spawnEnemiesCoroutine);
        }
    }
    private IEnumerator SpawnEnemies(float frequency)
    {
        while (true)
        {
            yield return new WaitForSeconds(frequency);
        }
    }
}