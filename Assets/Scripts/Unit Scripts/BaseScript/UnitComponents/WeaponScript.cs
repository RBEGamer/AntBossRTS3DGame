using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
	public UnitScript unitScript;

	public void Start() {
		unitScript = GetComponent<UnitScript>();
	}

	public void attackCurrentTarget() {
		SkillResult skillResult = new SkillResult();

		skillResult.skillType = SkillResult.SkillType.Damage;
		skillResult.skillPower = unitScript.attributeScript.CurrentAttackDamage;
		SkillCalculator.passSkillResult(transform.gameObject, unitScript.unitTargetScript.attackTarget.gameObject, skillResult);
	}
}
