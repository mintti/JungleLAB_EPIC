using UnityEngine;
using TH.Core;
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

            int catIndex = GameManager.Player.Index;
            GameObject cat = Instantiate(GameManager.Resource.LoadPrefab(ResourceManager.Prefabs.SUMMON_FIREBALL),
                BoardManager.I.GetTilePos(catIndex), Quaternion.identity);
            cat.GetComponent<SummonCat>().Init(value, catIndex);
            BoardManager.I.AddSummon(cat.GetComponent<SummonCat>());
        }
    }
}