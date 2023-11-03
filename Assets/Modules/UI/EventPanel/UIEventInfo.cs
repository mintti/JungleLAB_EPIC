using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIEventInfo : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI eventInfoTMP;

    public void UpdateInfo() // [TODO] 퀘스트 타입 받기 필요.
    {
        eventInfoTMP.text = $"패에 있는 숫자 카드 하나의 숫자를 2배 해줍니다.";
    }
}
