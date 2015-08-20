using UnityEngine;
using System.Collections;

public class PlaySporeEffect : MonoBehaviour {
	public ParticleSystem particleSystem;
	public float playTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(playTime > 0) {
			playTime -= Time.deltaTime;
			if(playTime <= 0) {
				particleSystem.Stop();
			}
		}
	}


	public void emitOnce() {
		particleSystem.Play();
		playTime = 5.0f;
		
	}
}
