using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Security.Cryptography.X509Certificates;

namespace TH.Core
{
	public class PlayerMagic : PlayerAbility
	{
		#region PublicVariables
		public int MaxCastingCount
		{
			get => _maxCastingCount;
			set
			{
				_maxCastingCount = value;
				UIManager.I.UIPlayerInfo.UIPlayerSkill.UpdateMaxCastingCount(_maxCastingCount);
			}
		}

		public int CastingGauge
		{
			get => _castingGauge;
			set
			{
				_castingGauge = value;

				if (_castingGauge >= MaxCastingCount)
				{
					MagicCircleCount += _castingGauge / MaxCastingCount;
					CastingGauge = _castingGauge % MaxCastingCount;
				}

				UIManager.I.UIPlayerInfo.UIPlayerSkill.UpdateCastingGauge(_castingGauge, _maxCastingCount);
			}
		}

		public int MagicCircleCount
		{
			get => _magicCircleCount;
			set
			{
				_magicCircleCount = value;
				UIManager.I.UIPlayerInfo.UIPlayerSkill.UpdateMagicCircleCount(_magicCircleCount);
			}
		}
		#endregion

		#region PrivateVariables
		[SerializeField] private int _maxCastingCount = 5;
		[SerializeField, ReadOnly] private int _castingGauge;
		[SerializeField, ReadOnly] private int _magicCircleCount;

		private List<SkillData> _learnedSkills;
		#endregion

		#region PublicMethod
		[Button]
		public void AddCastingGauge(int value)
		{
			CastingGauge += value;
		}

		public void LearnSkill(SkillData learningSkill)
		{
			_learnedSkills ??= new();

			var skill = _learnedSkills.FirstOrDefault(x => x.SkillType == learningSkill.SkillType);
			if (skill == null)
			{
				_learnedSkills.Add(learningSkill);
			}
			else
			{
				skill.Inner.LevelUp();
			}

			UIManager.I.UIPlayerInfo.UIPlayerSkill.LearnSkill(learningSkill);
		}

		public bool HasLearn(SkillData skillData)
		{
			return _learnedSkills.Any(x => x.SkillType == skillData.SkillType);
		}

		public SkillData GetSkill(SkillType skillType)
		{
			if (_learnedSkills == null)
			{
				return null;
			}

			if (_learnedSkills.Count == 0)
			{
				return null;
			}

			if (_learnedSkills.Find(x => x.SkillType == skillType) == null)
			{
				return null;
			}
			
			return _learnedSkills.FirstOrDefault(x => x.SkillType == skillType);
		}
        #endregion

        #region PrivateMethod
        #endregion
    }

}