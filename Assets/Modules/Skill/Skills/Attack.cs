using TH.Core;

namespace Modules.Skill.Skills
{
    public class Attack : SkillInner
    {
        public override void Execute()
        {
            int value = Level switch
            {
                1 => 6,
                2 => 8,
                3 => 10,
            };

            GameManager.Log.Log($"{value} 만큼의 데미지를 입힐 것임(몬스터 연결 필요");
        }
    }
}