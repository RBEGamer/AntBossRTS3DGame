using UnityEngine;
using System.Collections;

public class CastNumHitsOnTarget : MonoBehaviour {
	public Skill skill;
	public WeaponScript weaponScript;
	public int currentHits;

	public ParticleSystem particleSystem;
	// Use this for initialization
	void Start () {
		skill = GetComponent<Skill>();
		weaponScript = skill.unitScript.GetComponent<WeaponScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(weaponScript.numHitsOnCurrentTarget > currentHits) {

			for(int i = currentHits; i < weaponScript.numHitsOnCurrentTarget; i++) {
				SendMessage("OnFire", SendMessageOptions.DontRequireReceiver);
			}
		    currentHits = weaponScript.numHitsOnCurrentTarget;
		} else if(weaponScript.numHitsOnCurrentTarget < currentHits){
			for(int i = currentHits; i > weaponScript.numHitsOnCurrentTarget; i--) {
				SendMessage("OnUnFire", SendMessageOptions.DontRequireReceiver);
			}
			currentHits = weaponScript.numHitsOnCurrentTarget;
		}

		if(currentHits > 0) {
			particleSystem.Play();
		} else {
			particleSystem.Stop();
		}
		
	}
}
