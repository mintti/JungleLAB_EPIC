
/// <summary>
/// 스킬에서 동적으로 관리가 필요한 요소들을 포함합니다.
/// </summary>
public abstract class SkillInner
{
    public int Level { get; set; } = 1;

    public abstract void Execute();

    public void LevelUp()
    {
        Level++;
    }
}