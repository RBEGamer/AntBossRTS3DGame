using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {
	public int skillTier = 0;
	public int skillType = 0; // 0: on units, 1: on group, -1: everything else
	public bool specialNormal = false;
	public UnitScript unitScript;
	public UnitGroupScript unitGroupScript;

	public GameObject skillTarget;

	public bool researched = false;
	public int researchTime = 0;
	public int costA_research = 0;
	public int costB_research = 0;

	public int costA_equip = 0;
	public int costB_equip = 0;


	public string desc;
}
