using UnityEngine;
using System.Collections;

public class TutorialCheckIsConnected : MonoBehaviour {

	public path_point targetWP;
	// Use this for initialization
	void Start () {
		targetWP = GetComponent<path_point>();
	}
	
	// Update is called once per frame
	void Update () {
		if(targetWP.path_to_base.Count > 0) {
			GameObject.Find("MANAGERS").GetComponent<TutorialScriptMain>().updateStep();
			this.enabled = false;
		}
	}
}
