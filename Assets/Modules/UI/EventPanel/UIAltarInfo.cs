using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAltarInfo : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI altarCountTMP;


    public void UpdateInfo() // [TODO] 남은 횟수 데이터 받기 필요
    {
        altarCountTMP.text = $"남은 횟수 {2}";
    }
}
