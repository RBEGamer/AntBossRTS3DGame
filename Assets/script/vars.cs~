using UnityEngine;
using System.Collections;

public class vars : MonoBehaviour {

	/* ---------BEGIN DO NOT CHNAGE-------------------- */
	public enum ressource_type
	{
		A,B,C, default_type
	}
	
	public struct res_type_default{
		public static float max_harvest = 0f;
		public static int max_collector_ants = 0;
		public static float interaction_circle_radius = 0f;
		public static float interactition_latitude = 0f;
		public static float ant_bite_decrease = 0;
		public static int health_percentage = 0;
		public static string ui_displayname_ressource = "<no valid res type>";
	}


	public struct costs_nothing_ants{
		public static float costs_res_a = 0.0f;
		public static float costs_res_b = 0.0f;
		public static float costs_res_c = 0.0f;
	}


	public enum audio_playback_type
	{
		none, effect, music //is music -> looping is enabled
	}





	/* ---------END DO NOT CHNAGE-------------------- */
		





	/* ---------AUDIO SETTINGS-------------------- */

	//* change the values in audio clip state.cs too
	public enum audio_name
	{
		bgmusic, ui_click, audio_clip_info_ingame_uiclick
	}
	
	
	
	public struct audio_clip_info_bgmusic{
		public static string audio_clip_path = "carpenter_brut_run";
		public static audio_playback_type ptype = vars.audio_playback_type.music;
		public static float volume = 1.0f;
		public static int priority = 128;
		public static float pitch = 1.0f;
	}
	
	public struct audio_clip_info_uiclick{
		public static string audio_clip_path = "button-09";
		public static audio_playback_type ptype = vars.audio_playback_type.effect;
		public static float volume = 1.0f;
		public static int priority = 128;
		public static float pitch = 1.0f;
	}



	/* ---------END AUDIO SETTINGS-------------------- */







	/* -------RESSOURCE SETTING AREA ----------------------*/




	public static string ui_displayname_no_ressource_selected_text = "SELECT RESSOURCE";



	public struct res_type_a{
		public static float max_harvest = 1000.0f; //wie viel ressourcen können abgebaut werden von dieser ressource
		public static int max_collector_ants = 5; //wie viele Armeisen können an dieser REssource abbauen
		public static float interaction_circle_radius = 2.0f;
		public static float interactition_latitude = 0.1f;
		public static float ant_bite_decrease = 10; //wie viel soll eine ameise bei einem biss tragen können
		public static int health_percentage = 100;
		public static string ui_displayname_ressource = "Nahrung";
	}
	public struct res_type_b{
		public static float max_harvest = 2000.0f;
		public static int max_collector_ants = 10;
		public static float interaction_circle_radius = 2.0f;
		public static float interactition_latitude = 0.1f;
		public static float ant_bite_decrease = 25;
		public static int health_percentage = 100;
		public static string ui_displayname_ressource = "Baumaterial";
	}
	public struct res_type_c{
		public static float max_harvest = 3000.0f;
		public static int max_collector_ants = 15;
		public static float interaction_circle_radius = 2.0f;
		public static float interactition_latitude = 0.1f;
		public static float ant_bite_decrease = 50;
		public static int health_percentage = 100;
		public static string ui_displayname_ressource = "Blutegelvermehrungsmutter";
	}



	/* -------END RESSOURCE SETTING AREA ----------------------*/


	/* -------WAYPOINT SETTING AREA ----------------------*/
	public static Color waypoint_circle_color = Color.red; //farbe des kreises für die webpunkte
	public static Color ressource_circle_color = Color.cyan;
	public static float minimum_way_point_distance = 1.5f; //welcher abstand muss ein wegpunkt mindestens zum nächsten haben
	public static float way_point_interaction_circle_radius = 17.0f; //welcher ist der maximal abstand von wegpunkt zu wegpunkt
	public static float waypoint_interactition_latitude = 0.1f;
    public static float waypoint_node_connection_line_width = 0.1f; //wie dick soll die linie des kreises sein
	public static float res_interaction_radius = 1.0f; //wie nach muss der wegpunkt an der ressource gesetzt werden damit sich der wegpunkt mit der rressource verbindet
	public static float res_interaction_circle_width = 0.1f; //wie dick soll die linie des kreises sein
	public static int unit_scout_amount = 10; //ähm keine ahnugn wo die und ob die überhaupt verwendet wird............ zu faul zum suchen


