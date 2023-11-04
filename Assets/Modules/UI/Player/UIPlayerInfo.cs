using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerInfo : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private UIPlayerSkillInfo _uiPlayerSkillInfo;
    public UIPlayerSkillInfo UIPlayerSkill => _uiPlayerSkillInfo;

    [SerializeField] private UICardInfo _uiCardInfo;
    public UICardInfo UICardInfo => _uiCardInfo;

}
