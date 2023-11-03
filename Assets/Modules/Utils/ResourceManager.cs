using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core
{

	public class ResourceManager : IManager
	{
		public class Prefabs
		{
			public const string PATH = "Prefabs/";

			public const string UI_CARD = "UI/Card";
			public const string SUMMON_FIREBALL = "Summon/FireBall";
			public const string SUMMON_SEED = "Summon/Seed";
			public const string SUMMON_BlackCat = "Summon/BlackCat";
			public const string CURSE_FIREBREATH = "Summon/FireBreath";
		}

		public class Sprites
		{
			public const string PATH = "Image/";

			public const string UI_EMBLEM_WIZARD = "Emblem/Wizard";
			public const string TILE_ATTACK = "Tile/AttackTile";
			public const string TILE_DEFENSE = "Tile/DefenseTile";
			public const string TILE_CURSE = "Tile/CurseTile";
		}

		#region PublicVariables
		#endregion

		#region PrivateVariables
		private Dictionary<string, GameObject> _prefabList = new Dictionary<string, GameObject>();
		private Dictionary<string, Sprite> _spriteList = new Dictionary<string, Sprite>();
		#endregion

		#region PublicMethod
		public void Init()
		{

		}

		public GameObject LoadPrefab(string prefabName)
		{
			string targetPath = Prefabs.PATH + prefabName;

			if (_prefabList.ContainsKey(prefabName))
			{
				return _prefabList[prefabName];
			}
			_prefabList.Add(prefabName, Resources.Load<GameObject>(targetPath));
			return _prefabList[prefabName];
		}

		public Sprite LoadSprite(string spriteName)
		{
			string targetPath = Sprites.PATH + spriteName;

			if (_spriteList.ContainsKey(spriteName))
			{
				return _spriteList[spriteName];
			}
			_spriteList.Add(spriteName, Resources.Load<Sprite>(targetPath));
			return _spriteList[spriteName];
		}
		#endregion

		#region PrivateMethod
		#endregion
	}

}