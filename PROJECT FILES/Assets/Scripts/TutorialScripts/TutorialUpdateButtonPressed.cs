using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TutorialUpdateButtonPressed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) )
		{
			if (EventSystem.current.currentSelectedGameObject == this.gameObject) {
				GameObject.Find("MANAGERS").GetComponent<TutorialScriptMain>().updateStep();
				this.enabled = false;
			}
		}

	}
}
