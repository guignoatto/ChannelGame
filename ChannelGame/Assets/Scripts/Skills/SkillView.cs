using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SkillView : MonoBehaviour
{
    [SerializeField] private GameObject _skillPickCanvas;
    [SerializeField] private List<ButtonSkill> _buttonSkillsList;

    public Action<ISkillType> SkillClickedEvent; 
    
    public void GetSkillClicked(ButtonSkill skillType)
    {
        SkillClickedEvent?.Invoke(skillType.SkillType);
        _skillPickCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnLevelUpHandler(List<SkillPreset> skillPresets)
    {
        _skillPickCanvas.SetActive(true);
        Time.timeScale = 0;
        GetSkillForButtons(skillPresets);
    }

    public void GetSkillForButtons(List<SkillPreset> skillPresets)
    {
        for (int i = 0; i < _buttonSkillsList.Count; i++)
        {
            //todo change button view
            var randomSkill = Random.Range(0,skillPresets.Count);
            _buttonSkillsList[i].SkillType = skillPresets[randomSkill].SkillType;
            _buttonSkillsList[i].SkillText.text = skillPresets[randomSkill].SkillName;
            _buttonSkillsList[i].SkillSprite.sprite = skillPresets[randomSkill].skillPickSprite;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1;
            _skillPickCanvas.SetActive(false);
        }
    }
}