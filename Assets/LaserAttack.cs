using UnityEngine;
using System.Collections;

public class LaserAttack : MonoBehaviour {
	public UnitFaction targetFaction;
	public float attackRadius;
	public float attackPower;

	public LaserLookAt laser;
	// Use this for initialization
	void Start () {
		laser = transform.parent.GetComponentInChildren<LaserLookAt>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnFire() {
		Collider[] hitColliders = Physics.OverlapSphere(transform.parent.position, attackRadius);

		int i = 0;
		while (i < hitColliders.Length) {

			if(hitColliders[i].gameObject.tag.Contains(vars.attackable_tag) &&
			   hitColliders[i].gameObject.GetComponent<FlagScript>().Faction == targetFaction &&
			   hitColliders[i].gameObject.GetComponent<HealthScript>().hasHealth) {
				laser.target = hitColliders[i].gameObject;
				laser.emitOnce();
				Debug.Log ("FOUND TARGET");
				SkillResult skillResult = new SkillResult();
				
				skillResult.skillType = SkillResult.SkillType.Damage;
				skillResult.skillPower = attackPower;
				//if(unitScript.unitTargetScript.attackTarget != null) {
				SkillCalculator.passSkillResult(transform.gameObject, hitColliders[i].gameObject, skillResult);
				//}

				break;
			}
			i++;
		}
	}
}
