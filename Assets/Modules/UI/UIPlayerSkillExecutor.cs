using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 이벤트가 생성될 때, 자동으로 해당 함수를 연결합니다.
/// </summary>
public class UIPlayerSkillExecutor : MonoBehaviour, IUIButtonExecutor
{
    private SkillData BaseSkill => GetComponent<UISkillInfo>().BaseSkill; 
    private Action _action;

    private Action Action
    {
        get
        {
            _action ??= BaseSkill.Inner.Execute;
            return _action;
        }
    }

    public void B_Execute()
    {
        if (BaseSkill.Cost < 999) // [TODO] Check Magic Circle Count
        {
            Action();
        }
    }
}
