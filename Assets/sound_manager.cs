using UnityEngine;
using System.Collections;

public class sound_manager : MonoBehaviour {



	// Use this for initialization
	void Awake () {
		this.name = vars.sound_manager_name;
		DontDestroyOnLoad(this);
	}


}
