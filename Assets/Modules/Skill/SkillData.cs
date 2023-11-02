using System;
using Modules.Skill.Skills;
using UnityEngine;

/// <summary>
/// 스킬 정보를 담는 스크립터블 오브젝트
/// </summary>
[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = 0)]
public class SkillData : ScriptableObject
{
    [SerializeField] private SkillType _skillType;
    public SkillType SkillType => _skillType;
    
    [SerializeField] private string _name;
    public string Name => _name;
    
    [SerializeField] private int _maxLevel;
    public int MaxLevel => _maxLevel;	
    
    [SerializeField] private int _cost;
    public int Cost => _cost;
    
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;
    
    [SerializeField] private string _description;
    public string Description => _description;

    private SkillInner _inner;
    public SkillInner Inner
    {
        get
        {
            _inner ??= _skillType switch
            {
                SkillType.Defense => new Defense(),
                SkillType.Attack => new Attack(),
                SkillType.Draw => new Draw(),
                SkillType.Move => new Move(),
                SkillType.Spawn => new Spawn(),
                SkillType.Install => new Install(),
                SkillType.Heal => new Heal(),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            return _inner;
        }
        set => _inner = value;
    }
}