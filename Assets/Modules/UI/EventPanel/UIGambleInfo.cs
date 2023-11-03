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
        // �ʱ�ȭ
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
            
            GameManager.Log.Log($"{_selectedCard.CardData.CardNumber}/{_selectedCard.CardData.CardType} ī�尡 ���õ�");
        }
        else
        {
            GameManager.Log.Log("���õ� ī�尡 �������� �ʽ��ϴ�.");
        }
    }
    
    IEnumerator GambleFlow()
    {

        // ��� ī�� ���� or �̿� X
        yield return WaitSelect();
        if (_isCancel)
        {
            GameManager.Log.Log("�������� �̿����� ����");
            yield return End();
            yield return null;
        }
        
        // ���� ����
        _cover.SetActive(false);
        do
        {
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
            // GameManager.Card.
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
