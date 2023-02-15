using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillsList", menuName = "Skills/SkillsList")]
public class AllSkillsList : ScriptableObject
{
    public List<SkillPreset> Skills;
}
