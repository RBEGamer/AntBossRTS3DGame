using UnityEngine;
using System.Collections;

public class TutorialCheckWorkersAssigned : MonoBehaviour {
	public ressource targetRessource;
	public int numWorkers = 4;
	// Use this for initialization
	void Start () {
		targetRessource = GetComponent<ressource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(targetRessource.current_ants_working_on_this_res > numWorkers) {
			GameObject.Find("MANAGERS").GetComponent<TutorialScriptMain>().updateStep();
			this.enabled = false;
		}
	}
}
