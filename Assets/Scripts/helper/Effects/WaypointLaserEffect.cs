using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointLaserEffect : MonoBehaviour {
	public float referenceLifteTime;
	public float currentLifeTime;
	
	public float referenceDistance;
	public float currentDistance;


	public GameObject target;

	[SerializeField]
	public List<ParticleSystem> particleSystem;

	public void Start() {
		for(int i = 0; i < particleSystem.Count; i++) {
			particleSystem[i].Stop();
		}
	}

	void Update () {
		//
	}

	public void show(GameObject newTarget, bool show, int index) {
		if(show) {
			target = newTarget;
			if(target) {
				particleSystem[index].gameObject.transform.LookAt(target.transform);
				currentDistance = Vector3.Distance(this.transform.position, target.transform.position);
				currentLifeTime = (currentDistance/referenceDistance) * referenceLifteTime;
				particleSystem[index].startLifetime = currentLifeTime;
			} 
			particleSystem[index].Play();
		} else {

			target = null;
			particleSystem[index].Stop();
		}
		
	}
}
