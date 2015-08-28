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
	

		//wenn nicht in patheditmode schaue ob a) 1.selectiert -> mapp ui   b) wenn mehr als 1 deselect all  c) keine schaue ob über einer geklickt wurde wenn auf keine dann deselect all
		//wenn selektiert dann



		if(!vars.is_in_patheditmode || vars.is_in_patheditmode){

	
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;



			if(count_selected_ressources() == 0 || count_selected_ressources() == 1){
				if(Input.GetMouseButtonDown(0)){

					enable_all_collider();
				if (Physics.Raycast (ray, out hit)) {
						foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {
							if(hit.collider.gameObject == n.gameObject.GetComponent<ressource>().click_collider.gameObject){
							//	n.gameObject.GetComponent<ressource>().is_selected_by_res_manager = true;
								//-> ich würde das hier einfach nur mappen
								map_ui_to_ressource(n.gameObject.GetComponent<ressource>().ressource_id);

						}
						}

						//if(hit.collider.gameObject.tag == vars.environment_tag || hit.collider.gameObject.tag == vars.ground_tag){deselect_all_ressources();}

					}//ende raycast
					disable_all_collider();
				}//ende mousbutton



				//if(count_selected_ressources() > 1){deselect_all_ressources();}
		}else{
				//if(count_selected_ressources() > 0){deselect_all_ressources();disable_all_collider();}
				if (Physics.Raycast (ray, out hit) && count_selected_ressources() > 1) {
					if(hit.collider.gameObject.tag == vars.environment_tag || hit.collider.gameObject.tag == vars.ground_tag){deselect_all_ressources();}
				}
		}
	}//ende func






		//nur alle 100 frames oder so
		if(current_frame_count >= vars.res_manager_ant_spawn_speed){
		current_frame_count = 0;
		//	Debug.Log("res manager tick");
		manage_ant_amount();
		}else{
		current_frame_count++;
		}
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
					GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().refresh_ressource_ui_slots();
				coll_ant.set_walking_state();
				coll_ant.connected_ressource = _res_id;
				coll_ant.res_updated = true;
					new_ant_instance = null;
				}else if(_current_working_ants > _target_ants){
					foreach (GameObject nd in GameObject.FindGameObjectsWithTag(vars.collector_ant_tag)) {
						if(nd.gameObject.GetComponent<collector_ant>().connected_ressource == _res_id && calc_ant_diff(_res_id) >= 1 && nd.gameObject.GetComponent<collector_ant>().is_walking()){
							nd.gameObject.GetComponent<collector_ant>().set_destroy_state();
						}
					}
				//break;
				}else{ // if(_current_working_ants == _target_ants){
					//nichts machen alles gut
			}//end check if res connected
			//sonst alle ants löschen
		}//ende foreach
	}
}




	public int calc_ant_diff(int _resid){
		int _current_working_ants = 0, _target_ants =0, ant_diff = 0;
		foreach (GameObject	 n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {
			if(n.gameObject.GetComponent<ressource>().ressource_id == _resid){
				_current_working_ants = count_ants_by_ressource(_resid);
				_target_ants = n.gameObject.GetComponent<ressource>().res.target_collection_ants;
				ant_diff = _current_working_ants - _target_ants; 
				break;
			}
		}
		return ant_diff;
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


	public int count_ants_by_ressource(int _resid, bool with_walking_state_check = true){
		int _count = 0;
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.collector_ant_tag)) {
			if(with_walking_state_check){
				if(n.gameObject.GetComponent<collector_ant>().connected_ressource == _resid && (n.gameObject.GetComponent<collector_ant>().is_walking() || n.gameObject.GetComponent<collector_ant>().is_collectiong())){
					_count++;
				}
			}else{
				if(n.gameObject.GetComponent<collector_ant>().connected_ressource == _resid){
					_count++;
				}
			}
		}
		return _count;
	}
	

	public int count_ants(){
		int _count = 0;
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.collector_ant_tag)) {
				_count++;
		}
		return _count;
	}





	//alle diese beziehen sich auf wenn man nicht im patheditmode ist

	public int count_selected_ressources(){
		int _count = 0;
		if(GameObject.FindGameObjectsWithTag(vars.collector_ant_tag) != null){
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {
			if(n.GetComponent<ressource>().is_selected_by_res_manager){
				_count++;
			}
			}
		}
		return _count;
	}

	//first selected ressource
	public GameObject get_selected_ressource(){
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {
			if(n.GetComponent<ressource>().is_selected_by_res_manager){
				return n;
			}
		}
		return null;
	}


	public void deselect_all_ressources(){
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {
			n.GetComponent<ressource>().is_selected_by_res_manager = false;
		}
	}


	public void select_all_ressources(){
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {
			n.GetComponent<ressource>().is_selected_by_res_manager = true;
		}
	}


	public void enable_all_collider(){
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {
			n.GetComponent<ressource>().click_collider.SetActive(true);
		}
	}

	public void disable_all_collider(){
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {
			n.GetComponent<ressource>().click_collider.SetActive(false);
		}
	}



	public void map_ui_to_ressource(int _resid){
		//-> rid an den ui manager übergeben
	//	Debug.Log (" MAP RES TO UI : " + _resid);
		//GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().connected_res_to_ui = _resid;
		GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().slot_0_set_ressource();
	//s	GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().refresh_ressource_ui();
	
	}


	public int count_target_ant_amount(){
	int _counter = 0;
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {
			_counter += n.gameObject.GetComponent<ressource>().res.target_collection_ants;
		}


		return 0;
	}
}
