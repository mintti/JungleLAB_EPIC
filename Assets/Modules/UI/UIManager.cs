using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIPlayerInfo _uiPlayerInfo;
    public UIPlayerInfo UIPlayerInfo => _uiPlayerInfo;
    
    [SerializeField] private UIBossInfo _uiBossInfo;
    public UIBossInfo UIBossInfo => _uiBossInfo;
    
    [SerializeField] private UINewSkillSelector _uiNewSkillSelector;
    public UINewSkillSelector UINewSkillSelector => _uiNewSkillSelector;
}
