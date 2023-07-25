using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDetectionField : MonoBehaviour
{
    
    public List<Transform> EnemiesInRange = new List<Transform>();


    public Transform GetRandomEnemyPosition(Transform t)
    {
        if (EnemiesInRange.Count <= 0)
            return null;
        var activeEnemies = new List<Transform>(EnemiesInRange);
        foreach (var enemy in EnemiesInRange)
        {
            if (!enemy.gameObject.activeSelf)
                activeEnemies.Remove(enemy);
        }
        var newTarget = activeEnemies[Random.Range(0, activeEnemies.Count)];
        
        if (newTarget == t)
        {
            if(activeEnemies.Count == 1)
                return null;
            
            activeEnemies.Remove(t);
            if (activeEnemies.Count == 1)
                return activeEnemies[0];
            newTarget = activeEnemies[Random.Range(0, activeEnemies.Count)];
        }


        return newTarget;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger)
            return;
        EnemyBase enemy = null;
        if (!other.TryGetComponent(out enemy)) return;
        EnemiesInRange.Add(enemy.transform);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyBase enemy = null;
        other.TryGetComponent(out enemy);
        if (enemy != null)
        {
            EnemiesInRange.Remove(enemy.transform);
        }
    } 
}
