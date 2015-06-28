using UnityEngine;
using System.Collections;

public class base_manager : MonoBehaviour {

	public GameObject click_collider;

	public float res_a_storage;
	public float res_b_storage;
	public float res_c_storage;

	public int base_health_percentage = 0;
	public int health = 50;




	public int bought_collector_ants = 0;
	public int bought_attack_ants = 0;
	public int bought_scout_ants = 0;




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
