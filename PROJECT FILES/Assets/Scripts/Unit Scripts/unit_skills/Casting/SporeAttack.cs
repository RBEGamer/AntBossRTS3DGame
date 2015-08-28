using UnityEngine;
using System.Collections;

public class SporeAttack : MonoBehaviour {
	public UnitFaction targetFaction;
	public float attackRadius;
	public float attackPower;

	public PlaySporeEffect sporeEffect;
	// Use this for initialization
	void Start () {
		sporeEffect = transform.parent.GetComponentInChildren<PlaySporeEffect>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnFire() {
		Collider[] hitColliders = Physics.OverlapSphere(transform.parent.position, attackRadius);
		
		int i = 0;
		while (i < hitColliders.Length) {
			Debug.Log ("HIT");
			if(hitColliders[i].gameObject.tag.Contains(vars.attackable_tag) &&
			   hitColliders[i].gameObject.GetComponent<FlagScript>().Faction == targetFaction &&
			   !hitColliders[i].gameObject.GetComponent<FlagScript>().currentSkillFlags.ContainsKey(SkillResult.flag_movementspeedDecreased) &&
			   hitColliders[i].gameObject.GetComponent<HealthScript>().hasHealth) {
				SkillResult skillResult = new SkillResult();
				sporeEffect.emitOnce();
				skillResult.skillType = SkillResult.SkillType.Debuff;
				skillResult.skillAttribute = SkillResult.SkillAttribute.MovementSpeed;
				skillResult.skillFlag = SkillResult.flag_movementspeedDecreased;
				skillResult.skillFlagTimer = 1.0f;
				skillResult.skillPower = attackPower;

				SkillCalculator.passSkillResult(transform.gameObject, hitColliders[i].gameObject, skillResult);
			}
			i++;
		}
	}
}
