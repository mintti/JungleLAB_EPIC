using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class ResourceManager : IManager {
	public class Prefab {
		public const string PATH = "Prefabs/";

		public const string UI_CARD = "UICard";
		public const string SUMMON_FIREBALL = "SummonFireball";
	}

	#region PublicVariables
	public static GameObject LoadPrefab(string prefabName) {
		return Resources.Load<GameObject>(Prefab.PATH + prefabName);
	}
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public void Init() {
	
	}
	#endregion

	#region PrivateMethod
	#endregion
}

}