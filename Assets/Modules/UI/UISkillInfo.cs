using TH.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillInfo : MonoBehaviour
{
    [SerializeField] private Skill _baseSkill;
    // public SkillData BaseSkill { get; private set; }
    public Skill BaseSkill
    {
        get => _baseSkill;
        set => _baseSkill = value;
    }

    [Header("Component")]
    [SerializeField] private TextMeshProUGUI _nameTMP;
    [SerializeField] private Image _img;
    [SerializeField] private TextMeshProUGUI _descriptionTMP;

    bool _isNewSkill;
    public void Init(Skill skill, bool isNewSkill = false)
    {
        _isNewSkill = isNewSkill;
        BaseSkill = skill;
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        int level =  _baseSkill.Inner.Level;
        
        if (_isNewSkill)
        {
            var skill = GameManager.Player.Ability<PlayerMagic>().GetSkill(_baseSkill.Data.SkillType);

            if (skill != null)
            {
                level = skill.Inner.Level + 1;
            }
        }
    
        _nameTMP.text = $"{BaseSkill.Data.Name} {level}";
        _img.sprite = BaseSkill.Data.Sprite;
        _descriptionTMP.text = BaseSkill.Data.Description;
    }
}