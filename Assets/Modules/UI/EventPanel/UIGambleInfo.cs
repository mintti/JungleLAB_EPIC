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
        // �ʱ�ȭ
        gambleResultTMP.text = "-";
        _resultPanel.SetActive(false);
        gameObject.SetActive(false);

        GameManager.Card.RequestCard(
            "������",
            "ī�带 �ɰ� �����ϰڽ��ϱ�?\n"+
            "���� �� ī�尡 �ı��˴ϴ�.",
            (type, value) => {
                _resultPanel.SetActive(false);
                gameObject.SetActive(true);

                StartCoroutine(GambleFlow(type, value));
            },
            CardAfterUse.Remove,
            CardUseRestriction.JustOne,
            true,
            () => {
                GameManager.Log.Log("�������� �̿����� ����");
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

            gambleCardNumTMP.text = $"����: {curNum} / ���� ��: {ifSuccessNum}";
            gambleCountTMP.text = $"���� Ƚ��: {3 - SucessCount}";

            yield return WaitSelect();

            if (_isCancel) break;
            else // Ȧ¦ ���� �Ǻ�
            {
                bool odd = Random.Range(0f, 1f) < .5f;
                gambleResultTMP.text = odd ? "Ȧ" : "¦";
                bool result = _isOdd == odd;
            
                GameManager.Log.Log($"Ȧ¦ ���: {result}");
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

        // ����
        // [TODO] ī�� ���� �� ���� �۾�
        
        var copyCnt = SucessCount switch
        { // ���� ���� ����
            0 => 1,
            1 => 2,
            2 => 4,
            3 => 8,
            _ => -1
        };
        
        // ���
        _resultPanel.SetActive(true);
        _resultTMP.text = SucessCount == -1 ? "ī�� �ı�" : $"ī�尡 {copyCnt -1}��ŭ ������";
        
        if (copyCnt >= 0)
        {
            // [TODO] ī�� ����
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
            GameManager.Log.Log("�ƹ��͵� �����ʾҴ�.");
        }
        
        yield return End();
    }

    
    IEnumerator End()
    {
        GameManager.Log.Log("�̿� ����");
        yield return new WaitForSeconds(1);
        GameManager.Player.ShowCardPanels();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �÷��̾��� ����(�ൿ)�� ���
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
