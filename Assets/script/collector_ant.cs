using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collector_ant : MonoBehaviour {

	public int connected_ressource_id;
	public int base_id;
	public bool start_walk_cyclus;



	public List<int> walk_path;
	public float ant_walk_path_distance;
	private bool ant_dynamic_walk_speed = false;
	private float ant_move_speed = 3.0f;
	public int current_wp_step;
	wp_path_manager ant_path;
	public int wp_counter;

	public bool dir_sw;
	public int curr_dest_id;
	//-> get node id




	// Use this for initialization
	void Start () {
		ant_path = this.gameObject.GetComponent<wp_path_manager>();
		dir_sw = true;
		//sw_dir ();
		calc_way ();

	}
	
	// Update is called once per frame
	void Update () {
	

	



		if(walk_path.Count >= 2){
			
			
			
			
			
			
			
			float step = ant_move_speed * Time.deltaTime*ant_walk_path_distance;
			transform.position = Vector3.MoveTowards(transform.position, get_wp_pos(walk_path[wp_counter]), step);
			this.transform.LookAt(get_wp_pos(walk_path[wp_counter]));

			

			
			
			
			

			if(wp_counter >= walk_path.Count-1 && walk_path.Count >= 2 && walk_path[wp_counter] == curr_dest_id){
				if(Vector3.Distance(get_wp_pos(walk_path[wp_counter]),transform.position) < 0.1f){
					
					
					

					//sw_dir();

					walk_path.Reverse();
					curr_dest_id = walk_path[wp_counter];
					wp_counter = 0;
					dir_sw = !dir_sw;
				
				}
				
			}
			
			
			
			
			if(Vector3.Distance(get_wp_pos(current_wp_step),transform.position) < 0.1f ){//&& current_wp_step == wp_counter){
				
				if(wp_counter < walk_path.Count-1){
					wp_counter ++;
				}
				
				current_wp_step = walk_path[wp_counter];
			}
			
			
		}//ende walk count


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
		wp_counter = 0;
		int _ziel = 3;
		int _start = 0;
		curr_dest_id = _ziel;
		ant_path.start_node_id = _start; // der start ist immer die base also 0;
		ant_path.ziel_node_id = _ziel;	
		this.transform.position = get_wp_pos(_start);
	
		ant_path.compute_path();
		walk_path.Clear ();

		if (ant_path.final_path.Count >= 2) {
			walk_path = ant_path.final_path;



			walk_path.Reverse ();
			current_wp_step = walk_path [wp_counter];
			//Dynamic WALK SPEED CALCULATioN
			ant_walk_path_distance = 0;
			if (ant_dynamic_walk_speed) {
				for (int i = 0; i < walk_path.Count; i++) {
					if (i > 0) {
						ant_walk_path_distance += Vector3.Distance (get_wp_pos (walk_path [i - 1]), get_wp_pos (walk_path [i]));
					}		
				}
			} else {
				ant_walk_path_distance = 1.0f;
			}
		
		
			//	current_wp_step = ant_path.start_node_id;
		} else {

			Debug.Log("err array count");
			Destroy(this.gameObject);
		}//ende >2
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
