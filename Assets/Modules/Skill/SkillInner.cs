
using System;
using UnityEngine;

/// <summary>
/// 스킬에서 동적으로 관리가 필요한 요소들을 포함합니다.
/// </summary>
public abstract class SkillInner : MonoBehaviour
{
    public bool IsLearn { get; set; }
    private SkillData _baseSkill;

    public void Init(SkillData baseSkill)
    {
        IsLearn = false;
        _baseSkill = baseSkill;
    }

    private int _level = 1;

    public int Level
    {
        get => _level;
        set
        {
            _level = Math.Min(value, _baseSkill.MaxLevel);
        }
    }

    public abstract void Execute();

    public void LevelUp()
    {
        Level++;
    }
}