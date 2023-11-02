using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillInfo : MonoBehaviour
{
    public SkillData BaseSkill { get; private set; }
    
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI _nameTMP;
    [SerializeField] private Image _img;
    [SerializeField] private TextMeshProUGUI _descriptionTMP;
    
    public void Init(SkillData skill)
    {
        BaseSkill = skill;
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        _nameTMP.text = $"{BaseSkill.Name} {BaseSkill.Inner.Level}";
        _img.sprite = BaseSkill.Sprite;
        _descriptionTMP.text = BaseSkill.Description;
    }
}