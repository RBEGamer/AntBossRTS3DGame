using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {
	public int skillType = 0; // 0: on units, 1: on group, -1: everything else
	public UnitScript unitScript;
	public UnitGroupScript unitGroupScript;

	public GameObject skillTarget;

	public int researchTime;
	public int costA_research;
	public int costB_research;

	public int costA_equip;
	public int costB_equip;
}
