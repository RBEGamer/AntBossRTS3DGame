using UnityEngine;
using System.Collections;

public class ressource_manager : MonoBehaviour {

	public GameObject collection_ant_template;



	private int current_frame_count;

//	GUIText sel_res_text;
	// Use this for initialization
	void Start () {
		current_frame_count = 0;
		this.name = vars.ressource_manager_name;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	

		//nur alle 10 frames oder so
		if(current_frame_count >= vars.res_manager_ant_spawn_speed){
		current_frame_count = 0;
		//	Debug.Log("res manager tick");
		manage_ant_amount();
		}else{
		current_frame_count++;
		}
	//	sel_res_text = "0";


		//wenn ressource connected wurde oder amount geändert wurde 
		//schaue wie viele collectors zugewiesen sind wenn duviele dann anpassen bzw diese in den sleep modus schicken
		//per ant_sctivit state
		//wenn auf aktive geschltet wir res updatesaufgeru
	}


	public void manage_ant_amount(){


		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {

			int _max_ants = n.gameObject.GetComponent<ressource>().res.max_collector_ants;
			int _res_id = n.GetComponent<ressource>().ressource_id;
			int _current_working_ants = count_ants_by_ressource(_res_id);
			int _target_ants = n.gameObject.GetComponent<ressource>().res.target_collection_ants;


			if(_target_ants >= _max_ants){_target_ants = _max_ants;}

			if(check_if_any_node_connected(_res_id)){

			if(_current_working_ants < _target_ants){
				Debug.Log("spawn coll ant");
				GameObject new_ant_instance = (GameObject)Instantiate(collection_ant_template, GameObject.Find(vars.sleep_pos_manager_name).gameObject.GetComponent<sleep_pos_manager>().get_sleeping_pos(), Quaternion.identity);
				collector_ant coll_ant = 	new_ant_instance.gameObject.GetComponent<collector_ant>();
				coll_ant.set_walking_state();
				coll_ant.connected_ressource = _res_id;
				coll_ant.res_updated = true;



					new_ant_instance = null;
					break;
				}else if(_current_working_ants > _target_ants){

					//hier ants löschen

				}





			}//end check if res connected
			//sonst alle ants löschen
		}//ende foreach


	}

	public bool check_if_any_node_connected(int _rid){
		//for each node if coonected ant res_id = _rid

		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.wp_node_tag)) {
			if(n.gameObject.GetComponent<node>().connected_with_res && n.gameObject.GetComponent<node>().discoveres_by_scout && n.gameObject.GetComponent<node>().connected_res_id == _rid){
				return true;
			}
		}
		return false;
	}


	public int count_ants_by_ressource(int _resid){
		int _count = 0;
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.collector_ant_tag)) {
			if(n.gameObject.GetComponent<collector_ant>().connected_ressource == _resid){
				_count++;
			}
		}
		return _count;
	}
	
	public int count_ants(){
		return 0;
	}

}
