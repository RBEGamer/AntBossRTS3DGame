﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collector_ant : MonoBehaviour {

	public int connected_ressource;
	public int wp_start;
	public int wp_end;
	public bool is_hinweg;
	public int _saved_dest;
	wp_path_manager ant_path;
	public List<int> walk_path;
	//-> get node id
	private float ant_walk_path_distance;
	public bool res_updated;
	public int current_wp_step;
	public int wp_counter;
	List<GameObject> count_list;




	// Use this for initialization
	void Start () {
		ant_path = this.gameObject.GetComponent<wp_path_manager>();

		sw_path();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(res_updated){
			res_updated = false;
			is_hinweg = true;
			wp_counter = 0;
			sw_path();
		}



		if(walk_path.Count > 1){



			float step = vars.collector_ant_speed * Time.deltaTime*ant_walk_path_distance;

			transform.position = Vector3.MoveTowards(transform.position, get_wp_pos(walk_path[wp_counter]), step);
			//Quaternion rot = Quaternion.LookRotation(get_wp_pos(walk_path[wp_counter]));
			this.transform.LookAt(get_wp_pos(walk_path[wp_counter]));






			//Debug.Log(wp_counter);
			if(wp_counter >= walk_path.Count-1 && walk_path.Count >= 2 && walk_path[wp_counter] == _saved_dest){
				if(Vector3.Distance(get_wp_pos(walk_path[wp_counter]),transform.position) < 0.1f){
					
					
					
					
					//GameObject.Find(vars.wp_node_name +"_" + walk_path[wp_counter]).gameObject.GetComponent<node>().discoveres_by_scout = true;


					//hier je nach richtun aktion ausführen


					if(is_hinweg){
						GameObject.Find(vars.res_name + "_" + connected_ressource).GetComponent<ressource>().ant_bite();

					}else{

					}
					sw_path();




				}
				
			}
			
			
			
			
			if(Vector3.Distance(get_wp_pos(current_wp_step),transform.position) < 0.1f ){//&& current_wp_step == wp_counter){
				if(wp_counter < walk_path.Count-1){
					wp_counter ++;
				}
				current_wp_step = walk_path[wp_counter];
			}













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











	
	public void sw_path(){


		if(get_node_id_by_ressource(connected_ressource) >= 0){
			wp_end = get_node_id_by_ressource(connected_ressource); 
			wp_start = 0;
		if(is_hinweg){
			
			calc_way(wp_start, wp_end);

		}else{
			
			calc_way( wp_end, wp_start);
		}
			is_hinweg = !is_hinweg;
		}
	}








	public void calc_way(int _start, int _goal){


//		count_list.Clear();
//		count_list.AddRange(GameObject.FindGameObjectsWithTag(vars.wp_node_tag));

		if(GameObject.Find(vars.path_manager_name).GetComponent<pathmanager>().nodes.Count >= 2 && get_wp_comp(_start).discoveres_by_scout && get_wp_comp(_goal).discoveres_by_scout){
		//	this.gameObject.GetComponent<Renderer>().enabled = true;
			wp_counter = 0;
			walk_path.Clear();
			ant_path.start_node_id = _start;
			ant_path.ziel_node_id = _goal;
			ant_path.add_nodes();
			ant_path.compute_path();
			ant_path.final_path.Reverse(); //umdrehen denn die lsite ist immer falschrum
			walk_path = ant_path.final_path;
			this.transform.position = get_wp_pos(_start);
			_saved_dest = _goal;
			Debug.Log("calc way complete");


			if(vars.collector_ant_en_dyn_speed){
				for (int i = 0; i < walk_path.Count; i++) {
					if(i > 0){
						ant_walk_path_distance += Vector3.Distance(get_wp_pos(walk_path[i-1]),get_wp_pos(walk_path[i]));
					}		
				}
			}else{
				ant_walk_path_distance = 1.0f;
			}



		}else{
			walk_path.Clear();
			this.transform.position = get_wp_pos(0); //zur base setzten
		//	this.gameObject.GetComponent<Renderer>().enabled = false;
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
