using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAltarInfo : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI altarCountTMP;


    public void UpdateInfo() // [TODO] ���� Ƚ�� ������ �ޱ� �ʿ�
    {
        altarCountTMP.text = $"���� Ƚ�� {2}";
    }
}
