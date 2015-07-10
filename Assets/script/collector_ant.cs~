using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collector_ant : MonoBehaviour {

	public float ant_bite_size;


	public float collectiong_counter;

	private Material ant_material;
	public Texture ant_texture_normal;
	public Texture ant_texture_loaded;

	public GameObject model_holder;
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


	public float toggle_distance_a = 0.1f;
	public float toggle_distance_b = 0.1f;

	public int health = 10;

	public void receiveDamage(int damage) {
		health -= damage;
		if(health <= 0) {
			GameObject.Find(vars.base_tag).GetComponent<base_manager>().bought_collector_ants -=1;
			GameObject.Find(vars.res_tag + "_" + connected_ressource).GetComponent<ressource>().res.target_collection_ants -= 1;
			Destroy(gameObject);

		}
	}

	public enum ant_activity_state
	{
		sleep,walking,destroy, collecting
	}

	public  ant_activity_state ant_state;






	public void set_walking_state(){
		ant_state = ant_activity_state.walking;
		model_holder.SetActive(true);
	}

	public void set_sleep_state(){
		ant_state = ant_activity_state.sleep;
		model_holder.SetActive(false);
	}

	public void set_destroy_state(){
		ant_state = ant_activity_state.destroy;
		model_holder.SetActive(false);
	}
	public void set_collection_state(){
		ant_state = ant_activity_state.collecting;
		model_holder.SetActive(true);
	}


	public bool is_walking(){
		if(ant_state == ant_activity_state.walking){
			return true;
		}else{
			return false;
		}
	}

	public bool is_sleeping(){
		if(ant_state == ant_activity_state.sleep){
			return true;
		}else{
			return false;
		}
	}


	public bool is_destroyed(){
		if(ant_state == ant_activity_state.destroy){
			return true;
		}else{
			return false;
		}
	}

	public bool is_collectiong(){
		if(ant_state == ant_activity_state.collecting){
			return true;
		}else{
			return false;
		}
	}





	void Awake(){
		ant_material = new Material(Shader.Find("Diffuse"));
		if(model_holder.GetComponent<Renderer>() != null){
		model_holder.GetComponent<Renderer>().material = ant_material;
		}
	//material zuweisen
	}

	// Use this for initialization
	void Start () {
		ant_bite_size = 0.0f;
		ant_path = this.gameObject.GetComponent<wp_path_manager>();
		this.name = vars.collector_ant_name;
		if(ant_state == ant_activity_state.sleep){
			//invisible schalten
			model_holder.SetActive(false);
			this.transform.position = GameObject.Find(vars.sleep_pos_manager_name).GetComponent<sleep_pos_manager>().get_sleeping_pos();
		}else if(ant_state == ant_activity_state.walking){
			model_holder.SetActive(true);
			sw_path();
		}



		//sw_path();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(res_updated){
			res_updated = false;
			is_hinweg = true;
			wp_counter = 0;
			ant_bite_size = 0.0f;
			sw_path();
		}



		if(ant_bite_size > 0){
			ant_material.mainTexture = ant_texture_loaded;
		}else{
			ant_material.mainTexture = ant_texture_normal;
		}



		if(walk_path.Count > 1 ){



			float step = vars.collector_ant_speed * Time.deltaTime*ant_walk_path_distance;

			transform.position = Vector3.MoveTowards(transform.position, get_wp_pos(walk_path[wp_counter]), step);
			//Quaternion rot = Quaternion.LookRotation(get_wp_pos(walk_path[wp_counter]));
			this.transform.LookAt(get_wp_pos(walk_path[wp_counter]));


			if(is_collectiong() ){

				collectiong_counter -= Time.deltaTime;
				if(collectiong_counter <= 0.0f){
				ant_bite_size =	GameObject.Find(vars.res_name + "_" + connected_ressource).GetComponent<ressource>().ant_bite();
				sw_path();
				set_walking_state();
				}
			}



		




			//Debug.Log(wp_counter);
			if(wp_counter >= walk_path.Count-1 && walk_path.Count >= 2 && walk_path[wp_counter] == _saved_dest){

				if(is_hinweg){
					toggle_distance_b = 1f;
				}else{
					toggle_distance_b = 2f;
				}


				if(Vector3.Distance(get_wp_pos(walk_path[wp_counter]),transform.position) < toggle_distance_b){
					
					
					
					
					//GameObject.Find(vars.wp_node_name +"_" + walk_path[wp_counter]).gameObject.GetComponent<node>().discoveres_by_scout = true;


					//hier je nach richtun aktion ausführen


					if(!is_hinweg && is_walking()){

						switch (GameObject.Find(vars.res_name + "_" + connected_ressource.ToString()).GetComponent<ressource>().res_type) {
						case vars.ressource_type.A:
						collectiong_counter = vars.res_type_a.harwesting_time;
						break;
						case vars.ressource_type.B:
							collectiong_counter = vars.res_type_b.harwesting_time;
							break;
						case vars.ressource_type.C:
							collectiong_counter = vars.res_type_c.harwesting_time;
							break;
						case vars.ressource_type.default_type:
							collectiong_counter = vars.res_type_default.harwesting_time;
							break;

						default:
						break;
						}

						set_collection_state();

					}else{

						//Debug.Log("entered base");
						if(ant_state == ant_activity_state.sleep){
						//	Debug.Log(" goto sleep");
							this.transform.position = GameObject.Find(vars.sleep_pos_manager_name).GetComponent<sleep_pos_manager>().get_sleeping_pos();
							model_holder.SetActive(false);
						}else if(ant_state == ant_activity_state.destroy){
						//	Debug.Log(" destroy");
							Destroy(this.gameObject);
						}else if (ant_state == ant_activity_state.walking){
						//	Debug.Log(" switch dir");


						



					//		GameObject.Find(vars.base_name).gameObject.GetComponent<base_manager>().add_to_storage(vars.ressource_type.A, ant_bite_size);
							//base.restype_a_storage += ant_bite_size;
								GameObject.Find(vars.base_name).GetComponent<base_manager>().add_to_storage(get_res_type_by_id(connected_ressource),ant_bite_size);
							ant_bite_size = 0.0f;
							sw_path();
						}


					}





				}
				
			}
			



			
			 if(Vector3.Distance(get_wp_pos(current_wp_step),transform.position) < toggle_distance_a && wp_counter < walk_path.Count-1){//&& current_wp_step == wp_counter){
				wp_counter++;
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



	private vars.ressource_type get_res_type_by_id(int _rid){
								foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {
									if(n.gameObject.GetComponent<ressource>().ressource_id == _rid){
										return	n.gameObject.GetComponent<ressource>().res_type;
									}
								}
								return vars.ressource_type.default_type;
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
			model_holder.SetActive(true);
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
			//Debug.Log("calc way complete");


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
			model_holder.SetActive(false);
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
