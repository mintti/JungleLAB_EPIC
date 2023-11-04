using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSkillInfo : MonoBehaviour
{
    [Header("Casting Component")]
    [SerializeField] private Transform _castingGaugeParentTr;
    [SerializeField] private TextMeshProUGUI magicCircleCntTMP;
    [SerializeField] private Color _castingGaugeOnColor;
    [SerializeField] private Color _castingGaugeOffColor;
    
    [Header("Skill Component")]
    [SerializeField] private Transform _playerSkillTr;
    [SerializeField] private GameObject _learnedSkillPrefab;
    private List<UISkillInfo> _learnedSkills;
    
    /// <summary>
    /// 새로 배우는 스킬 정보를 받아 화면에 표시합니다.
    /// </summary>
    public void LearnSkill(Skill learningSkill)
    {
        _learnedSkills ??= new();

        var uiSkill = _learnedSkills.FirstOrDefault(x => x.BaseSkill.Data.SkillType == learningSkill.Data.SkillType);
        if (uiSkill == null)
        {
            var obj = Instantiate(_learnedSkillPrefab, _playerSkillTr);
            var uiSkillInfo =  obj.GetComponent<UISkillInfo>();
            uiSkillInfo.Init(learningSkill);
            
            _learnedSkills.Add(uiSkillInfo);
        }
        else
        {
            uiSkill.UpdateInfo();
        }
    }

    #region Casting-Related
    public void UpdateCastingGauge(int value, int maxValue)
    {
        for (int i = 0; i < maxValue ; i++)
        {
            var color = i < value ? _castingGaugeOnColor : _castingGaugeOffColor;
            _castingGaugeParentTr.GetChild(i).GetComponent<Image>().color = color;
        }
    }

    public void UpdateMaxCastingCount(int value)
    {
        for (int i = 0, cnt = _castingGaugeParentTr.childCount; i < cnt ; i++)
        {
            bool isActive = i < value;
            _castingGaugeParentTr.GetChild(i).gameObject.SetActive(isActive);   
        }
    }
    
    public void UpdateMagicCircleCount(int value)
    {
        magicCircleCntTMP.text = $"{value}";
    }
    #endregion
    
}