using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class objectives_manager : MonoBehaviour {



	public GameObject objective_holder_0;
	public GameObject objective_holder_1;
	public GameObject objective_holder_2;

	public GameObject obejctives_text_holder;
	public GameObject title_background_holder;

	public bool objectives_visible;



	public GameObject objectives_btn_text;
	public List<objective_desc> objectives = new List<objective_desc>();

	public List<objective_desc> negative_objectives = new List<objective_desc>();

	//public SortedList<objective_desc> sorted_objectives;
	// Use this for initialization
	void Start () {
		this.name = vars.objective_manager_name;
		objectives_visible = true;

		objectives.Clear();
		negative_objectives.Clear();

		refresh_obj_list();
		}

		//LISTE MIT ALLEN OBJECTIVE MIT DIESEM LEVEL ANLEGEN AUF REIHEN FLOGE SORTIEREN


	public void add_objective(objective_desc desc){
		if(!objectives.Contains(desc)){
			if(desc.is_positive_objective){
				objectives.Add(desc);
			}else{
				negative_objectives.Add(desc);
			}
	}
	}


	public void refresh_obj_list(){

		objective_desc desc;
		foreach (GameObject godesc in GameObject.FindGameObjectsWithTag(vars.objective_tag_name)) {
			desc = godesc.GetComponent<objective_desc>();
			if(!desc.finished && !objectives.Contains(desc)){
				if(desc.is_positive_objective){
					objectives.Add(desc);
				}else{
					negative_objectives.Add(desc);
				}

			}
			
		}


		for (int i = 0; i < objectives.Count; i++) {
			objectives[i].active = false;
			objectives[i].finished = false;
		}
		objectives[0].active = true;





	


	}

	public void toggle_objective_view(){

		objectives_visible = !objectives_visible;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		objectives_btn_text.GetComponent<Text>().text = "OBJECTIVES (" + objectives.Count + ")";

		if(objectives_visible){
			obejctives_text_holder.SetActive(true);
			title_background_holder.SetActive(false);



			for (int j = 0; j < negative_objectives.Count; j++) {




				if(negative_objectives[j].active){
					
					
					//CHECK HERE FOR WARS
					float value_to_check = 0.0f;
					
					switch (negative_objectives[j].variable_to_toggle) {
						
					case vars.objective_toggle_vars.none:
						value_to_check = -1.0f;
						break;
					case vars.objective_toggle_vars.ressource_a:
						value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage;
						break;
					case vars.objective_toggle_vars.ressource_b:
						value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage;
						break;
					case vars.objective_toggle_vars.ressource_c:
						value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage;
						break;
					case vars.objective_toggle_vars.amount_ans_scout:
						value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_scout_ants;
						break;
					case vars.objective_toggle_vars.amount_ants_attack:
						value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants;
						break;
					case vars.objective_toggle_vars.amount_ants_collector:
						value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants;
						break;
					case vars.objective_toggle_vars.base_health:
						value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().health;
					break;




					case vars.objective_toggle_vars.amount_waypoints:
						value_to_check = 0.0f;
						foreach (GameObject item in GameObject.FindGameObjectsWithTag(vars.wp_node_tag)) {
							value_to_check += 1.0f;
						}
						break;
					case vars.objective_toggle_vars.unitgroups:
						value_to_check = GameObject.Find(vars.UnitGroupUIManager).GetComponent<UnitGroupUIManager>().unitGroupList.Count;
						break;
						
					default:
						value_to_check = -1.0f;
						break;
					}
					
					
					
					
					
					
					//operator check

					switch (negative_objectives[j].value_comp_mode) {
					case vars.objective_comparison_mode.equals:
						if(value_to_check == negative_objectives[j].value_to_toggle){
							Application.LoadLevel(vars.level_fail_scene_name);
						}
						break;
						
					case vars.objective_comparison_mode.bigger:
						if(value_to_check > negative_objectives[j].value_to_toggle){
							Application.LoadLevel(vars.level_fail_scene_name);
						}
						break;
						
					case vars.objective_comparison_mode.biggerequals:
						if(value_to_check >= negative_objectives[j].value_to_toggle){
							Application.LoadLevel(vars.level_fail_scene_name);
						}
						break;

					case vars.objective_comparison_mode.smaller:
						if(value_to_check < negative_objectives[j].value_to_toggle){
							Application.LoadLevel(vars.level_fail_scene_name);
						}
						break;
						
						
					case vars.objective_comparison_mode.smallerequals:
						if(value_to_check <= negative_objectives[j].value_to_toggle){
							Application.LoadLevel(vars.level_fail_scene_name);
						}
						break;
						
					case vars.objective_comparison_mode.not:
						if(value_to_check != negative_objectives[j].value_to_toggle){
							Application.LoadLevel(vars.level_fail_scene_name);
						}
						break;
					default:
						break;
					}
				}
			}



		//ALLE FERTGEN OBJ LÖSCHEN und das nächste auf active setzten
		//das aktuelle überwachen
		//die nächsten 3  in die liste eintragen

		for (int i = 0; i < objectives.Count; i++) {


			if(i == 0){
				objective_holder_0.transform.FindChild("objective_text").GetComponent<Text>().text = objectives[i].objective_description;
				objective_holder_0.transform.FindChild("objective_headline").GetComponent<Text>().text = objectives[i].objective_headline;

				if(objectives[i].active && !objectives[i].finished){
					objective_holder_0.transform.FindChild("objective_background").GetComponent<Image>().color = Color.yellow;
				}else if(!objectives[i].active && !objectives[i].finished){
					objective_holder_0.transform.FindChild("objective_background").GetComponent<Image>().color = Color.grey;
				}else if(objectives[i].finished && !objectives[i].active){
					objective_holder_0.transform.FindChild("objective_background").GetComponent<Image>().color = Color.green;
				}

			}else if(i == 1){
				objective_holder_1.transform.FindChild("objective_text").GetComponent<Text>().text = objectives[i].objective_description;
				objective_holder_1.transform.FindChild("objective_headline").GetComponent<Text>().text = objectives[i].objective_headline;

				if(objectives[i].active && !objectives[i].finished){
					objective_holder_1.transform.FindChild("objective_background").GetComponent<Image>().color = Color.yellow;
				}else if(!objectives[i].active && !objectives[i].finished){
					objective_holder_1.transform.FindChild("objective_background").GetComponent<Image>().color = Color.grey;
				}else if(objectives[i].finished && !objectives[i].active){
					objective_holder_1.transform.FindChild("objective_background").GetComponent<Image>().color = Color.green;
				}


			}else if(i == 2){
				objective_holder_2.transform.FindChild("objective_text").GetComponent<Text>().text = objectives[i].objective_description;
				objective_holder_2.transform.FindChild("objective_headline").GetComponent<Text>().text = objectives[i].objective_headline;

				if(objectives[i].active && !objectives[i].finished){
					objective_holder_2.transform.FindChild("objective_background").GetComponent<Image>().color = Color.yellow;
				}else if(!objectives[i].active && !objectives[i].finished){
					objective_holder_2.transform.FindChild("objective_background").GetComponent<Image>().color = Color.grey;
				}else if(objectives[i].finished && !objectives[i].active){
					objective_holder_2.transform.FindChild("objective_background").GetComponent<Image>().color = Color.green;
				}
			}









			if(objectives[i].active){


				//CHECK HERE FOR WARS
				float value_to_check = 0.0f;

				switch (objectives[i].variable_to_toggle) {

				case vars.objective_toggle_vars.none:
					value_to_check = -1.0f;
					break;
				case vars.objective_toggle_vars.ressource_a:
					value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage;
					break;
				case vars.objective_toggle_vars.ressource_b:
					value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage;
				break;
				case vars.objective_toggle_vars.ressource_c:
					value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage;
					break;
				case vars.objective_toggle_vars.amount_ans_scout:
					value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_scout_ants;
					break;
				case vars.objective_toggle_vars.amount_ants_attack:
					value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants;
					break;
				case vars.objective_toggle_vars.amount_ants_collector:
					value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants;
					break;
					case vars.objective_toggle_vars.base_health:
						value_to_check = GameObject.Find(vars.base_name).GetComponent<base_manager>().health;
						break;
				case vars.objective_toggle_vars.amount_waypoints:
					value_to_check = 0.0f;
					foreach (GameObject item in GameObject.FindGameObjectsWithTag(vars.wp_node_tag)) {
						value_to_check += 1.0f;
					}
					break;
					case vars.objective_toggle_vars.unitgroups:
						value_to_check = GameObject.Find(vars.UnitGroupUIManager).GetComponent<UnitGroupUIManager>().unitGroupList.Count;
						break;

				default:
					value_to_check = -1.0f;
				break;
				}






//operator check

				switch (objectives[i].value_comp_mode) {
				case vars.objective_comparison_mode.equals:
					if(value_to_check == objectives[i].value_to_toggle){
						objectives[i].finished = true;
						objectives[i].active = false;
					}
					break;

				case vars.objective_comparison_mode.bigger:
					if(value_to_check > objectives[i].value_to_toggle){
						objectives[i].finished = true;
						objectives[i].active = false;
					}
					break;

					case vars.objective_comparison_mode.biggerequals:
						if(value_to_check >= objectives[i].value_to_toggle){
							objectives[i].finished = true;
							objectives[i].active = false;
						}
						break;

				case vars.objective_comparison_mode.smaller:
					if(value_to_check < objectives[i].value_to_toggle){
						objectives[i].finished = true;
						objectives[i].active = false;
					}
					break;

				
					case vars.objective_comparison_mode.smallerequals:
						if(value_to_check <= objectives[i].value_to_toggle){
							objectives[i].finished = true;
							objectives[i].active = false;
						}
						break;

				case vars.objective_comparison_mode.not:
					if(value_to_check != objectives[i].value_to_toggle){
						objectives[i].finished = true;
						objectives[i].active = false;
					}
					break;
				default:
				break;
				}




			}



	
			
			
			if(objectives[i].finished){
				if(i < objectives.Count-1){
					objectives[i+1].active = true;
					objectives[i+1].finished = false;
				}else{
					//GAME FINISHED
					Application.LoadLevel(vars.level_won_scene_name);
					Debug.Log("GAME FINISHED");
				}
			}




		}//END FOR




		}else{
			obejctives_text_holder.SetActive(false);
			title_background_holder.SetActive(true);
		}



	}
}
