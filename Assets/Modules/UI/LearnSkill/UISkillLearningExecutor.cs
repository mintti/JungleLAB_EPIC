using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillLearningExecutor : MonoBehaviour ,IUIButtonExecutor
{
    public void B_Execute()
    {
        var skill = GetComponent<UISkillInfo>().BaseSkill;
        SkillInfo.I.LearnSkill(skill);

        UIManager.I.UINewSkillSelector.DisableSelector();
    }
}
