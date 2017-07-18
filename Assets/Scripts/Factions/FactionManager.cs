using UnityEngine;
using System.Collections;
using System.Linq;

public static class FactionManager {
	#region Static Methods
	public static bool AreHostile (GameObject obj1, GameObject obj2) {
		var faction1 = GetFactionId (obj1);
		return faction1 == 0 || faction1 != GetFactionId (obj2);
	}

	public static bool AreAllied (GameObject obj1, GameObject obj2) {
		var faction1 = GetFactionId (obj1);
		return faction1 != 0 && faction1 == GetFactionId (obj2);
	}

	/// <summary>
	/// Look up FactionMember recursively in object hierarchy
	/// Keyword: recursion
	/// </summary>
	static FactionMember GetFactionMember (GameObject obj) {
		// check if this object is of a given faction
		var factionMember = obj.GetComponent<FactionMember> ();
		if (factionMember == null && obj.transform.parent != null) {
			// check faction type of parent (and recurse through all ancestors)
			factionMember = GetFactionMember (obj.transform.parent.gameObject);
		}
		return factionMember;
	}

	public static int GetFactionId (GameObject obj) {
		var factionMember = GetFactionMember (obj);
		return factionMember != null ? factionMember.FactionId : 0;
	}

	public static void SetFaction (GameObject dest, GameObject src) {
		SetFaction (dest, GetFactionId (src));
	}

	public static void SetFaction (GameObject dest, int factionId) {
		var factionMember = GetFactionMember (dest);
		if (factionMember == null) {
			// make sure, object has FactionMember component
			factionMember = dest.AddComponent<FactionMember> ();
		}
		factionMember.FactionId = factionId;
	}
	#endregion
}