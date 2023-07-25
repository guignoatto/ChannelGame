using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
    }
}
