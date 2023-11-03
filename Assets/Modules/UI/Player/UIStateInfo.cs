using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TH.Core.TMP.TMPUtil;

public class UIStateInfo : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI testInfoTMP;

    public void UpdateInfo() // [TODO] 데이터 받기 필요
    {
        testInfoTMP.text = $"체력:{1000}/{1000}\n" +
                           $"방어:{1000}\n" +
                           $"현재 [행동]".ToTMP();
    }
}
