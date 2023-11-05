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
        if (BaseSkill.Data.Cost < 999) // [TODO] Check Magic Circle Count
        {
            Action();
            // Action()의 반환값이 true일때 코스트 깎기
        }
    }
}
