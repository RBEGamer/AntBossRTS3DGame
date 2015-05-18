using UnityEngine;
using System.Collections;

public class ressource : MonoBehaviour {









	//ressource registreieren
	//ressourcemanager liste
	//name info : type, zugewiesene helfer daraus diese spawnen lassen, mesh state verwaltung,
	// Use this for initialization
	//-> node -> int connected to ressource id




	public int ressource_id;

	public vars.ressource_type res_type;

  public bool is_node_connected;

  public Vector3 ressource_pos;

  public GameObject circle_holder;


	public bool is_selected_by_res_manager;
	 public struct res_info{
		public float max_harvest;
		public int max_collector_ants;
		public float interaction_circle_radius;
		public float interactition_latitude;
		public float ant_bite_decrease;
		public float current_harvest_amount;
		public int target_collection_ants;
	}


	public res_info res;


	public void set_target_ants(int _target){
		res.target_collection_ants = _target;
	}


	public void set_res_info(){



	}

	void Start () {
		is_selected_by_res_manager = false;
    ressource_pos = this.gameObject.transform.position;
		res = new res_info();
		//res = vars.res_type_a;
		res.target_collection_ants = 5;
		//hier node registeren
		this.name = vars.res_name + "_" + ressource_id;
		refresh_res_info();


	}



	public float ant_bite(){
		res.current_harvest_amount -= res.ant_bite_decrease;
		if(res.current_harvest_amount < 0 ){res.current_harvest_amount = 0;}
		//Debug.Log("BITE RES " + ressource_id + " current harvest : " + res.current_harvest_amount);
		return res.ant_bite_decrease;
	}



	public void refresh_res_info(){
		switch (res_type) {
		case vars.ressource_type.A:
			res.max_harvest = vars.res_type_a.max_harvest;
			res.max_collector_ants = vars.res_type_a.max_collector_ants;
			res.interaction_circle_radius = vars.res_type_a.interaction_circle_radius;
			res.interactition_latitude= vars.res_type_a.interactition_latitude;
			res.ant_bite_decrease = vars.res_type_a.ant_bite_decrease;
			break;
		case vars.ressource_type.B:
			res.max_harvest = vars.res_type_b.max_harvest;
			res.max_collector_ants = vars.res_type_b.max_collector_ants;
			res.interaction_circle_radius = vars.res_type_b.interaction_circle_radius;
			res.interactition_latitude= vars.res_type_b.interactition_latitude;
			res.ant_bite_decrease = vars.res_type_b.ant_bite_decrease;
			break;
		case vars.ressource_type.C:
			res.max_harvest = vars.res_type_c.max_harvest;
			res.max_collector_ants = vars.res_type_c.max_collector_ants;
			res.interaction_circle_radius = vars.res_type_c.interaction_circle_radius;
			res.interactition_latitude= vars.res_type_c.interactition_latitude;
			res.ant_bite_decrease = vars.res_type_c.ant_bite_decrease;
			break;
		default:
			res.max_harvest = vars.res_type_default.max_harvest;
			res.max_collector_ants = vars.res_type_default.max_collector_ants;
			res.interaction_circle_radius = vars.res_type_default.interaction_circle_radius;
			res.interactition_latitude= vars.res_type_default.interactition_latitude;
			res.ant_bite_decrease = vars.res_type_default.ant_bite_decrease;
			break;
		}


		res.current_harvest_amount = res.max_harvest;
	}


	public int current_ants_working_on_this_res;
	// Update is called once per frame
	void Update () {


		res.target_collection_ants = current_ants_working_on_this_res;
		//hier schauen welcher node connected ist





    this.transform.position = ressource_pos;
    circle_holder.gameObject.GetComponent<selection_circle>().cirlce_offset = ressource_pos;
    if (!is_node_connected && vars.is_in_patheditmode && GameObject.Find(vars.path_manager_name).GetComponent<pathmanager>().count_selected_nodes() == 1)
    {
      circle_holder.gameObject.GetComponent<selection_circle>().circle_enabled = true;




		}else if(is_node_connected && !vars.is_in_patheditmode){


			//hier schauen ob geklickt
			/*
			if (Physics.Raycast (ray, out hit)) {
				//	is_mouse_in_node_range = c.draw_point_on_circle (hit.point);
				is_mouse_in_node_range = is_mouse_in_range(hit.point);
				curr_pos_in_circle = hit.point; //save the mouse pos fpr the pathmanager
				
				if(hit.collider.tag == vars.environment_tag || hit.collider.tag == vars.wp_node_tag){
					is_mouse_in_node_range = false;
				}

*/
    
    }
    else
    {
      circle_holder.gameObject.GetComponent<selection_circle>().circle_enabled = false;
    }


	}
}
