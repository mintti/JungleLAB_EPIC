using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        state2.patterns.Add(pattern8);
        state2.patterns.Add(pattern9);
        state2.patterns.Add(pattern10);
        state2.patterns.Add(pattern11);

        bossStates.Add(state3);
    }

    public Pattern GetCurrentPattern()
    {
        var pattern = bossStates[_currentState].patterns[_currentPatternIndex];;
        return pattern;
    }
    
    public void BossTurn()
    {
        _currentDefense = 0;

        var pattern = GetCurrentPattern();
        PatternType _paternType = pattern.type;
        int _value = pattern.value;

        if (_paternType == PatternType.FireMagic)
        {
            for(int i = 0; i < _value; i++)
            {
                int _ranValue1;
                int _ranValue2;
                int _tileIndex;

                do
                {
                    _ranValue1 = Random.Range(1, 5);
                    _ranValue2 = Random.Range(1, 4);
                    _tileIndex = (_ranValue1 - 1) * 4 + _ranValue2;
                } while (BoardManager.I.tiles[_tileIndex].IsCurse);

                BoardManager.I.tiles[_tileIndex].OnCurse(2);
                // ¸¶¹ıÁøÀÌ 2ÅÏµ¿¾È À¯Áö.
            }
        }
        else if (_paternType == PatternType.Attack)
        {
            //À¯Àú Å¸°ÙÆÃ °ø°İ
        }
        else if (_paternType == PatternType.Defense)
        {
            _currentDefense += _value;
            //º¸½º ¹æ¾î È¹µæ
        }
        else if (_paternType == PatternType.FireBreath) 
        {
            int _ranValue = Random.Range(1, 5);
            int _startIndex = (_ranValue - 1) * 4;

            for (int i = _startIndex; i < _startIndex + 5; i++)
            {
                BoardManager.I.tiles[i].debuff = new FireBreath(1, 2);
                //1ÀÇ µ¥¹ÌÁö¸¦ ÁÖ´Â Å¸ÀÏÀÌ 2ÅÏµ¿¾È À¯Áö
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
    }

    public void HpUpdate(int _dmg)
    {
        int _afterHp = _currentHp - _dmg;
        if (_currentHp > maxHp * 0.7 && _afterHp <= maxHp * 0.7)
        {
            _currentState++;
            _currentPatternIndex = 0;
            //1Â÷ ±¤ÆøÈ­
        }

        if (_currentHp > maxHp * 0.3 && _afterHp <= maxHp * 0.3)
        {
            _currentState++;
            _currentPatternIndex = 0;
            //2Â÷ ±¤ÆøÈ­
        }

    }

}
