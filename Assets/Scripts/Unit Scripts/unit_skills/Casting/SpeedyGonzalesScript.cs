using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeedyGonzales : MonoBehaviour {
	public UnitFaction targetFaction;
	public float duration;
	public float radius;
	public float boost;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnFire() {
		Collider[] hitColliders = Physics.OverlapSphere(transform.parent.position, radius);

		int i = 0;
		while (i < hitColliders.Length) {
			if(hitColliders[i].gameObject.tag.Contains(vars.attackable_tag) &&
			   hitColliders[i].gameObject.GetComponent<FlagScript>().Faction == targetFaction &&
			   !hitColliders[i].gameObject.GetComponent<FlagScript>().currentSkillFlags.ContainsKey(SkillResult.flag_movementspeedIncreased) &&
			   hitColliders[i].gameObject.GetComponent<HealthScript>().hasHealth) {
				if(hitColliders[i].gameObject.tag.Contains(vars.collector_ant_tag) || 
				   hitColliders[i].gameObject.tag.Contains(vars.scout_ant_tag)) {
					SkillResult skillResult = new SkillResult();
					
					skillResult.skillType = SkillResult.SkillType.Buff;
					skillResult.skillAttribute = SkillResult.SkillAttribute.MovementSpeed;
					skillResult.skillFlag = SkillResult.flag_movementspeedIncreased;
					skillResult.skillFlagTimer = duration;
					skillResult.skillPower = boost;

					SkillCalculator.passSkillResult(transform.gameObject, hitColliders[i].gameObject, skillResult);
				}
			}
			i++;
		}
	}
}
