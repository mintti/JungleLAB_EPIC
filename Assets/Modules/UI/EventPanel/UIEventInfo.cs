using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIEventInfo : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI eventInfoTMP;

    public void UpdateInfo() // [TODO] ����Ʈ Ÿ�� �ޱ� �ʿ�.
    {
        eventInfoTMP.text = $"�п� �ִ� ���� ī�� �ϳ��� ���ڸ� 2�� ���ݴϴ�.";
    }
}
