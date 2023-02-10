using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Waves/Wave Config")]
public class WaveConfig : ScriptableObject
{
    public List<SingleWaveConfig> SingleWaveConfig;
}

[Serializable]
public struct SingleWaveConfig
{
    public string FontName;
    public List<EnemyConfig> EnemyConfig;
}



