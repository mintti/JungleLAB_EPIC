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

    public void UpdateInfo() // [TODO] 남은 도박 횟수, 도박 결과(홀&짝), 성공 여부, 도박 단계 받기
    {
        gambleCountTMP.text = $"남은 횟수 {3}";
        gambleResultTMP.text = $"홀";
        gambleCardNumTMP.text = $"획득 개수 : 1 , 2 , 4 , 8"; //성공 시, 도박 단계에 초록색 입히기
        //gambleCardNumTMP.text = $"실패";

    }
}
