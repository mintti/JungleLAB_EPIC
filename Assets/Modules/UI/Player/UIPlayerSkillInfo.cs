using System.Collections.Generic;
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
    private List<SkillInfo> _learnedSkills;
    
    public int MaxCastingCount => 5;
    private void UpdateAfterCasting() // 캐스팅 정보 받기
    {
        // [TODO] 게이지 정보 업데이트
        magicCircleCntTMP.text = $"NN"; // [TODO] 마법진 갯수 업데이트
    }

    public void AddSkillInfo(SkillInfo skill)
    {
        _learnedSkills ??= new();
        
        // [TODO] 이미 배운 스킬인지 아닌지에 따라, 화면에 추가 생성
        _learnedSkills.Add(skill);
        
    }
}