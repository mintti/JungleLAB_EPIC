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
    
    [FormerlySerializedAs("_uiSkillSelector")] [SerializeField] private UINewSkillSelector uiNewSkillSelector;
    public UINewSkillSelector UINewSkillSelector => uiNewSkillSelector;
}
