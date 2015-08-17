using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class path_point : MonoBehaviour {

	public	enum node_type
	{
		normal_node, base_node, res_node
	}




	public static wp_manager wpManager;
	public node_type type;
	public int waypoint_id;
	//public bool accessable = false;
	public bool is_selected = false;
	public bool erobert = false;


	public GameObject circle_holder;


	//public bool visited;
	//public float distance;
	//public int ancestor;
	//public bool tagged;



	public List<int> path_to_base; //immer ausgehend vom base node die nodes id zu diesem node
	public void add_path_to_node(List<int> path){
		path_to_base.Clear();
		path_to_base.AddRange(path);

	}




	


	public void enable_collider(){
		if(type == node_type.normal_node){
		if(!this.GetComponent<CapsuleCollider>().enabled){
			this.GetComponent<CapsuleCollider>().enabled = true;
		}
		}
	}

	public void disabled_collider(){
		if(type == node_type.normal_node){
		if(this.GetComponent<CapsuleCollider>().enabled){
			this.GetComponent<CapsuleCollider>().enabled = false;
		}
		}
	}



	public void enable_range_cycle(){
		if(type == node_type.normal_node){
		if(!circle_holder.activeSelf){
			circle_holder.SetActive(true);
			}
		}

	}

	public void disable_range_cycle(){
		if(type == node_type.normal_node){
		if(circle_holder.activeSelf){
			circle_holder.SetActive(false);
			}
		}
	}

	// Use this for initialization
	void Start () {

		if(!wpManager) {
			wpManager = GameObject.Find(vars.path_manager_name).GetComponent<wp_manager>();
		}
		wpManager.addNodeObject(this.gameObject);

		if(type == node_type.res_node){
			wpManager.addResObject(this.gameObject);
		}else if( type == node_type.base_node){
			wpManager.addBaseObject(this.gameObject);
		}


		circle_holder.SetActive(false);
	
		if(type == node_type.base_node){
			erobert = true;
			this.waypoint_id  = 1;
		}else{
		this.waypoint_id = wpManager.get_wp_id();
		}
		//disabled_collider();
		//this.name = "node_" + waypoint_id.ToString();

	}
	
	// Update is called once per frame
	void Update () {
	



	}
}
