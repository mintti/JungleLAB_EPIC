using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TH.Core;

public class UINewSkillSelector : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameObject _newSkillInfoPrefab;
    [SerializeField] private List<UISkillInfo> _newSkillInfos;
    [SerializeField] private Transform _newSkillTr;
    
    [SerializeField] private SkillData[] _skillDatas;
    
    public int ShowSkillCount => 3;
    

    [Button]
    public void ActiveNewSkillSelector()
    {
        gameObject.SetActive(true);
        // [TODO] 표시 할 스킬 겟
        // 아직 안 배운 스킬 + 배운 스킬 중 맥스레벨에 도달하지않은 스킬 중 랜덤 3개 봅아서 아래에 등록해주기 
        List<int> pickNumbers = new();
        while (pickNumbers.Count < ShowSkillCount)
        {
            int rand = Random.Range(0, _skillDatas.Length);
            if (pickNumbers.Contains(rand))
                continue;

            if (GameManager.Player.Ability<PlayerMagic>().GetSkill(_skillDatas[rand].SkillType)!=null&&
                _skillDatas[rand].MaxLevel 
                == GameManager.Player.Ability<PlayerMagic>().GetSkill(_skillDatas[rand].SkillType).Inner.Level)
                continue;
            pickNumbers.Add(rand);

        }
        
        // [TODO] 만약, 배울 마법이 존재하지 않는 경우, 칸을 감소하여 출력 시킬 것

        Debug.Log(pickNumbers.Count);

        for (int i = 0; i < ShowSkillCount; i++)
        {

            _newSkillTr.GetChild(i).GetComponent<UISkillInfo>().Init(_skillDatas[pickNumbers[i]], true);
        }
    }

    public void DisableSelector()
    {
        gameObject.SetActive(false);
    }
}
