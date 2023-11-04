using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyInfo : MonoBehaviour
{
    [Header("Enemy Info")]
    [SerializeField] private TextMeshProUGUI _hpTMP;
    [SerializeField] private RectTransform _maxHPRect;
    [SerializeField] private RectTransform _curHPRect;
    [SerializeField] private Image _stateImg;

    private float _defaultHPWidthSize;
    
    
    [Header("Action Info")]
    [SerializeField] private Image _actionImg;
    [SerializeField] private TextMeshProUGUI _actionValueTMP;


    public void Start()
    {
        _defaultHPWidthSize = _maxHPRect.sizeDelta.x + 10;
    }

    public void Active(string name, int hp)
    {
        gameObject.SetActive(true);
        UpdateHP(hp, hp);
    }

    public void UpdateHP(int hp, int maxHp)
    {
        
        _hpTMP.text = $"{hp}/{maxHp}";
        
        _maxHPRect.sizeDelta = new Vector2(_defaultHPWidthSize, _maxHPRect.sizeDelta.y);
        _curHPRect.sizeDelta = new Vector2((_defaultHPWidthSize - 10) * hp / maxHp, _curHPRect.sizeDelta.y);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void UpdateAction(Sprite sprite, int value = 0)
    {
        _actionImg.sprite = sprite;
        _actionValueTMP.gameObject.SetActive(value != 0);
        _actionValueTMP.text = $"{value}";
    }


    public void UpdateState(Sprite sprite)
    {
        _stateImg.sprite = sprite;
    }
}
