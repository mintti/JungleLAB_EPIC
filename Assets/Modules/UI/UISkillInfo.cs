using TH.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillInfo : MonoBehaviour
{
    [SerializeField] private SkillData _baseSkill;
    // public SkillData BaseSkill { get; private set; }
    public SkillData BaseSkill
    {
        get => _baseSkill;
        set => _baseSkill = value;
    }

    [Header("Component")]
    [SerializeField] private TextMeshProUGUI _nameTMP;
    [SerializeField] private Image _img;
    [SerializeField] private TextMeshProUGUI _descriptionTMP;

    bool _isLearn;
    public void Init(SkillData skill, bool isLearn = false)
    {
        _isLearn = isLearn;

        BaseSkill = skill;
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        var level = BaseSkill.Inner.Level;
        
        if(_isLearn)
        {
            if(GameManager.Player.Ability<PlayerMagic>().GetSkill(BaseSkill.SkillType) != null)
            {
                level += 1;
            }
        }
        
        _nameTMP.text = $"{BaseSkill.Name} {level}";
        _img.sprite = BaseSkill.Sprite;
        _descriptionTMP.text = BaseSkill.Description;
    }
}