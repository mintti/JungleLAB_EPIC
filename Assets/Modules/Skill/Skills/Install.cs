namespace Modules.Skill.Skills
{
    public class Install : SkillInner
    {
        public override void Execute()
        {
            int value = Level switch
            {
                1 => 4,
                2 => 6,
                3 => 8,
            };

            GameManager.Log.Log($"{value} 만큼의 데미지를 입히는 식물을 현재 땅에 심을 것임");
        }
    }
}