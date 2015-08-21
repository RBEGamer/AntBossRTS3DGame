using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AreaHeal : MonoBehaviour {
	public Skill skill;

	public float healpower;
	public float healrange;
	
	public float flagCooldown;

	public UnitFaction targetFaction;
	public int numTargets;

	public GameObject healEffect;
	public void Start() {
		skill = GetComponent<Skill>();
	}

	void OnFire() {
		List<UnitScript> targetUnits = new List<UnitScript>();
		for(int i = 0; i < skill.unitGroupScript.unitsInGroupRange.Count; i++) {
			if(i == numTargets) {
				break;
			}
			UnitScript unit = skill.unitGroupScript.unitsInGroupRangeScripts[i];
			if(unit != null) {
				if(unit.flagScript.Faction == targetFaction
				   && !unit.flagScript.currentSkillFlags.ContainsKey(SkillResult.flag_recentlyHealed)
				   && unit.healthScript.CurrentHealth < unit.healthScript.BaseHealth) {
					targetUnits.Add(unit);
				}
			}
		}

		if(targetUnits.Count < numTargets) {
			for(int i = 0; i < skill.unitGroupScript.unitsInGroupScripts.Count; i++) {
				if(i == numTargets) {
					break;
				}
				UnitScript unit = skill.unitGroupScript.unitsInGroupScripts[i];
				if(unit != null) {
					if(unit.flagScript.Faction == targetFaction
					   && !unit.flagScript.currentSkillFlags.ContainsKey(SkillResult.flag_recentlyHealed)
					   && unit.healthScript.CurrentHealth < unit.healthScript.BaseHealth) {
						targetUnits.Add(unit);
					}
				}
			}
		}
		
		sortByLowestHealth(ref targetUnits);
		SkillResult skillResult = new SkillResult();
		
		skillResult.skillType = SkillResult.SkillType.Heal;
		skillResult.skillPower = healpower;
		skillResult.skillFlag = SkillResult.flag_recentlyHealed;
		skillResult.skillFlagTimer = Time.time + flagCooldown;

		if(targetUnits.Count > 0 && numTargets > 0) {
			for(int i = 0; i < numTargets; i++) {
				SkillCalculator.passSkillResult(this.gameObject, targetUnits[i].gameObject, skillResult);
				Instantiate(healEffect, targetUnits[i].transform.position, Quaternion.identity);
			}
		}
	}

	void sortByLowestHealth(ref List<UnitScript> list) {
			list = list.OrderBy(x=>x.healthScript.CurrentHealth).ToList();
	}
}
