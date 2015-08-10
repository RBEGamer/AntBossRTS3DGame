using UnityEngine;
using System.Collections;

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
}
