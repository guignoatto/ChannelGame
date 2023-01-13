using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillPickManager : MonoBehaviour
{
    public Action<SkillBase> NewSkillInstantiated;

    [SerializeField] private ProjectilePool _projectilePool;
    [SerializeField] private SkillsList skillsList;
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
        _projectilePool.Initialize();
    }

    private void InstantiateNewSkill(ISkillType skillType)
    {
        var skillPreset = GetSkillType(skillType);
        var skill = Instantiate(skillPreset.SkillPrefab, _player.transform).GetComponent<SkillBase>();
        skill._skillPreset = skillPreset;
        skill.GetProjectile = _projectilePool.GetProjectile;
        skill.SetEnemyDetectionField(_player.enemyDetectionField);
        skill.Initialize();
        NewSkillInstantiated.Invoke(skill);
    }

    private SkillPreset GetSkillType(ISkillType skillType)
    {
        foreach (var skill in skillsList.Skills)
        {
            if (skill.SkillType == skillType)
            {
                return skill;
            }
        }

        return null;
    }

    private List<SkillPreset> RandomizeSkills()
    {
        var skill1 = skillsList.Skills[Random.Range(0, skillsList.Skills.Count)];
        var skill2 = skillsList.Skills[Random.Range(0, skillsList.Skills.Count)];
        var skill3 = skillsList.Skills[Random.Range(0, skillsList.Skills.Count)];

        var skillSaber = new List<SkillPreset>()
            { skill1, skill2, skill3 }; //test
        return skillSaber;
    }
}