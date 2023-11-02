using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillInfo : MonoBehaviour
{
    private SkillInfo _baseSkill;
    
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI _nameTMP;
    [SerializeField] private Image _img;
    [SerializeField] private TextMeshProUGUI _descriptionTMP;
    
    public void Init(SkillInfo skill)
    {
        _baseSkill = skill;
        
        _nameTMP.text = skill.Name;
        _img.sprite = skill.Sprite;
        _descriptionTMP.text = skill.Description;
    }
}