using UnityEngine;
using System.Collections;

public class WaypointLaserEffect : MonoBehaviour {
	public float referenceLifteTime;
	public float currentLifeTime;
	
	public float referenceDistance;
	public float currentDistance;


	public GameObject target;

	public ParticleSystem particleSystem;

	public void Start() {
		show (null, false);
	}
	void Update () {
		if(target) {
			particleSystem.gameObject.transform.LookAt(target.transform);
			currentDistance = Vector3.Distance(this.transform.position, target.transform.position);
			currentLifeTime = (currentDistance/referenceDistance) * referenceLifteTime;
			particleSystem.startLifetime = currentLifeTime;
		} 
	}

	public void show(GameObject newTarget, bool show) {
		if(show) {
			target = newTarget;
			particleSystem.Play();
		} else {

			target = null;
			particleSystem.Stop();
		}
		
	}
}
