using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

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


        for (int i = 0; i < ShowSkillCount; i++)
        {
            _newSkillTr.GetChild(i).GetComponent<UISkillInfo>().Init(_skillDatas[i]);
        }
        
    }


    public void DisableSelector()
    {
        gameObject.SetActive(false);
    }
}
