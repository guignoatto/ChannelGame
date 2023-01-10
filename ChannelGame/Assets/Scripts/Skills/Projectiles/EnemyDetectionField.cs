using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionField : MonoBehaviour
{
    
    public List<EnemyBase> EnemiesInRange = new List<EnemyBase>();


    public Transform GetRandomEnemyPosition()
    {
        return EnemiesInRange[Random.Range(0, EnemiesInRange.Count)].GetComponent<Transform>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = null;
        other.TryGetComponent(out enemy);
        if (enemy != null)
        {
            EnemiesInRange.Add(enemy);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyBase enemy = null;
        other.TryGetComponent(out enemy);
        if (enemy != null)
        {
            EnemiesInRange.Remove(enemy);
        }
    } 
}
