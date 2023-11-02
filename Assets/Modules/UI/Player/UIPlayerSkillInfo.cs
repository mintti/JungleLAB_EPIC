using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIPlayerSkillInfo : MonoBehaviour
{
    [Header("Casting Component")]
    [SerializeField] private Transform _castingGaugeParentTr;
    [SerializeField] private TextMeshProUGUI magicCircleCntTMP;
    
    [Header("Skill Component")]
    [SerializeField] private Transform _playerSkillTr;
    [SerializeField] private GameObject _learnedSkillPrefab;
    private List<UISkillInfo> _learnedSkills;
    
    public int MaxCastingCount => 5;
    private void UpdateAfterCasting() // 캐스팅 정보 받기
    {
        // [TODO] 게이지 정보 업데이트
        magicCircleCntTMP.text = $"NN"; // [TODO] 마법진 갯수 업데이트
    }

    /// <summary>
    /// 새로 배우는 스킬 정보를 받아 화면에 표시합니다.
    /// [TODO] 배운 스킬 등록 방법에 따라 엎을 수 있음
    /// </summary>
    public void LearnSkill(SkillData learningSkill)
    {
        _learnedSkills ??= new();

        var skill = _learnedSkills.FirstOrDefault(x => x.BaseSkill.SkillType == learningSkill.SkillType);
        if (skill == null)
        {
            var obj = Instantiate(_learnedSkillPrefab, _playerSkillTr);
            var uiSkillInfo =  obj.GetComponent<UISkillInfo>();
            uiSkillInfo.Init(learningSkill);
            
            _learnedSkills.Add(uiSkillInfo);
        }
        else
        {
            skill.BaseSkill.Inner.LevelUp();
        }
    }
}