using UnityEngine;
using System.Collections;

public class SkillCalculator : MonoBehaviour {

	public static void passSkillResult(GameObject attacker, GameObject target, SkillResult skillResult) {
		if(target != null) {
			target.SendMessage("OnSkillResult", skillResult, SendMessageOptions.DontRequireReceiver);
		}
	}
}
