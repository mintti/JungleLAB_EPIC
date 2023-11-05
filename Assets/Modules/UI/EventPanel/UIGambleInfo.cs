using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TH.Core;

public class UIGambleInfo : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI gambleCountTMP;
    [SerializeField] private TextMeshProUGUI gambleResultTMP;
    [SerializeField] private TextMeshProUGUI gambleCardNumTMP;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TextMeshProUGUI _resultTMP;

    private ComponentGetter<TextMeshProUGUI> _cardNumberText 
        = new(TypeOfGetter.ChildByName, "TargetCard/CardNumberText");
    private ComponentGetter<Image> _cardImage 
        = new(TypeOfGetter.ChildByName, "TargetCard/CardImage");

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
        gameObject.SetActive(false);

        GameManager.Card.RequestCard(
            "도박장",
            "카드를 걸고 참여하겠습니까?\n"+
            "실패 시 카드가 파괴됩니다.",
            (type, value) => {
                _resultPanel.SetActive(false);
                gameObject.SetActive(true);

                StartCoroutine(GambleFlow(type, value));
            },
            CardAfterUse.Remove,
            CardUseRestriction.JustOne,
            true,
            () => {
                GameManager.Log.Log("도박장을 이용하지 않음");
                GameManager.Player.ShowCardPanels();
                gameObject.SetActive(false);
            },
            CardRequestPosition.Left
        );
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
    
    IEnumerator GambleFlow(Card.Type type, int value)
    {
        _cardNumberText.Get(gameObject).text = value.ToString();
		_cardImage.Get(gameObject).sprite = CardManager.GetCardEmblem(type);

        SucessCount = 0;
        do
        {
            int curNum = (int)Mathf.Pow(2, SucessCount);
            int ifSuccessNum = (int)Mathf.Pow(2, SucessCount + 1);

            gambleCardNumTMP.text = $"현재: {curNum} / 성공 시: {ifSuccessNum}";
            gambleCountTMP.text = $"남은 횟수: {3 - SucessCount}";

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
            // GameManager.Card
            for (int i = 0; i < copyCnt; i++)
            {
                Card newCard = new Card(new CardData(value, type), i == 0 ? false : true);
                GameManager.Card.CardDeck.AddCard(newCard);
                GameManager.Card.CardDeck.PutCardIntoHand(newCard);
            }
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
        GameManager.Player.ShowCardPanels();
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
