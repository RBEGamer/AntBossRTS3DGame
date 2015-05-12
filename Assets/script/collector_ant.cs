using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collector_ant : MonoBehaviour {

	public int connected_ressource_id;
	public int base_id;
	public bool start_walk_cyclus;



	public List<int> walk_path;


	wp_path_manager ant_path;


	public bool dir_sw;
	public int curr_dest_id;
	//-> get node id




	// Use this for initialization
	void Start () {
		ant_path = this.gameObject.GetComponent<wp_path_manager>();


	}
	
	// Update is called once per frame
	void Update () {
	

		if (dir_sw) {
			calc_way(0,3);
		} else {
			calc_way(3,0);
		}




	


	}



	private int get_node_id_by_ressource(int _rid){



		foreach (GameObject n in GameObject.Find(vars.path_manager_name).GetComponent<pathmanager>().nodes) {
		
			if(n.gameObject.GetComponent<node>().connected_with_res && n.gameObject.GetComponent<node>().connected_res_id == _rid){
			
				return n.GetComponent<node>().node_id;

			}
	}
		return -1;
	}











	









	public void calc_way(int _start, int _ziel){

		ant_path.start_node_id = _start;
		ant_path.ziel_node_id = _ziel;
		ant_path.compute_path ();
	}
	
	public node get_wp_comp(int _id){
		//verbessern
		return GameObject.Find(vars.wp_node_name +"_" + _id).gameObject.GetComponent<node>();
	}
	
	
	public Vector3 get_wp_pos(int _id){
		//verbessern
		return GameObject.Find(vars.wp_node_name +"_" + _id).gameObject.GetComponent<node>().node_pos;
	}





}
