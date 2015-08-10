using UnityEngine;
using System.Collections;

public class SkillCalculator : MonoBehaviour {

	public static void passSkillResult(GameObject attacker, GameObject target, SkillResult skillResult) {
		if(target != null) {
			target.SendMessage("OnSkillResult", skillResult, SendMessageOptions.DontRequireReceiver);
		}
		
	}
	/*
	public static SkillResult simpleHeal(UnitBase attacker, UnitBase target) {
		SkillResult skillResult = new SkillResult();
		
		skillResult.skillType = SkillResult.SkillType.Heal;
		skillResult.skillPower = power;
		skillResult.skillFlag = SkillResult.flag_recentlyHealed;

		target.SendMessage("OnTakeSkillResult", skillResult, SendMessageOptions.DontRequireReceiver);
		return skillResult;
	}

	public static SkillResult simpleBuff(UnitBase attacker, UnitBase target, float power, SkillResult.SkillAttribute attribute) {
		SkillResult skillResult = new SkillResult();
		
		skillResult.skillType = SkillResult.SkillType.Buff;
		skillResult.skillPower = power;
		skillResult.skillAttribute = attribute;

		target.SendMessage("OnTakeSkillResult", skillResult, SendMessageOptions.DontRequireReceiver);
		return skillResult;
	}*/
}
