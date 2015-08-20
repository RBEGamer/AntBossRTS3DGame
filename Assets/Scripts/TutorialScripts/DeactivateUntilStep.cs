using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeactivateUntilStep : MonoBehaviour {
	public TutorialScriptMain tutorialScript;
	public int targetStep;

	MonoBehaviour[] listComponents;

	// Use this for initialization
	void Start () {
		tutorialScript = GameObject.Find ("MANAGERS").GetComponent<TutorialScriptMain>();
		listComponents = GetComponents<MonoBehaviour>();

		foreach(MonoBehaviour comp in listComponents) {
			if(comp != this) {
				comp.enabled = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(tutorialScript.tutorialStep >= targetStep) {
			foreach(MonoBehaviour comp in listComponents) {
					comp.enabled = true;
			}

			this.enabled = false;
		}
	}
}
