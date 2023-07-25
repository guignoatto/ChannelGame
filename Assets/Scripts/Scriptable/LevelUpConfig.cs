using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpConfig", menuName = "Experience/LevelUpConfig")]
public class LevelUpConfig : ScriptableObject
{
    [SerializeField]
    private List<int> _xpToLevelUp;

    public List<int> XpToLevelUp { get { return _xpToLevelUp; } }
}
