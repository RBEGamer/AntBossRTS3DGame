using UnityEngine;
using System.Collections;

public class AutomaticCast : MonoBehaviour {

	public float castCooldown;
	private float nextCast;
	// Use this for initialization
	void Start () {
		nextCast = Time.time + castCooldown;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Time.time > nextCast) {
			SendMessage("OnFire");
			nextCast = Time.time + castCooldown;
		}
	}
}
