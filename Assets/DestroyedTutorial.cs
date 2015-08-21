using UnityEngine;
using System.Collections;

public class DestroyedTutorial : MonoBehaviour {

	public static int countDestroyed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void destroyObject() {
		countDestroyed++;
		if(countDestroyed == 2) {
			Application.LoadLevel(vars.mission_selection_scene_name);
		}
		Destroy (gameObject, 0.1f);
	}
}
