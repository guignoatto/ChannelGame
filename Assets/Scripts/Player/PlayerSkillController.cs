using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    private List<SkillBase> _playerSkills;

    public void AddNewSkill(SkillBase newSkill)
    {
        _playerSkills.Add(newSkill);
    }
}
