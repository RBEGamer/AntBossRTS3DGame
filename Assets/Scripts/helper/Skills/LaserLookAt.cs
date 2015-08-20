using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LaserLookAt : MonoBehaviour {
	public GameObject target;
	public ParticleSystem particleSystem;

	public float referenceLifteTime;
	public float currentLifeTime;

	public float referenceDistance;
	public float currentDistance;


	public float playTime;

	// Update is called once per frame
	void Update () {
		if(target) {
			currentDistance = Vector3.Distance(transform.position, target.transform.position);
			transform.LookAt(target.transform);
			currentDistance = Vector3.Distance(this.transform.position, target.transform.position);
			currentLifeTime = (currentDistance/referenceDistance) * referenceLifteTime;
			particleSystem.startLifetime = currentLifeTime;
		}

		if(playTime > 0) {
			playTime -= Time.deltaTime;
			if(playTime <= 0) {
				particleSystem.Stop();
			}
		}
	}

	public void emitOnce() {
		particleSystem.Play();
		playTime = 0.5f;

	}
}
