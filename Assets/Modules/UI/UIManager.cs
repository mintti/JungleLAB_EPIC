using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : Singleton<UIManager>
{
    [Header("Info UI")] 
    [SerializeField] private UIPlayerInfo _uiPlayerInfo;
    public UIPlayerInfo UIPlayerInfo => _uiPlayerInfo;
    
    // [SerializeField] private UIBossInfo _uiBossInfo;
    // public UIBossInfo UIBossInfo => _uiBossInfo;
    
    [SerializeField]private UIEnemyInfo _uiEnemyInfo;
    public UIEnemyInfo UIEnemyInfo => _uiEnemyInfo;
    
    [Header("Player Event")] 
    [SerializeField] private UINewSkillSelector _uiNewSkillSelector;
    public UINewSkillSelector UINewSkillSelector => _uiNewSkillSelector;

    [Header("Board Event")] 
    [SerializeField] private UIAltarInfo _uiAltarInfo;
    public UIAltarInfo UIAltarInfo => _uiAltarInfo;
    
    [SerializeField] private UIEventInfo _uiEventInfo;
    public UIEventInfo UIEventInfo => _uiEventInfo;
    
    [SerializeField] private UIGambleInfo _uiGambleInfo;
    public UIGambleInfo UIGambleInfo => _uiGambleInfo;
    
    [SerializeField] private UIStartPanel _uiStartPanel;
    public UIStartPanel UIStartPanel => _uiStartPanel;

    [Header("Main UI")]
    [SerializeField] private UIMainButtonPanel _uiMain;
    public UIMainButtonPanel UIMain => _uiMain;


    [SerializeField] private GameObject _clearObj;
    public GameObject ClearObj => _clearObj;
    [SerializeField] private GameObject _gameOverObj;
    public GameObject GameOverObj => _gameOverObj;


}
