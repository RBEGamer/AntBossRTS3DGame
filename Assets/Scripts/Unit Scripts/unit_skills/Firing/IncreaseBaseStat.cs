using UnityEngine;
using System.Collections;

public class IncreaseBaseStat : MonoBehaviour {
	public Skill skill;
	public SkillResult.SkillAttribute skillAttribute;
	public float skillPower;
	public void Start() {
		skill = GetComponent<Skill>();
	}

	void OnFire() {
		SkillResult skillResult = new SkillResult();

		skillResult.skillAttribute = skillAttribute;
		skillResult.skillType = SkillResult.SkillType.Buff;
		skillResult.skillPower = skillPower;
		skillResult.skillFlag = skillResult.skillType.ToString()+""+skillAttribute.ToString();

		SkillCalculator.passSkillResult(this.gameObject, skill.unitScript.gameObject, skillResult);
	}
}
