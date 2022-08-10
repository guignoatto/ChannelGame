using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillPickManager : MonoBehaviour
{
    [SerializeField] private SkillsPrefabs _skillsPrefabs;
    [SerializeField] private PlayerMovement _player;

    private SkillView _skillView;
    
    public void OnLevelUpHandler()
    {
        _skillView.OnLevelUpHandler(RandomizeSkills());
    }

    public void Initialize()
    {
        _skillView = GetComponent<SkillView>();
        _skillView.SkillClickedEvent += InstantiateNewSkill;
    }
    private void InstantiateNewSkill(ISkillType skillType)
    {
        var skill = GetSkillType(skillType);
        Instantiate(skill, _player.transform);
    }

    private GameObject GetSkillType(ISkillType skillType)
    {
        switch (skillType)
        {
            case ISkillType.LIGHTSABER:
                return _skillsPrefabs.LightSaber;
            default:
                return null;
        }
    }
    
    private List<ISkillType> RandomizeSkills()
    {
        var skillSaber = new List<ISkillType>() { ISkillType.LIGHTSABER, ISkillType.LIGHTSABER, ISkillType.LIGHTSABER }; //test
        return skillSaber;
    }
}