using UnityEngine;
using System.Collections;

public class SkillResult : ScriptableObject {
	// enums

	public enum SkillTargetType {
		Unit,
		UnitGroup,
		None
	}

	public enum SkillType {
		Damage,
		Heal,
		Buff,
		Debuff,
		RemoveBuff,
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

	public enum SkillPowerType {
		Absolute,
		Percantage
	}

	// Flags
	public static string flag_recentlyHealed = "RecentlyHealed";

	// Skill Data
	public SkillType skillType = SkillType.Heal;
	public SkillAttribute skillAttribute = SkillAttribute.Health;
	public SkillPowerType skillPowerType = SkillPowerType.Absolute;

	public string skillFlag = "";
	public string skillFlagUnique = "";
	public float skillFlagTimer = 0.0f;

	public float skillPower = 0.0f;


}
