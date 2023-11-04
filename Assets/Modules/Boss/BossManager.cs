using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Mono.CSharp;

public class BossManager : MonoBehaviour
{
    public int maxHp;
    public int _currentHp;
    private int _currentState;
    private int _currentPatternIndex;
    private int _currentDefense;
    public List<BossState> bossStates = new List<BossState>();

    [Header("ï¿½ï¿½ï¿½Ï¼ï¿½Ä¡ï¿½ï¿½")]
    public int normalMagic;
    public int normalAttack;
    public int normalDefense;

    public int angryMagic;
    public int angryAttack;
    public int angryBreath;
    public int angryDefense;

    public int enragedMagic;
    public int enragedAttack;
    public int enragedBreath;
    public int enragedDefense;


    [Header("Status Effect")]
    [SerializeField] private GameObject _weakIconObj;
    [SerializeField] private TextMeshProUGUI _weakTMP;
    private int _weakCount;
    private int WeakCount
    {
        get => _weakCount;
        set
        {
            _weakCount = value;
            _weakIconObj.SetActive(_weakCount > 0);
            _weakTMP.text = $"{_weakCount}";
        }
    }
    
    [SerializeField] private GameObject _brokenIconObj;
    [SerializeField] private TextMeshProUGUI _brokenTMP;
    private int _brokenCount;
    private int BrokenCount
    {
        get => _brokenCount;
        set
        {
            _brokenCount = value;
            _brokenIconObj.SetActive(_brokenCount > 0);
            _brokenTMP.text = $"{_brokenCount}";
        }
    }

    public void EventDebuffBoss(int broken, int weak)
    {
        WeakCount = weak;
        BrokenCount = broken;
    }

    public void Start()
    {
        _currentHp = maxHp;
        _currentState = 0;
        _currentPatternIndex = 0;

        Pattern pattern1 = new Pattern { type = PatternType.FireMagic, value = normalMagic };
        Pattern pattern2 = new Pattern { type = PatternType.Attack, value = normalAttack };
        Pattern pattern3 = new Pattern { type = PatternType.Defense, value = normalDefense };

        BossState state1 = new BossState { name = "Normal" };
        state1.patterns.Add(pattern1);
        state1.patterns.Add(pattern2);
        state1.patterns.Add(pattern3);

        bossStates.Add(state1);

        Pattern pattern4 = new Pattern { type = PatternType.FireMagic, value = angryMagic };
        Pattern pattern5 = new Pattern { type = PatternType.Attack, value = angryAttack };
        Pattern pattern6 = new Pattern { type = PatternType.FireBreath, value = angryBreath };
        Pattern pattern7 = new Pattern { type = PatternType.Defense, value = angryDefense };

        BossState state2 = new BossState { name = "Angry" };
        state2.patterns.Add(pattern4);
        state2.patterns.Add(pattern5);
        state2.patterns.Add(pattern6);
        state2.patterns.Add(pattern7);

        bossStates.Add(state2);

        Pattern pattern8 = new Pattern { type = PatternType.FireMagic, value = enragedMagic };
        Pattern pattern9 = new Pattern { type = PatternType.Attack, value = enragedAttack };
        Pattern pattern10 = new Pattern { type = PatternType.FireBreath, value = enragedBreath };
        Pattern pattern11 = new Pattern { type = PatternType.Defense, value = enragedDefense };

        BossState state3 = new BossState { name = "Enraged" };
        state3.patterns.Add(pattern8);
        state3.patterns.Add(pattern9);
        state3.patterns.Add(pattern10);
        state3.patterns.Add(pattern11);

        bossStates.Add(state3);
    }

    public Pattern GetCurrentPattern()
    {
        var pattern = bossStates[_currentState].patterns[_currentPatternIndex];
        return pattern;
    }
    
    public IEnumerator TurnEndEvent()
    {
        BrokenCount--;
        WeakCount--;

        yield return new WaitForSeconds(.5f);
    }
    
