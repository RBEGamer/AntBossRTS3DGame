using UnityEngine;
using System.Collections;

public class vars : MonoBehaviour {

	
	public enum ressource_type
	{
		A,B,C
	}

	public struct res_type_default{
		public static float max_harvest = 0f;
		public static int max_collector_ants = 0;
		public static float interaction_circle_radius = 0f;
		public static float interactition_latitude = 0f;
		public static float ant_bite_decrease = 0;
	}
	public struct res_type_a{
		public static float max_harvest = 1000.0f;
		public static int max_collector_ants = 5;
		public static float interaction_circle_radius = 2.0f;
		public static float interactition_latitude = 0.1f;
		public static float ant_bite_decrease = 10;
	}
	public struct res_type_b{
		public static float max_harvest = 2000.0f;
		public static int max_collector_ants = 10;
		public static float interaction_circle_radius = 2.0f;
		public static float interactition_latitude = 0.1f;
		public static float ant_bite_decrease = 25;
	}
	public struct res_type_c{
		public static float max_harvest = 3000.0f;
		public static int max_collector_ants = 15;
		public static float interaction_circle_radius = 2.0f;
		public static float interactition_latitude = 0.1f;
		public static float ant_bite_decrease = 50;
	}



	//res activation cycle


	public static float minimum_way_point_distance = 1.5f;
	public static float way_point_interaction_circle_radius = 7.0f;
	public static float waypoint_interactition_latitude = 0.1f;
    public static float waypoint_node_connection_line_width = 0.1f;
	public static float res_interaction_radius = 1.0f;
	public static float res_interaction_circle_width = 0.1f;
	public static string environment_tag = "environment";
	public static int unit_scout_amount = 10;
	public static string path_manager_name = "path_manager";
	public static string wp_node_name = "node";
	public static string wp_node_tag = "wp_node";
	public static string res_name = "ressource";
  	public static string res_tag = "ressource";
	public static string scout_ant_tag = "scout_ant";
	public static string collector_ant_tag = "collector_ant";
	public static float scout_ant_move_speed = 3.0f;
	public static bool scout_ant_en_dyn_speed = false;
	public static float collector_ant_speed = 2.0f;
	public static bool collector_ant_en_dyn_speed = false;

	public static string sleep_pos_manager_name = "sleep_pos_manager";



	//WAYPOINT VARS
	public static bool is_in_patheditmode = true;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void toggle_patheditmode(){

		is_in_patheditmode = !is_in_patheditmode;

	}

}
