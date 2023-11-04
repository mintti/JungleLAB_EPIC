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

            int catIndex = GameManager.Player.Position;
            GameObject cat = Instantiate(GameManager.Resource.LoadPrefab(ResourceManager.Prefabs.SUMMON_BLACKCAT),
                BoardManager.I.GetTilePos(catIndex), Quaternion.identity);
            cat.GetComponent<SummonCat>().Init(catIndex,3,value);
        }
    }
}