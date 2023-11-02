using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINewSkillSelector : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameObject _skillInfoPrefab;
    
    public int ShowSkillCount => 3;
    public List<UISkillInfo> SkillInfos { get; private set; }

    public void ActiveNewSkillSelector()
    {
        // [TODO] 표시 할 스킬 겟
        gameObject.SetActive(true);
    }
}
