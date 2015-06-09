using UnityEngine;
using System.Collections;

public class menu_button_manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void goto_main_menu(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.ui_click);}
		Application.LoadLevel(vars.main_menu_scene_name);
	}

	public void exit_application(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.ui_click);}
		Application.Quit();
	}

	public void goto_credits(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.ui_click);}
		Application.LoadLevel(vars.credit_scene_name);
	}

	public void goto_mission_selection(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.ui_click);}
		Application.LoadLevel(vars.mission_selection_scene_name);
	}











	public void load_level_one(){
		GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.ui_click);
		Application.LoadLevel(vars.mission_one_scene_name);
	}
	public void load_level_two(){
		GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.ui_click);
		Application.LoadLevel(vars.mission_two_scene_name);
	}
	public void load_level_three(){
		GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.ui_click);
		Application.LoadLevel(vars.mission_three_scene_name);
	}
	public void load_level_four(){
		GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.ui_click);
		Application.LoadLevel(vars.mission_four_scene_name);
	}
	public void load_level_five(){
		GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.ui_click);
		Application.LoadLevel(vars.mission_five_scene_name);
	}
}
