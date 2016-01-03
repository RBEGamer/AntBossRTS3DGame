using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
	public UnitScript unitScript;

	public GameObject previousTarget;
	public int numHitsOnCurrentTarget;

	public void Start() {
		unitScript = GetComponent<UnitScript>();
	}

	public void attackCurrentTarget() {
        if (unitScript.unitTargetScript.attackTarget == null)
        {
            return;
        }
		SkillResult skillResult = new SkillResult();
		GameObject weaponTarget = unitScript.unitTargetScript.attackTarget.gameObject;

		if(weaponTarget == previousTarget) {
			numHitsOnCurrentTarget++;
		} else {
			numHitsOnCurrentTarget = 0;
			previousTarget = weaponTarget;
		}


		skillResult.skillType = SkillResult.SkillType.Damage;
		skillResult.skillPower = unitScript.attributeScript.CurrentAttackDamage;
		if(unitScript.unitTargetScript.attackTarget != null) {
			SkillCalculator.passSkillResult(transform.gameObject, unitScript.unitTargetScript.attackTarget.gameObject, skillResult);
		}
	}
}
