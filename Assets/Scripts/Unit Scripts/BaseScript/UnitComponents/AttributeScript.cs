using UnityEngine;
using System.Collections;

public class AttributeScript : MonoBehaviour {

	// Basic unit attributes
	public float BaseAttackSpeed;
	public float CurrentAttackSpeed;
	
	public float BaseAttackDamage;
	public float CurrentAttackDamage;
	
	public float BaseHealthRegeneration;
	public float CurrentHealthRegeneration;

	public float BaseShieldRegeneration;
	public float CurrentShieldRegeneration;
	
	public float BaseMovementSpeed;
	public float CurrentMovementSpeed;
	
	public float BaseVisionRange;
	public float CurrentVisionRange;
	
	public float BaseAttackRange;
	public float CurrentAttackRange;

	public float UnitRadius;

	public UnitScript unitScript;

	void Awake() {
		resetAttributes();
		unitScript = GetComponent<UnitScript>();
	}

	void OnSkillResult(SkillResult skillResult) {

		if(skillResult.skillType == SkillResult.SkillType.Buff) {
			switch(skillResult.skillAttribute) {
			case SkillResult.SkillAttribute.AttackRange: {
				CurrentAttackRange += skillResult.skillPower;
				break;
			}
			case SkillResult.SkillAttribute.AttackDamage: {
				CurrentAttackDamage += skillResult.skillPower;
				break;
			}
			case SkillResult.SkillAttribute.AttackSpeed: {
				CurrentAttackSpeed += skillResult.skillPower;
				break;
			}
			case SkillResult.SkillAttribute.HealthRegeneration: {
				CurrentHealthRegeneration += skillResult.skillPower;
				break;
			}
			case SkillResult.SkillAttribute.ShieldRegenration: {
				CurrentShieldRegeneration += skillResult.skillPower;
				break;
			}
			case SkillResult.SkillAttribute.VisionRange: {
				CurrentVisionRange += skillResult.skillPower;
				break;
			}
			case SkillResult.SkillAttribute.MovementSpeed: {
				CurrentMovementSpeed += skillResult.skillPower;
				unitScript.navMeshAgent.speed = CurrentMovementSpeed;
				break;
			}
			}
		}
	}
	
	void resetAttributes() {
		CurrentAttackRange = BaseAttackRange;

		CurrentAttackSpeed = BaseAttackSpeed;

		CurrentAttackDamage = BaseAttackDamage;

		CurrentHealthRegeneration = BaseHealthRegeneration;

		CurrentMovementSpeed = BaseMovementSpeed;

		CurrentVisionRange = BaseVisionRange;
	}
}
