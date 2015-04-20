using UnityEngine;
using System.Collections;

public class vars : MonoBehaviour {


	public static float minimum_way_point_distance = 1.5f;
	public static float way_point_interaction_circle_radius = 3.0f;
	public static float waypoint_interactition_latitude = 0.1f;

  public static float waypoint_node_connection_line_width = 0.2f;

	public static int unit_scout_amount = 10;
	public static string path_manager_name = "pah_manager";





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
