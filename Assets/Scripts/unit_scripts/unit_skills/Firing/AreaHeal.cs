using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaHeal : MonoBehaviour {
	public UnitBase thisUnit;

	public float healpower;
	public float healrange;
	
	public float flagCooldown;

	public UnitFaction targetFaction;
	public int numTargets;

	void OnFire() {
		List<UnitBase> targetUnits = new List<UnitBase>();
		for(int i = 0; i < thisUnit.unitsInRange.Count; i++) {
			if(i == numTargets) {
				break;
			}
			UnitBase unit = thisUnit.unitsInRange[i].GetComponent<UnitBase>();
			if(unit != null) {
				if(unit.unitFaction == targetFaction
				   && Vector3.Distance(transform.position, unit.transform.position) <= healrange
				   && !unit.currentSkillFlags.ContainsKey(SkillResult.flag_recentlyHealed)
				   && unit.unitCurrentHealth < unit.unitBaseHealth) {
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
			SkillCalculator.passSkillResult(thisUnit, targetUnits[i], skillResult);
		}
	}
}
