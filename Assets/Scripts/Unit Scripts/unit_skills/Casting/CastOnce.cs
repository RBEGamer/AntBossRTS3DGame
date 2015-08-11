using UnityEngine;
using System.Collections;

public class CastOnce : MonoBehaviour {

	public bool isCasted = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!isCasted) {
			SendMessage("OnFire");
			isCasted = true;
			return;
		}
		if(isCasted) {
			this.transform.gameObject.SetActive(false);
		}
	}
}
