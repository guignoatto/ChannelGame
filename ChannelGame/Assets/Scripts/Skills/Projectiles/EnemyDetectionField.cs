using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionField : MonoBehaviour
{
    
    public List<EnemyBase> EnemiesInRange = new List<EnemyBase>();


    public Transform GetRandomEnemyPosition(Transform t)
    {
        if (EnemiesInRange.Count <= 0)
            return null;
        
        var newTarget = EnemiesInRange[Random.Range(0, EnemiesInRange.Count)].GetComponent<Transform>();
        if (newTarget == t)
        {
            if(EnemiesInRange.Count == 1)
                return null;
            
            var newList = EnemiesInRange.GetRange(0, EnemiesInRange.Count);
            newList.Remove(t.gameObject.GetComponent<EnemyBase>());
            if (newList.Count == 1)
                return newList[0].GetComponent<Transform>();
            newTarget = newList[Random.Range(0, newList.Count)].GetComponent<Transform>();
        }


        return newTarget;
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
