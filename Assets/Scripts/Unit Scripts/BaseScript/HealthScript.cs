using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public bool hasShield = false;
	public float BaseShield = 0.0f;
	public float CurrentShield = 0.0f;

	public bool hasHealth = true;
	public float BaseHealth = 100.0f;
	public float CurrentHealth = 100.0f;

	public bool isRegenerating = true;
	
	void Awake() {
		resetHealth();
	}

	void OnSkillResult(SkillResult skillresult) {
		if(skillresult.skillType == SkillResult.SkillType.Damage) {
			float removedFromShield = 0;

			if(CurrentShield > 0) {
				removedFromShield = Mathf.Abs(CurrentShield - skillresult.skillPower);
			}

			CurrentHealth = Mathf.Clamp(CurrentHealth - (skillresult.skillPower - removedFromShield), -1.0f, BaseHealth);
		}

		else if(skillresult.skillType == SkillResult.SkillType.Heal) {
			CurrentHealth = Mathf.Clamp(CurrentHealth + skillresult.skillPower, -1.0f, BaseHealth);
		}

		else if(skillresult.skillType == SkillResult.SkillType.Buff) {
			if(skillresult.skillAttribute == SkillResult.SkillAttribute.Shield) {
				BaseShield += skillresult.skillPower;
				CurrentShield = BaseShield;
			}

			if(skillresult.skillAttribute == SkillResult.SkillAttribute.Health) {
				BaseHealth += skillresult.skillPower;
				CurrentHealth = BaseHealth;
			}
		}

		checkDeath();
	}

	public void regenerate(float healthRegen, float shieldRegen) {
		if(hasHealth) {
			CurrentHealth = Mathf.Clamp(CurrentHealth + healthRegen * Time.deltaTime, 0.0f, BaseHealth);
		}

		if(hasShield) {
			CurrentShield = Mathf.Clamp(CurrentShield + shieldRegen * Time.deltaTime, 0.0f, BaseShield);
		}
	}

	void resetHealth() {
		CurrentShield = BaseShield;
		CurrentHealth = BaseHealth;
	}

	void checkDeath() {
		if(CurrentHealth <= 0) {
			hasHealth = false;
		}
	}
}
