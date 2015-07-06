using UnityEngine;
using System.Collections;

public class base_manager : MonoBehaviour {

	public GameObject click_collider;

	public float res_a_storage;
	public float res_b_storage;
	public float res_c_storage;

	public int base_health_percentage = 0;
	public int health = 0;
	

	public int bought_collector_ants = 0;
	public int bought_attack_ants = 0;
	public int bought_scout_ants = 0;

	public GameObject upgrade_slot_0;
	public GameObject upgrade_slot_1;
	public GameObject upgrade_slot_2;
	public GameObject upgrade_slot_3;




	public bool add_upgrade(GameObject upgrade){
		Debug.Log("call add");
		if(upgrade.GetComponent<upgrade_description>() != null && !upgrade.GetComponent<upgrade_description>().taken && upgrade.GetComponent<upgrade_description>().active & upgrade.GetComponent<upgrade_description>().upgrade_type == vars.upgrade_type.ant_base){
			if(res_a_storage >= upgrade.GetComponent<upgrade_description>().costs_res_a && res_b_storage >= upgrade.GetComponent<upgrade_description>().costs_res_b && res_c_storage >= upgrade.GetComponent<upgrade_description>().costs_res_c){

				if(upgrade_slot_0 != null || upgrade_slot_0 != null || upgrade_slot_0 != null || upgrade_slot_0 != null){

					upgrade.GetComponent<upgrade_description>().taken = true;
					res_a_storage -= upgrade.GetComponent<upgrade_description>().costs_res_a;
					res_b_storage -= upgrade.GetComponent<upgrade_description>().costs_res_b;
					res_c_storage -= upgrade.GetComponent<upgrade_description>().costs_res_c;


					switch (upgrade.GetComponent<upgrade_description>().upgrade_add_to_value) {
					case vars.upgrade_values.lagerplatz_res_all:
						vars.max_storage_res_a += (int)upgrade.GetComponent<upgrade_description>().increase_value;
						vars.max_storage_res_b += (int)upgrade.GetComponent<upgrade_description>().increase_value;
						vars.max_storage_res_c += (int)upgrade.GetComponent<upgrade_description>().increase_value;
						break;
					case vars.upgrade_values.lagerplatz_res_a:
						vars.max_storage_res_a += (int)upgrade.GetComponent<upgrade_description>().increase_value;
						break;
					case vars.upgrade_values.lagerplatz_res_b:
						vars.max_storage_res_b += (int)upgrade.GetComponent<upgrade_description>().increase_value;
						break;
					case vars.upgrade_values.lagerplatz_res_c:
						vars.max_storage_res_c += (int)upgrade.GetComponent<upgrade_description>().increase_value;
						break;
					case vars.upgrade_values.leben:
						health += (int)upgrade.GetComponent<upgrade_description>().increase_value;
						break;
					default:
					break;
					}

				if(upgrade_slot_0 != null){
					upgrade_slot_0 = upgrade;
				}else 	if(upgrade_slot_1 != null){
					upgrade_slot_1 = upgrade;
				}else 	if(upgrade_slot_2 != null){
					upgrade_slot_2 = upgrade;
				}else 	if(upgrade_slot_3 != null){
					upgrade_slot_3 = upgrade;
				}
				




					Debug.Log("upgrade taken");
					return true;
			}
			}else{
				Debug.Log("err 2");
			}

		}else{
			Debug.Log("err 1");
		}
		return false;
}






	public int avariable_collector_ants;
	// Use this for initialization
	void Awake () {
		this.name = vars.base_name;

	}

	public void receiveDamage(int damage) {
		health -= damage;
		if(health <= 0) {
			Destroy(gameObject);

		}
	}
	void Start(){
		res_a_storage = vars.initial_res_a_storage;
		res_b_storage = vars.initial_res_b_storage;
		res_c_storage = vars.initial_res_c_storage;
		bought_collector_ants = vars.initial_collector_ants;
		bought_attack_ants = vars.initial_attack_ants;
		bought_scout_ants = vars.initial_scout_ants;
		base_health_percentage = vars.base_start_health_percentage;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	

		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			//enable_all_collider();
			if (Physics.Raycast (ray, out hit)) {

				if(hit.collider.gameObject == click_collider.gameObject){
					GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().slot_0_set_base();
						//	n.gameObject.GetComponent<ressource>().is_selected_by_res_manager = true;
						//-> ich würde das hier einfach nur mappen
						//map_ui_to_ressource(n.gameObject.GetComponent<ressource>().ressource_id);
						
					}

				
				//if(hit.collider.gameObject.tag == vars.environment_tag || hit.collider.gameObject.tag == vars.ground_tag){deselect_all_ressources();}
				
			}//ende raycast
			//disable_all_collider();
		}//ende mousbutton



	}

	public void calc_avariable_collecotr_ats(){
		if(GameObject.Find(vars.ressource_manager_name) != null){
		//avariable_collector_ants = bought_collector_ants - GameObject.Find(vars.ressource_manager_name).GetComponent<ressource_manager>().count_ants();
			avariable_collector_ants = bought_collector_ants - GameObject.Find(vars.ressource_manager_name).GetComponent<ressource_manager>().count_target_ant_amount();
		}else{
			avariable_collector_ants = 0;
		}
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
			if(res_a_storage > vars.max_storage_res_a){res_a_storage = vars.max_storage_res_a;}
			break;

		case vars.ressource_type.B:
			res_b_storage += _amount;
			if(res_b_storage > vars.max_storage_res_b){res_b_storage = vars.max_storage_res_b;}
			break;

		case vars.ressource_type.C:
			res_c_storage += _amount;
			if(res_c_storage > vars.max_storage_res_c){res_c_storage = vars.max_storage_res_c;}
			break;
		default:
			break;
		}
	}
}
