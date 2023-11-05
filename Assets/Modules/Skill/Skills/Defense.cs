using System;
using UnityEngine;

namespace Modules.Skill.Skills
{
    public class Defense : SkillInner
    {
        public override bool Execute()
        {
            int value = Level switch
            {
                1 => 3,
                2 => 4,
                3 => 5,
            };

            GameManager.Log.Log($"{value} 만큼의 쉴드를 추가할 것임(플레이어 연결 필요");
            return true;
        }
    }
}