	/* -------END WAYPOINT SETTING AREA ----------------------*/

	
	public static float scout_ant_move_speed = 8.0f; //wie schnell sollen sich die scout ameisen bewegen
	public static bool scout_ant_en_dyn_speed = false; //wenn auf true  brauchen die Ameisen immer die selbe zeit zum node -> längere strecke = schnellere ameise
	public static float collector_ant_speed = 5.0f;//wie schnell sollen sich die collector ameisen bewegen
	public static bool collector_ant_en_dyn_speed = false;//wenn auf true  brauchen die Ameisen immer die selbe zeit zum node -> längere strecke = schnellere ameise
	public static int res_manager_ant_spawn_speed = 100; //wie schnell werden neue collector ameisen erstellt



	/* -------BASE SETTINGS AREA ----------------------*/

	public static float initial_res_a_storage = 5000.0f;
	public static float initial_res_b_storage = 500.0f;
	public static float initial_res_c_storage = 500.0f;

	public static int initial_collector_ants = 1;
	public static int initial_attack_ants = 10;
	public static int initial_scout_ants = 10;

	public static int base_start_health_percentage = 100;

	public struct costs_scout_ants{
		public static float costs_res_a = 100.0f;
		public static float costs_res_b = 500.0f;
		public static float costs_res_c = 0.0f;
	}
	
	public struct costs_collector_ants{
		public static float costs_res_a = 2000.0f;
		public static float costs_res_b = 0.0f;
		public static float costs_res_c = 0.0f;
	}

	public struct costs_attack_ants{
		public static float costs_res_a = 700.0f;
		public static float costs_res_b = 100.0f;
		public static float costs_res_c = 0.0f;
	}
	
	/* -------END BASE SETTINGS AREA ----------------------*/


	/*----------- TAGS ----------------------------------- Please define in the unity tag area */
	public static string wp_node_tag = "wp_node";
	public static string res_tag = "ressource";
	public static string scout_ant_tag = "scout_ant";
	public static string collector_ant_tag = "collector_ant";
	public static string environment_tag = "environment";
	public static string ground_tag = "ground";	
	public static string base_tag = "base";	
	public static string audio_clip_tag = "audio_clip_holder";
	/* --------------- NAMES -------------------------------: most cases <name> + "_" + <id> */
	public static string path_manager_name = "path_manager";
	public static string res_name = "ressource";
	public static string wp_node_name = "node";
	public static string ui_manager_name = "ui_manager";
	public static string collector_ant_name = "collector_ant";
	public static string sleep_pos_manager_name = "sleep_pos_manager";
	public static string ressource_manager_name = "ressource_manager";
	public static string base_name = "ant_base";
	public static string walk_way_manager_name = "walk_way_manager";
	public static string ground_terrain_name = "ground";
	public static string sound_manager_name = "sound_manager";
	public static string main_camera_script_holder_name = "camera_controller";
	/*---------------  SCENE NAMES (please add the scene in the Unity Build settings ------------------*/
	public static string main_menu_scene_name  = "main_menu";
	public static string mission_selection_scene_name = "mission_selection";
	public static string credit_scene_name = "credits";
	public static string intro_scene_name = "intro";
	public static string optionn_scene_name = "options";
	public static string mission_one_scene_name = "main";
	public static string mission_two_scene_name = "main";
	public static string mission_three_scene_name = "main";
	public static string mission_four_scene_name = "main";
	public static string mission_five_scene_name = "main";


	//WAYPOINT VARS
	public static bool is_in_patheditmode = false;






	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//


























}
