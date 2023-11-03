namespace Modules.Skill.Skills
{
    public class Spawn : SkillInner
    {
        public override void Execute()
        {
            int value = Level switch
            {
                1 => 1,
                2 => 2,
                3 => 3,
            };

            GameManager.Log.Log($"플레이어와 동일하게 이동하며 {value} 값만큼 타일을 활성화하는 소환수를 소환할 것임");
        }
    }
}