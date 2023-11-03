using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGambleInfo : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI gambleCountTMP;
    [SerializeField] private TextMeshProUGUI gambleResultTMP;
    [SerializeField] private TextMeshProUGUI gambleCardNumTMP;
    [SerializeField] private GameObject _cover;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TextMeshProUGUI _resultTMP;

    [Header("Data")]
    [SerializeField] private bool _isSelect;
    [SerializeField] private bool _isOdd;
    [SerializeField] public bool _isCancel;
    
    [SerializeField] private int _sucessCount;
    public int SucessCount
    {
        get => _sucessCount;
        set
        {
            _sucessCount = value;
            
        }
    }
    
    [Button]
    public void OnTileEvent()
    {
        // 초기화
        gambleResultTMP.text = "-";
        _resultPanel.SetActive(false);
        _cover.SetActive(true);
        gameObject.SetActive(true);
        
        StartCoroutine(GambleFlow());
    }
    public void B_SelectOdd(bool isOdd)
    {
        _isOdd = isOdd;
        _isSelect = true;
    }

    public void B_Cancel()
    {
        _isCancel = true;
        _isSelect = true;
    }


    private Card _selectedCard;
    public void B_SelectCard()
    {
        var card = UIManager.I.UIPlayerInfo.UICardInfo.SelectedCards.FirstOrDefault()?.Card;

        if (card != null)
        {
            _selectedCard = card;
            _isSelect = true;
            
            GameManager.Log.Log($"{_selectedCard.CardData.CardNumber}/{_selectedCard.CardData.CardType} 카드가 선택됨");
        }
        else
        {
            GameManager.Log.Log("선택된 카드가 존재하지 않습니다.");
        }
    }
    
    IEnumerator GambleFlow()
    {

        // 대상 카드 선택 or 이용 X
        yield return WaitSelect();
        if (_isCancel)
        {
            GameManager.Log.Log("도박장을 이용하지 않음");
            yield return End();
            yield return null;
        }
        
        // 배팅 시작
        _cover.SetActive(false);
        do
        {
            yield return WaitSelect();

            if (_isCancel) break;
            else // 홀짝 여부 판별
            {
                bool odd = Random.Range(0f, 1f) < .5f;
                gambleResultTMP.text = odd ? "홀" : "짝";
                bool result = _isOdd == odd;
            
                GameManager.Log.Log($"홀짝 결과: {result}");
                yield return new WaitForSeconds(.5f);
                if (result)
                {
                    SucessCount++;
                }
                else
                {
                    SucessCount = -1;
                    break;
                }
            }
        } while (SucessCount < 3);

        // 제거
        // [TODO] 카드 제거 후 이후 작업
        
        var copyCnt = SucessCount switch
        { // 복제 갯수 지정
            0 => 1,
            1 => 2,
            2 => 4,
            3 => 8,
            _ => -1
        };
        
        // 결과
        _resultPanel.SetActive(true);
        _resultTMP.text = SucessCount == -1 ? "카드 파괴" : $"카드가 {copyCnt -1}만큼 복제됨";
        
        if (copyCnt >= 0)
        {
            // [TODO] 카피 수행
            // GameManager.Card.
        }
        else
        {
            GameManager.Log.Log("아무것도 남지않았다.");
        }
        
        yield return End();
    }

    
    IEnumerator End()
    {
        GameManager.Log.Log("이용 종료");
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 플레이어의 선택(행동)을 대기
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitSelect()
    {
        _isOdd = false;
        _isCancel = false;
        _isSelect = false;
        yield return new WaitUntil(() => _isSelect);
    }
}
