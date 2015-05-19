using UnityEngine;
using System.Collections;

public class base_manager : MonoBehaviour {

	public float res_a_storage;
	public float res_b_storage;
	public float res_c_storage;

	public int bought_collector_ants = 7;
	public int avariable_collector_ants;
	// Use this for initialization
	void Awake () {
		this.name = vars.base_name;
	}


	void Start(){

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		avariable_collector_ants = bought_collector_ants - GameObject.Find(vars.ressource_manager_name).GetComponent<ressource_manager>().count_ants();




	}



	public void loose_percentage_value_of_storage(vars.ressource_type _type, int percentage){
		float npercentage = percentage/100;
		npercentage = 1- npercentage;
		switch (_type) {
		case vars.ressource_type.A:
			res_a_storage = res_a_storage * npercentage;
			if(res_a_storage < 0.0f){res_a_storage = 0.0f;}
			break;
		case vars.ressource_type.B:
			res_a_storage = res_b_storage * npercentage;
			if(res_b_storage < 0.0f){res_b_storage = 0.0f;}
			break;
		case vars.ressource_type.C:
			res_a_storage = res_c_storage * npercentage;
			if(res_c_storage < 0.0f){res_c_storage = 0.0f;}
			break;
		default:
			break;
		}
	}

	public void add_to_storage(vars.ressource_type _type, float _amount){
		switch (_type) {
			case vars.ressource_type.A:
			res_a_storage += _amount;
			break;

		case vars.ressource_type.B:
			res_b_storage += _amount;
			break;

		case vars.ressource_type.C:
			res_c_storage += _amount;
			break;
		default:
			break;
		}
	}
}
