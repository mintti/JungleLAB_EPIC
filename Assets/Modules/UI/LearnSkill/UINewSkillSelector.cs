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
    
    private bool _hasSkillSelected = false;

    [Button]
    public IEnumerator ActiveNewSkillSelector()
    {
        gameObject.SetActive(true);

        List<int> pickNumbers = new();
        int pickNumbersCount=0;

        foreach (SkillData s in _skillDatas)
        {
            if(GameManager.Player.Ability<PlayerMagic>().GetSkill(s.SkillType).Inner.Level
                != s.MaxLevel)
            {
                pickNumbersCount++;
            }

            if (pickNumbersCount == ShowSkillCount)
                break;
        }

        while (pickNumbers.Count < pickNumbersCount)
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
        
        for(int i = 0; i < ShowSkillCount; i++)
        {
            _newSkillTr.GetChild(i).gameObject.SetActive(false);

        }

        for (int i = 0; i < ShowSkillCount; i++)
        {

            _newSkillTr.GetChild(i).GetComponent<UISkillInfo>().Init(_skillDatas[pickNumbers[i]], true);
        }

        _hasSkillSelected = false;
        yield return new WaitUntil(() => _hasSkillSelected);
        gameObject.SetActive(false);
    }

    public void DisableSelector()
    {
        _hasSkillSelected = true;
    }
}
