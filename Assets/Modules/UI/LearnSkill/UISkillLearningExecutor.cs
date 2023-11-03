using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;

public class UISkillLearningExecutor : MonoBehaviour ,IUIButtonExecutor
{
    public void B_Execute()
    {
        var skill = GetComponent<UISkillInfo>().BaseSkill;
        GameManager.Player.Ability<PlayerMagic>().LearnSkill(skill);

        UIManager.I.UINewSkillSelector.DisableSelector();
    }
}
