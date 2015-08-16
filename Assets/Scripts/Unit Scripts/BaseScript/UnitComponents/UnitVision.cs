using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitVision : MonoBehaviour {
	public UnitScript unitScript;
	public List<FlagScript> objectsFlagsInRange;
	public List<GameObject> objectsInRange;

	public SphereCollider sphereCollider;

	public void Awake() {
		objectsFlagsInRange = new List<FlagScript>();
		objectsInRange = new List<GameObject>();
		sphereCollider = GetComponent<SphereCollider>();
		unitScript = transform.parent.GetComponent<UnitScript>();
	}

	public void Update() {
		cleanUp();
	}

	bool canSeeUnit(Vector3 target) {
		Ray testRay = new Ray(transform.position, (target - transform.position));
		RaycastHit hit;
		if(Physics.Raycast(testRay, out hit, LayerMask.GetMask("Obstacle"))) {
			if(hit.collider.tag.Contains(vars.blockage_tag)) {
				Debug.DrawRay(transform.position, (target - transform.position), Color.magenta, 10.0f);
				return false;
			}
			else {
				Debug.DrawRay(transform.position, (target - transform.position), Color.green, 10.0f);
				return true;
			}
		}
		else {
			Debug.DrawRay(transform.position, (target - transform.position), Color.green, 10.0f);
			return true;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(!canSeeUnit(other.transform.position)) {
			return;
		}
		if(other.gameObject.tag.Contains(vars.attackable_tag)) {
			if(!canSeeUnit(other.transform.position)) {
				return;
			}
			if(other.gameObject != null) {
				AddObject(other.gameObject.GetComponent<FlagScript>());
			}
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag.Contains(vars.attackable_tag)) {
			if(other.gameObject != null) {
				RemoveObject(other.gameObject.GetComponent<FlagScript>());
			}
		}
	
	}

	void AddObject(FlagScript visionObject) {
		if(visionObject != null) {
			if(!objectsFlagsInRange.Contains(visionObject)) {
				objectsFlagsInRange.Add(visionObject);
				objectsInRange.Add(visionObject.gameObject);
				unitScript.unitGroupScript.addUnitToRange(visionObject.gameObject);
			}
		}
	}

	void RemoveObject(FlagScript visionObject) {
		if(objectsFlagsInRange.Contains(visionObject)) {
			objectsFlagsInRange.Remove(visionObject);
			objectsInRange.Remove(visionObject.gameObject);
			unitScript.unitGroupScript.removeUnitFromRange(visionObject.gameObject);
		}
	}

	void setRange(float range) {
		sphereCollider.radius = range;
	}

	// keep order in list
	public void cleanUp()
	{
		for(int i = objectsFlagsInRange.Count - 1; i >= 0; i--) {
			if (objectsFlagsInRange[i] == null)
			{
				objectsFlagsInRange.RemoveAt(i);
			}
		}
		
		for(int i = objectsInRange.Count - 1; i >= 0; i--) {
			if (objectsInRange[i] == null)
			{
				objectsInRange.RemoveAt(i);
			}
		}
	}
}
