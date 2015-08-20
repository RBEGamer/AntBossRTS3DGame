﻿using UnityEngine;
using System.Collections;

public class SporeAttack : MonoBehaviour {
	public UnitFaction targetFaction;
	public float attackRadius;
	public float attackPower;

	// Use this for initialization
	void Start () {
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
			   !hitColliders[i].gameObject.GetComponent<FlagScript>().currentSkillFlags.ContainsKey(SkillResult.flag_recentlyHealed) &&
			   hitColliders[i].gameObject.GetComponent<HealthScript>().hasHealth) {
				Debug.Log ("FOUND TARGET");
				SkillResult skillResult = new SkillResult();
				
				skillResult.skillType = SkillResult.SkillType.Debuff;
				skillResult.skillAttribute = SkillResult.SkillAttribute.MovementSpeed;
				skillResult.skillFlag = SkillResult.flag_movementspeedDecreased;
				skillResult.skillPower = attackPower;

				SkillCalculator.passSkillResult(transform.gameObject, hitColliders[i].gameObject, skillResult);
			}
			i++;
		}
	}
}
