using UnityEngine;
using System.Collections;

public class AttributeScript : MonoBehaviour {

	// Basic unit attributes
	public float BaseAttackSpeed;
	protected float CurrentAttackSpeed;
	
	public float BaseAttackDamage;
	protected float CurrentAttackDamage;
	
	public float BaseHealthRegeneration;
	protected float CurrentHealthRegeneration;

	public float BaseShieldRegeneration;
	protected float CurrentShieldRegeneration;
	
	public float BaseMovementSpeed;
	protected float CurrentMovementSpeed;
	
	public float BaseVisionRange;
	protected float CurrentVisionRange;
	
	public float BaseAttackRange;
	protected float CurrentAttackRange;

	void Awake() {
		resetAttributes();
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
