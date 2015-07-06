using UnityEngine;
using System.Collections;

public class scene_forwarder : MonoBehaviour {



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//if(Application.loadedLevelName == vars.intro_scene_name){
			if(Input.anyKey){
				GameObject.Find("audio_playback_manager").GetComponent<audio_playback_manager>().play_ingame_ui_click_sound();

				Application.LoadLevel(vars.main_menu_scene_name);

			}
		//}
	}
}
