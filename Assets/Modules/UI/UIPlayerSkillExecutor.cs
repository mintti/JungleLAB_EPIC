using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 이벤트가 생성될 때, 자동으로 해당 함수를 연결합니다.
/// </summary>
public class UIPlayerSkillExecutor : MonoBehaviour, IUIButtonExecutor
{
    private Skill BaseSkill => GetComponent<UISkillInfo>().BaseSkill; 
    private Func<bool> _action;

    private Func<bool> Action
    {
        get
        {
            _action ??= BaseSkill.Inner.Execute;
            return _action;
        }
    }

    public void B_Execute()
    {
        int needCost = BaseSkill.Data.Cost; 
        if ( needCost <= GameManager.Player.PlayerMagic.MagicCircleCount) // [TODO] Check Magic Circle Count
        {
            if (Action())
            {
                GameManager.Player.PlayerMagic.MagicCircleCount -= needCost;
            }
        }
    }
}
