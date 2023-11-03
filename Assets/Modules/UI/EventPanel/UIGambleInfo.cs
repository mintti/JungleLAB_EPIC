using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGambleInfo : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI gambleCountTMP;
    [SerializeField] private TextMeshProUGUI gambleResultTMP;
    [SerializeField] private TextMeshProUGUI gambleCardNumTMP;

    public void UpdateInfo() // [TODO] ���� ���� Ƚ��, ���� ���(Ȧ&¦), ���� ����, ���� �ܰ� �ޱ�
    {
        gambleCountTMP.text = $"���� Ƚ�� {3}";
        gambleResultTMP.text = $"Ȧ";
        gambleCardNumTMP.text = $"ȹ�� ���� : 1 , 2 , 4 , 8"; //���� ��, ���� �ܰ迡 �ʷϻ� ������
        //gambleCardNumTMP.text = $"����";

    }
}
