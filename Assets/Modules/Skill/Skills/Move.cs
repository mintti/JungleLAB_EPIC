namespace Modules.Skill.Skills
{
    public class Move : SkillInner
    {
        public override bool Execute()
        {
            GameManager.Log.Log($"시작 지점으로 순간이동을 할 것임");
            StartCoroutine(GameManager.Player.Teleport(0));
            return true;
        }
    }
}