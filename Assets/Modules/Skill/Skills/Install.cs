using UnityEngine;
using TH.Core;

namespace Modules.Skill.Skills
{
    public class Install : SkillInner
    {
        public override bool Execute()
        {
            int value = Level switch
            {
                1 => 4,
                2 => 6,
                3 => 8,
            };

            if (BoardManager.I.CheckDuplicateSummonOnTile<MagicFlower>(BoardManager.I.PlayerOnIndex))
            {
                return false;
            }
            int seedIndex = GameManager.Player.Position;
            GameObject seed = Instantiate(GameManager.Resource.LoadPrefab(ResourceManager.Prefabs.SUMMON_SEED),
                BoardManager.I.GetTilePos(seedIndex), Quaternion.identity);
            seed.GetComponent<MagicFlower>().Init(value, seedIndex);

            return true;
        }
    }
}