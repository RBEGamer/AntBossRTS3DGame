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
		Application.LoadLevel(vars.main_menu_scene_name);
	}

	public void exit_application(){
		Application.Quit();
	}

	public void goto_credits(){
		Application.LoadLevel(vars.credit_scene_name);
	}

	public void goto_mission_selection(){
		Application.LoadLevel(vars.mission_selection_scene_name);
	}











	public void load_level_one(){
		Application.LoadLevel(vars.mission_one_scene_name);
	}
	public void load_level_two(){
		Application.LoadLevel(vars.mission_two_scene_name);
	}
	public void load_level_three(){
		Application.LoadLevel(vars.mission_three_scene_name);
	}
	public void load_level_four(){
		Application.LoadLevel(vars.mission_four_scene_name);
	}
	public void load_level_five(){
		Application.LoadLevel(vars.mission_five_scene_name);
	}
}
