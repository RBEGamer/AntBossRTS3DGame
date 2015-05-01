using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ant_path_walker : MonoBehaviour {


	//Hier wird die ameise zum letzten hinzugefügten wp gehen und dort verscheinden
	public int last_added_node;
	public List<int> walk_path;

	public float speed;
	public int current_wp_step;
	wp_path_manager ant_path;

	public int wp_counter;
	// Use this for initialization
	void Start () {
		ant_path = this.GetComponent<wp_path_manager>();
		ant_path.start_node_id = 0; // der start ist immer die base also 0;
		this.transform.position = get_wp_pos(ant_path.start_node_id);

		node_added();
	}
	
	// Update is called once per frame
	void Update () {
		if(walk_path.Count >= 2){
		float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, get_wp_pos(walk_path[wp_counter]), step);



		//if(current_wp_step == walk_path.Count &&  walk_path.Count-1 >0){
		//	Debug.Log("count = wp");
		//}else

			Debug.Log(wp_counter);
		if(wp_counter >= walk_path.Count-1 && walk_path.Count >= 2 && walk_path[wp_counter] == last_added_node){
				if(Vector3.Distance(get_wp_pos(walk_path[wp_counter]),transform.position) < 0.1f){
					Destroy(this.gameObject);
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



	public void node_added(){
		wp_counter = 0;
		ant_path.start_node_id = 0; // der start ist immer die base also 0;
		this.transform.position = get_wp_pos(ant_path.start_node_id);
		last_added_node =  GameObject.Find(vars.path_manager_name).GetComponent<pathmanager>().last_added_wp;
		ant_path.ziel_node_id = last_added_node;	
		ant_path.compute_path();
		walk_path = ant_path.final_path;
		walk_path.Reverse();
		current_wp_step = walk_path[wp_counter];


	//	current_wp_step = ant_path.start_node_id;

	}

	public Vector3 get_wp_pos(int _id){
		//verbessern
		return GameObject.Find(vars.wp_node_name +"_" + _id).gameObject.GetComponent<node>().node_pos;



	}
}