    public IEnumerator BossTurn()
    {
        _currentDefense = 0;

        var pattern = GetCurrentPattern();
        PatternType _paternType = pattern.type;
        int _value = pattern.value;

        if (_paternType == PatternType.FireMagic)
        {
            Debug.Log("Spawn MagicCircle");
            for (int i = 0; i < _value; i++)
            {
                int _ranValue1;
                int _ranValue2;
                int _tileIndex;

                do
                {
                    _ranValue1 = Random.Range(1, 5);
                    _ranValue2 = Random.Range(1, 4);
                    _tileIndex = (_ranValue1 - 1) * 4 + _ranValue2;
                } while (BoardManager.I.tiles[_tileIndex].IsCurse || _tileIndex == BoardManager.I.PlayerOnIndex);
                // ?„¤ì¹? ????¼?´ ???ì£¼ë°›ì§? ?•Š??? ?ƒ?ƒœ?—¬?•¼ ?•˜ê³?, ?œ ???ê°? ?„œ?žˆ?Š” ????¼?´ë©? ?•ˆ?¨.
                Debug.Log(_tileIndex);

                BoardManager.I.tiles[_tileIndex].OnCurse(3);
                // ë§ˆë²•ì§„ì´ 2?„´?™?•ˆ ?œ ì§?.
            }
        }
        else if (_paternType == PatternType.Attack)
        {
            Debug.Log("AttackPlayer");

            var value = WeakCount > 0 ? _value / 2 : _value;
            GameManager.Player.Hit(value); 
        }
        else if (_paternType == PatternType.Defense)
        {
            Debug.Log("GetDefense");
            _currentDefense += _value;
        }
        else if (_paternType == PatternType.FireBreath) 
        {
            Debug.Log("FireBreath");
            int _ranValue;
            int _startIndex;
            int pi = BoardManager.I.PlayerOnIndex;
            do
            {
                _ranValue = Random.Range(1, 5);
                _startIndex = (_ranValue - 1) * 4;
            } while (Enumerable.Range(_startIndex, 5).Contains(pi));

            for (int i = _startIndex; i < _startIndex + 5; i++)
            {
                FireBreath fb = new FireBreath(1, 2);
                BoardManager.I.tiles[i].AddDebuff(fb);
                //1?˜ ?°ë¯¸ì??ë¥? ì£¼ëŠ” ????¼?´ 2?„´?™?•ˆ ?œ ì§?
            }
        }

        if (_currentPatternIndex + 1 == bossStates[_currentState].patterns.Count)
        {
            _currentPatternIndex = 0;
        }
        else
        {
            _currentPatternIndex++;
        }
        //TestUpdateUI();

        yield return new WaitForSeconds(1f);
    }

    public void HpUpdate(int _dmg)
    {
        _dmg = _brokenCount > 0 ? (int)(_dmg * 1.5f) : _dmg;
        if (_currentDefense > 0)
        {
            _currentDefense -= _dmg;

            if (_currentDefense < 0)
            {
                _dmg = _currentDefense * -1;
            }
        }

        int _afterHp = _currentHp - _dmg;
        if (_currentHp > maxHp * 0.7 && _afterHp <= maxHp * 0.7)
        {
            _currentState++;
            _currentPatternIndex = 0;
            //1ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½È­
            Debug.Log("Angry1");
        }

        if (_currentHp > maxHp * 0.3 && _afterHp <= maxHp * 0.3)
        {
            _currentState++;
            _currentPatternIndex = 0;
            //2ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½È­
            Debug.Log("Angry2");
        }

        _currentHp = _afterHp;
        //TestUpdateUI();
    }

    public void TestHit()
    {
        HpUpdate(7); 
    }

    [Header("test")]
    public TextMeshProUGUI hp;
    public TextMeshProUGUI patternType;
    public TextMeshProUGUI bossState;
    public TextMeshProUGUI patternValue;
    public TextMeshProUGUI deffense;
    public void TestUpdateUI()
    {
        var pattern = GetCurrentPattern();
        PatternType _paternType = pattern.type;
        int _value = pattern.value;


        hp.text = $"HP : {_currentHp}/{maxHp}";
        if(_currentDefense > 0)
        {
            deffense.text = $"+{_currentDefense}";
        }
        else
        {
            deffense.text = $"";
        }

        if (_paternType == PatternType.FireMagic)
        {
            patternType.text = $"FireBall Magic";
            patternValue.text = $"";
        }
        else if (_paternType == PatternType.Attack)
        {
            patternType.text = $"Attack";
            patternValue.text = $"{_value}";
        }
        else if (_paternType == PatternType.Defense)
        {
            patternType.text = $"Deffense";
            patternValue.text = $"{_value}";
        }
        else
        {
            patternType.text = $"FireBreath";
            patternValue.text = $"";
        }

        if (_currentState == 0)
        {
            bossState.text = $"Normal";
        }
        else if (_currentState == 1)
        {
            bossState.text = $"Angry";
        }
        else
        {
            bossState.text = $"Very Angry";
        }


    }

}
