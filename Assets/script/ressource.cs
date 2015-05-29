﻿using UnityEngine;
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

	public GameObject click_collider;

	public bool is_selected_by_res_manager;
	 public struct res_info{
		public float max_harvest;
		public int max_collector_ants;
		public float interaction_circle_radius;
		public float interactition_latitude;
		public float ant_bite_decrease;
		public float current_harvest_amount;
		public int target_collection_ants;
		public int health_percentage;
		public string ui_displayname_ressource;
	}


	public res_info res;


	public GameObject res_a_model_complete;
	public GameObject res_a_model_bitten;

	public GameObject res_b_model_complete;
	public GameObject res_b_model_bitten;

	//public GameObject res_c_model_complete;
	//public GameObject res_c_model_bitten;

	public void set_target_ants(int _target){
		Debug.Log("set target ants resid:" + ressource_id + "   amount:" +_target);
		res.target_collection_ants = _target;
	}


	public void set_res_info(){



	}

	void Start () {
		click_collider.SetActive(false);
		is_selected_by_res_manager = false;
    ressource_pos = this.gameObject.transform.position;
		res = new res_info();
		//res = vars.res_type_a;
		res.target_collection_ants = 0;
		//hier node registeren
		this.name = vars.res_name + "_" + ressource_id;
		refresh_res_info();
		show_complete_model();



	}




	private void show_complete_model(){


		switch (res_type) {
		case vars.ressource_type.A:

			res_a_model_complete.SetActive(true);
			res_a_model_bitten.SetActive(false);
			res_b_model_complete.SetActive(false);
			res_b_model_bitten.SetActive(false);
			//res_c_model_complete.SetActive(false);
			//res_c_model_bitten.SetActive(false);
			break;
		case vars.ressource_type.B:
			res_a_model_complete.SetActive(false);
			res_a_model_bitten.SetActive(false);
			res_b_model_complete.SetActive(true);
			res_b_model_bitten.SetActive(false);
			//res_c_model_complete.SetActive(false);
			//res_c_model_bitten.SetActive(false);
			break;
		case vars.ressource_type.C:
			res_a_model_complete.SetActive(false);
			res_a_model_bitten.SetActive(false);
			res_b_model_complete.SetActive(false);
			res_b_model_bitten.SetActive(false);
			//res_c_model_complete.SetActive(true);
			//res_c_model_bitten.SetActive(false);
			break;
		default:
			res_a_model_complete.SetActive(false);
			res_a_model_bitten.SetActive(false);
			res_b_model_complete.SetActive(false);
			res_b_model_bitten.SetActive(false);
			//res_c_model_complete.SetActive(false);
			//res_c_model_bitten.SetActive(false);
			break;
		}

	}

	private void show_bitten_model(){


		switch (res_type) {
		case vars.ressource_type.A:
			res_a_model_complete.SetActive(false);
			res_a_model_bitten.SetActive(true);
			res_b_model_complete.SetActive(false);
			res_b_model_bitten.SetActive(false);
			//res_c_model_complete.SetActive(false);
			//res_c_model_bitten.SetActive(false);
			break;
		case vars.ressource_type.B:
			res_a_model_complete.SetActive(false);
			res_a_model_bitten.SetActive(false);
			res_b_model_complete.SetActive(false);
			res_b_model_bitten.SetActive(true);
			//res_c_model_complete.SetActive(false);
			//res_c_model_bitten.SetActive(false);
			break;
		case vars.ressource_type.C:
			res_a_model_complete.SetActive(false);
			res_a_model_bitten.SetActive(false);
			res_b_model_complete.SetActive(false);
			res_b_model_bitten.SetActive(false);
			//res_c_model_complete.SetActive(false);
			//res_c_model_bitten.SetActive(trues);
			break;
		default:
			res_a_model_complete.SetActive(false);
			res_a_model_bitten.SetActive(false);
			res_b_model_complete.SetActive(false);
			res_b_model_bitten.SetActive(false);
			//res_c_model_complete.SetActive(false);
			//res_c_model_bitten.SetActive(false);
			break;
		}


	}

	public float ant_bite(){
		res.current_harvest_amount -= res.ant_bite_decrease;
		if(res.current_harvest_amount < 0 ){res.current_harvest_amount = 0;}
		//Debug.Log("BITE RES " + ressource_id + " current harvest : " + res.current_harvest_amount);

		show_bitten_model();


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
			res.health_percentage = vars.res_type_a.health_percentage;
			res.ui_displayname_ressource = vars.res_type_a.ui_displayname_ressource;
			break;
		case vars.ressource_type.B:
			res.max_harvest = vars.res_type_b.max_harvest;
			res.max_collector_ants = vars.res_type_b.max_collector_ants;
			res.interaction_circle_radius = vars.res_type_b.interaction_circle_radius;
			res.interactition_latitude= vars.res_type_b.interactition_latitude;
			res.ant_bite_decrease = vars.res_type_b.ant_bite_decrease;
			res.health_percentage = vars.res_type_b.health_percentage;
			res.ui_displayname_ressource = vars.res_type_b.ui_displayname_ressource;
			break;
		case vars.ressource_type.C:
			res.max_harvest = vars.res_type_c.max_harvest;
			res.max_collector_ants = vars.res_type_c.max_collector_ants;
			res.interaction_circle_radius = vars.res_type_c.interaction_circle_radius;
			res.interactition_latitude= vars.res_type_c.interactition_latitude;
			res.ant_bite_decrease = vars.res_type_c.ant_bite_decrease;
			res.health_percentage = vars.res_type_c.health_percentage;
			res.ui_displayname_ressource = vars.res_type_c.ui_displayname_ressource;
			break;
		default:
			res.max_harvest = vars.res_type_default.max_harvest;
			res.max_collector_ants = vars.res_type_default.max_collector_ants;
			res.interaction_circle_radius = vars.res_type_default.interaction_circle_radius;
			res.interactition_latitude= vars.res_type_default.interactition_latitude;
			res.ant_bite_decrease = vars.res_type_default.ant_bite_decrease;
			res.health_percentage = vars.res_type_default.health_percentage;
			res.ui_displayname_ressource = vars.res_type_default.ui_displayname_ressource;
			break;
		}


		res.current_harvest_amount = res.max_harvest;
	
	}


	public int current_ants_working_on_this_res;
	// Update is called once per frame
	void Update () {


		//res.target_collection_ants = current_ants_working_on_this_res;
		//hier schauen welcher node connected ist



		if(	res.health_percentage <= 0){

			//wp löschen -> beim vorherigen den next auf -1
			//node diconnecteten
			//is node connected = 0;

			//enum anlegen res_state -> healthy, destoryed, attacked, nothing-> delete



		}




    this.transform.position = ressource_pos;
    circle_holder.gameObject.GetComponent<selection_circle>().cirlce_offset = ressource_pos;
    if (!is_node_connected && vars.is_in_patheditmode && GameObject.Find(vars.path_manager_name).GetComponent<pathmanager>().count_selected_nodes() == 1)
    {
      circle_holder.gameObject.GetComponent<selection_circle>().circle_enabled = true;




		}else if(is_node_connected && !vars.is_in_patheditmode){


    
			if(is_selected_by_res_manager){
				circle_holder.gameObject.GetComponent<selection_circle>().circle_enabled = true;
			}else{
				circle_holder.gameObject.GetComponent<selection_circle>().circle_enabled = false;
			}


    }
    else
    {
      circle_holder.gameObject.GetComponent<selection_circle>().circle_enabled = false;
    }


	}


}
