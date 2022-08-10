using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillView : MonoBehaviour
{
    [SerializeField] private GameObject _skillPickCanvas;

    [SerializeField] private Button _buttonSkill1;
    [SerializeField] private Button _buttonSkill2;
    [SerializeField] private Button _buttonSkill3;
    
    public Action<ISkillType> SkillClickedEvent; 
    
    public void GetSkillClicked(ISkillType skillType)
    {
        SkillClickedEvent?.Invoke(skillType);
    }

    public void OnLevelUpHandler(List<ISkillType> skillTypes)
    {
        _skillPickCanvas.SetActive(true);
        GetSkillForButtons(skillTypes);
    }

    public void GetSkillForButtons(List<ISkillType> skillType)
    {
        foreach (var type in skillType)
        {
            ChangeButton(type);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _skillPickCanvas.SetActive(false);
        }
    }

    private void ChangeButton(ISkillType skillType)
    {
        //change buttons style + add ISkillType
    }
}