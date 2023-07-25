using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillPickManager : MonoBehaviour
{
    public Action<SkillBase> NewSkillInstantiated;

    [SerializeField] private ProjectilePool _projectilePool;
    [SerializeField] private AllSkillsList _allSkillsList;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private int maxSkills;
    [SerializeField] private int maxPassive;

    private List<SkillBase> mySkills;

    private SkillView _skillView;

    public void OnLevelUpHandler()
    {
        _skillView.OnLevelUpHandler(RandomizeSkills());
    }

    public void Initialize()
    {
        mySkills = new List<SkillBase>();
        
        _skillView = GetComponent<SkillView>();
        _skillView.SkillClickedEvent += InstantiateNewSkill;
       
        _projectilePool.Initialize();
    }

    private void InstantiateNewSkill(ISkillType skillType)
    {
        foreach (var s in mySkills.Where(s => s.SkillPreset.SkillType == skillType))
        {
            s.LevelUp();
            return;
        }
        var skillPreset = GetSkillType(skillType);
        var skill = Instantiate(skillPreset.SkillPrefab, _player.transform).GetComponent<SkillBase>();
        skill.SkillPreset = skillPreset;
        skill.GetProjectile += _projectilePool.GetProjectile;
        skill.SetEnemyDetectionField(_player.enemyDetectionField);
        skill.Initialize();
        mySkills.Add(skill);
        NewSkillInstantiated.Invoke(skill);
    }

    private SkillPreset GetSkillType(ISkillType skillType)
    {
        foreach (var skill in _allSkillsList.Skills)
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
        SkillPreset skill1, skill2, skill3;
        if (mySkills.Count < maxSkills)
        {
            skill1 = _allSkillsList.Skills[Random.Range(0, _allSkillsList.Skills.Count)];
            skill2 = _allSkillsList.Skills[Random.Range(0, _allSkillsList.Skills.Count)];
            skill3 = _allSkillsList.Skills[Random.Range(0, _allSkillsList.Skills.Count)];
        }
        else
        {
            skill1 = mySkills[Random.Range(0, mySkills.Count)].SkillPreset;
            skill2 = mySkills[Random.Range(0, mySkills.Count)].SkillPreset;
            skill3 = mySkills[Random.Range(0, mySkills.Count)].SkillPreset;
        }

        var skillSaber = new List<SkillPreset>()
            { skill1, skill2, skill3 }; //test
        return skillSaber;
    }
}