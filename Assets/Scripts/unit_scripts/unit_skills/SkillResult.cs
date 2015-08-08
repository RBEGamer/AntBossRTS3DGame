using UnityEngine;
using System.Collections;

public class SkillResult : ScriptableObject {
	// enums

	public enum SkillType {
		Damage,
		Heal,
		Buff,
		Debuff,
		Other
	}

	public enum SkillAttribute {
		AttackRange,
		AttackSpeed,
		AttackDamage,
		HealthRegeneration,
		ShieldRegenration,
		MovementSpeed,
		VisionRange,
		Health,
		Shield,
		None
	}


	// Flags
	public static string flag_recentlyHealed = "RecentlyHealed";

	// Skill Data
	public SkillType skillType = SkillType.Heal;

	public string skillFlag = "";
	public float skillFlagTimer = 0.0f;

	public float skillPower = 0.0f;

	public SkillAttribute skillAttribute = SkillAttribute.Health;
}
