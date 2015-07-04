using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitRangeScript : MonoBehaviour {

	public UnitBase thisUnit;
	public SphereCollider myCollider;
	// Use this for initialization
	void Awake () {
		myCollider = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Contains(vars.attackable_tag)) {
			if(other.gameObject.tag.Contains(vars.enemy_tag) && thisUnit.gameObject.tag.Contains(vars.friendly_tag)){

				if(other.gameObject != null) {
					Debug.Log (thisUnit.gameObject.tag + " sees " + other.gameObject.tag);
					thisUnit.addEnemyInRange(other.gameObject);
				}
			}

			if(other.gameObject.tag.Contains(vars.friendly_tag) && thisUnit.gameObject.tag.Contains(vars.enemy_tag)){

				if(other.gameObject != null) {
					Debug.Log (thisUnit.gameObject.tag + " sees " + other.gameObject.tag);
					thisUnit.addEnemyInRange(other.gameObject);
				}
			}
		}
	}

	void OnCollisionEnter (Collision other)
	{
		if(other.gameObject.tag.Contains(vars.attackable_tag)) {
			if(other.gameObject.tag.Contains(vars.enemy_tag) && thisUnit.gameObject.tag.Contains(vars.friendly_tag)){
				
				if(other.gameObject != null) {
					Debug.Log (thisUnit.gameObject.tag + " sees " + other.gameObject.tag);
					thisUnit.addEnemyInRange(other.gameObject);
				}
			}
			
			if(other.gameObject.tag.Contains(vars.friendly_tag) && thisUnit.gameObject.tag.Contains(vars.enemy_tag)){
				
				if(other.gameObject != null) {
					Debug.Log (thisUnit.gameObject.tag + " sees " + other.gameObject.tag);
					thisUnit.addEnemyInRange(other.gameObject);
				}
			}
		}
	}

	void OnTriggerStay(Collider other) {
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag.Contains(vars.attackable_tag)) {
			if(other.gameObject.tag.Contains(vars.enemy_tag) && thisUnit.gameObject.tag.Contains(vars.friendly_tag)){
				//Debug.Log (thisUnit.gameObject.tag + " does not see " + other.gameObject.tag + " anymore");
				if(other.gameObject != null) {
					thisUnit.removeEnemyInRange(other.gameObject);
				}
			}

			if(other.gameObject.tag.Contains(vars.friendly_tag) && thisUnit.gameObject.tag.Contains(vars.enemy_tag)){
				//Debug.Log (thisUnit.gameObject.tag + " does not see " + other.gameObject.tag + " anymore");
				if(other.gameObject != null) {
					thisUnit.removeEnemyInRange(other.gameObject);
				}
			}
		}
	}

	void OnCollisionExit (Collision other)
	{
		if(other.gameObject.tag.Contains(vars.attackable_tag)) {
			if(other.gameObject.tag.Contains(vars.enemy_tag) && thisUnit.gameObject.tag.Contains(vars.friendly_tag)){
				
				if(other.gameObject != null) {
					Debug.Log (thisUnit.gameObject.tag + " sees " + other.gameObject.tag);
					thisUnit.addEnemyInRange(other.gameObject);
				}
			}
			
			if(other.gameObject.tag.Contains(vars.friendly_tag) && thisUnit.gameObject.tag.Contains(vars.enemy_tag)){
				
				if(other.gameObject != null) {
					Debug.Log (thisUnit.gameObject.tag + " sees " + other.gameObject.tag);
					thisUnit.addEnemyInRange(other.gameObject);
				}
			}
		}
	}
}
