﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collector_ant : MonoBehaviour {

	wp_path_manager ant_path;
	public List<int> wp;
	//-> get node id

	List<GameObject> count_list;


	// Use this for initialization
	void Start () {
		ant_path = this.gameObject.GetComponent<wp_path_manager>();
	
	
	}
	
	// Update is called once per frame
	void Update () {
		calc_way();

	





	}



	private int get_node_id_by_ressource(int _rid){



		foreach (GameObject n in GameObject.Find(vars.path_manager_name).GetComponent<pathmanager>().nodes) {
		
			if(n.gameObject.GetComponent<node>().connected_with_res && n.gameObject.GetComponent<node>().connected_res_id == _rid){
			
				return n.GetComponent<node>().node_id;

			}
	}
		return -1;
	}











	









	public void calc_way(){


//		count_list.Clear();
//		count_list.AddRange(GameObject.FindGameObjectsWithTag(vars.wp_node_tag));

		if(GameObject.Find(vars.path_manager_name).GetComponent<pathmanager>().nodes.Count >= 2){

			wp.Clear();
			ant_path.start_node_id = 0;
			ant_path.ziel_node_id = 1;
			ant_path.add_nodes();
			ant_path.compute_path();


			wp = ant_path.final_path;




		}

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
