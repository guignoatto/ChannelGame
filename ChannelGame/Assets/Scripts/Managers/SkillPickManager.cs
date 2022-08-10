using System;
using UnityEngine;
public class SkillPickManager : MonoBehaviour
{
    [SerializeField] private SkillsPrefabs _skillsPrefabs;
    [SerializeField] private PlayerMovement _player;

    private void Start()
    {
        _player.GetWeaponTypeEvent += InstantiateNewSkill;
    }

    private void InstantiateNewSkill(Enum skillType)
    {
        var skill = GetSkillType(skillType);
        Instantiate(skill);
    }

    private GameObject GetSkillType(Enum skillType)
    {
        switch (skillType)
        {
            case ISkillType.LIGHTSABER:
                return _skillsPrefabs.LightSaber;
            default:
                return null;
        }
    }
}