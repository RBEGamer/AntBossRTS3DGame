using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum UnitFaction
{
	PlayerFaction,
	EnemyFaction,
	NeutralFaction,
	NoFaction
}



public class FlagScript : MonoBehaviour {
	public UnitFaction Faction;
	public string skillFlags;
	// Upgrade logic(all units)
	[SerializeField]
	public Dictionary<string, float> currentSkillFlags;

	public void clearFlags() {
		for (int index = 0; index < currentSkillFlags.Count; index++) {
			var item = currentSkillFlags.ElementAt(index);
			if(Time.time > item.Value) {
				currentSkillFlags.Remove(item.Key);
			}
		}
	}

	public void FixedUpdate() {
		clearFlags();
	}
}
