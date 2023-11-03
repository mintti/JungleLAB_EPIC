namespace Modules.Skill.Skills
{
    public class Heal : SkillInner
    {
        public override void Execute()
        {
            int value = Level switch
            {
                1 => 5,
                2 => 7,
                3 => 10,
            };

            GameManager.Log.Log($"{value} 만큼 힐할 것임(플레이어 연결 필요");
        }
    }
}