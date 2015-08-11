using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaHeal : MonoBehaviour {
	public Skill skill;

	public float healpower;
	public float healrange;
	
	public float flagCooldown;

	public UnitFaction targetFaction;
	public int numTargets;

	public void Start() {
		skill = GetComponent<Skill>();
	}

	void OnFire() {
		List<UnitScript> targetUnits = new List<UnitScript>();
		for(int i = 0; i < skill.unitScript.unitGroupScript.unitsInGroup.Count; i++) {
			if(i == numTargets) {
				break;
			}
			UnitScript unit = skill.unitScript.unitGroupScript.unitsInGroupScripts[i];
			if(unit != null) {
				if(unit.flagScript.Faction == targetFaction
				   && Vector3.Distance(transform.position, unit.transform.position) <= healrange
				   //&& !unit.currentSkillFlags.ContainsKey(SkillResult.flag_recentlyHealed)
				   && unit.healthScript.CurrentHealth < unit.healthScript.BaseHealth) {
					targetUnits.Add(unit);
				}
			}
		}
		SkillResult skillResult = new SkillResult();
		
		skillResult.skillType = SkillResult.SkillType.Heal;
		skillResult.skillPower = healpower;
		skillResult.skillFlag = SkillResult.flag_recentlyHealed;
		skillResult.skillFlagTimer = Time.time + flagCooldown;

		for(int i = 0; i < targetUnits.Count; i++) {
			SkillCalculator.passSkillResult(this.gameObject, targetUnits[i].gameObject, skillResult);
		}
	}
}
