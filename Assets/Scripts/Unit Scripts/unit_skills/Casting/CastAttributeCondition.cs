using UnityEngine;
using System.Collections;

public class CastAttributeCondition : MonoBehaviour {

	public Skill skill;
	public SkillResult.SkillAttribute skillAttribute;
	public float skillPower;


	public bool casted = false;
	// Use this for initialization
	void Start () {
		skill = GetComponent<Skill>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(skillAttribute) {
			case SkillResult.SkillAttribute.AttackRange: {
				//skill.unitScript.attributeScript.CurrentAttackRange += skillPower;
				break;
			}
			case SkillResult.SkillAttribute.AttackDamage: {
				//skill.unitScript.attributeScript.CurrentAttackDamage += skillPower;
				break;
			}
			case SkillResult.SkillAttribute.AttackSpeed: {
				//skill.unitScript.attributeScript.CurrentAttackSpeed += skillPower;
				break;
			}
			case SkillResult.SkillAttribute.HealthRegeneration: {
				//skill.unitScript.attributeScript.CurrentHealthRegeneration += skillPower;
				break;
			}
			case SkillResult.SkillAttribute.ShieldRegenration: {
				//skill.unitScript.attributeScript.CurrentShieldRegeneration += skillPower;
				break;
			}
			case SkillResult.SkillAttribute.VisionRange: {
				//skill.unitScript.attributeScript.CurrentVisionRange += skillPower;
				break;
			}
			case SkillResult.SkillAttribute.MovementSpeed: {
				//skill.unitScript.attributeScript.CurrentMovementSpeed += skillPower;
				break;
			}
			case SkillResult.SkillAttribute.isInFight: {
				if(skill.unitScript.unitGroupScript.isGroupInFight && !casted) {
					SendMessage("OnFire", SendMessageOptions.DontRequireReceiver);
					casted = true;
				} else if(!skill.unitScript.unitGroupScript.isGroupInFight && casted){
					SendMessage("OnUnFire", SendMessageOptions.DontRequireReceiver);
					casted = false;
				}
					break;
				}
		case SkillResult.SkillAttribute.isNotInFight: {
			if(!skill.unitScript.unitGroupScript.isGroupInFight && !casted) {
				SendMessage("OnFire", SendMessageOptions.DontRequireReceiver);
				casted = true;
			} else if(skill.unitScript.unitGroupScript.isGroupInFight && casted){
				SendMessage("OnUnFire", SendMessageOptions.DontRequireReceiver);
				casted = false;
			}
			break;
		}
			case SkillResult.SkillAttribute.Health: {
				if(skill.unitScript.healthScript.CurrentHealth < skillPower && !casted) {
					SendMessage("OnFire", SendMessageOptions.DontRequireReceiver);
					casted = true;
				} else if(skill.unitScript.healthScript.CurrentHealth >= skillPower && casted){
					SendMessage("OnUnFire", SendMessageOptions.DontRequireReceiver);
					casted = false;
				}
				break;
			}
		}
	}
}
