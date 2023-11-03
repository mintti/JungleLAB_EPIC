using UnityEngine;
using TH.Core;

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

            int seedIndex = GameManager.Player.Position;
            GameObject seed = Instantiate(GameManager.Resource.LoadPrefab(ResourceManager.Prefabs.SUMMON_FIREBALL),
                BoardManager.I.GetTilePos(seedIndex), Quaternion.identity);
            seed.GetComponent<MagicFlower>().Init(value, seedIndex);
            BoardManager.I.AddSummon(seed.GetComponent<MagicFlower>());
        }
    }
}