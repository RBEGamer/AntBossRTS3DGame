using UnityEngine;
using System.Collections;

public class audio_playback_manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.name = "audio_playback_manager";
	}
	
	// Update is called once per frame
	void Update () {
	
	}




	public void play_bgmusic(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.bgmusic);}
	}

	public void play_ingame_ui_click_sound(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.ui_click);}
	}

	public void play_wp_add_fail_sound(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.not_enought_ressources);}
	}

	public void play_wp_add_succ_sound(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.wp_add);}

	}

	public void play_wp_remove_sound(){

	}

	public void play_wp_connect_sound(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.wp_connect);}

	}

	public void play_wp_disconnect_sound(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.wp_remove_connect);}

	}

	public void play_wp_move_place_sound(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.wp_move);}

	}

	public void play_wp_select_sound(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.select_waypoint);}
	}


	public void play_unit_select_sound(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.select_unit);}
	}
	public void play_base_select_sound(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.select_base_1);}
		//if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.select_base_2);}

	}
	public void play_ressource_select_sound(){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source(vars.audio_name.select_ressource);}
	}





	public void play_unit_move_sound(GameObject obj){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source_to_object(vars.audio_name.unit_move_1, obj);}
		//if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source_to_object(vars.audio_name.unit_move_2, obj);}
		//if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source_to_object(vars.audio_name.unit_move_3, obj);}
		//if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source_to_object(vars.audio_name.unit_move_4, obj);}

	}
	public void play_unit_back_move_sound(GameObject obj){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source_to_object(vars.audio_name.unit_back_move_1, obj);}
		//if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source_to_object(vars.audio_name.unit_back_move_2, obj);}
	}

	public void play_unit_attack_move_sound(GameObject obj){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source_to_object(vars.audio_name.unit_attack_move_1, obj);}
		//if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source_to_object(vars.audio_name.unit_attack_move_2, obj);}
	}

	public void play_destroy_base_sound(GameObject obj){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source_to_object(vars.audio_name.destroy_base, obj);}
		//if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source_to_object(vars.audio_name.unit_attack_move_2, obj);}
	}

	public void play_destroy_skorpion_base_sound(GameObject obj){
		if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source_to_object(vars.audio_name.destroy_skorpion_base, obj);}
		//if(GameObject.Find(vars.sound_manager_name) != null){GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().create_audio_source_to_object(vars.audio_name.unit_attack_move_2, obj);}
	}

	destroy_skorpion_base
}
