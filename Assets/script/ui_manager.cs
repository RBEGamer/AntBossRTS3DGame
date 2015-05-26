using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ui_manager : MonoBehaviour {


	public int connected_res_to_ui = -1;
	public int uirc = 0;
	public int uirct = 50;
	// Use this for initialization
	void Start () {
		this.name = vars.ui_manager_name;
	//	refresh_ressource_ui();
	//	pem_btn_text.GetComponent<Text>().text = "ENTER PATHEDITMODE";
		vars.is_in_patheditmode = false;
		uirc = 0;

		curr_sel_type = selected_ant_type.nothing;
			clear_base_ui_values();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		manage_view();
		refresh_ressource_ui();
	//	refresh_base_ui();
	//	if(uirc > uirct){
	//		uirc = 0;
			//refresh_ressource_ui();
	//	}else{
	//		uirc++;
	//	}
	
	}




	public enum selected_ui_in_slot_0
	{
		empty_ui, base_ui, ressource_ui
	}

	public selected_ui_in_slot_0 ui_view_slot_0;


	public GameObject empty_ui_holder;
	public GameObject base_ui_holder;
	public GameObject ressource_ui_holder;


	public void slot_0_set_empty(){
		ui_view_slot_0 = selected_ui_in_slot_0.empty_ui;
		manage_view();
	}

	public void slot_0_set_base(){
		ui_view_slot_0 = selected_ui_in_slot_0.base_ui;
		manage_view();
	}

	public void slot_0_set_ressource(){
		if(!vars.is_in_patheditmode){
		ui_view_slot_0 = selected_ui_in_slot_0.ressource_ui;
		}
		manage_view();
	}


	private void manage_view(){

		switch (ui_view_slot_0) {

		case selected_ui_in_slot_0.empty_ui:
			empty_ui_holder.SetActive(true);
			base_ui_holder.SetActive(false);
			ressource_ui_holder.SetActive(false);
			break;
		case selected_ui_in_slot_0.base_ui:
			empty_ui_holder.SetActive(false);
			base_ui_holder.SetActive(true);
			ressource_ui_holder.SetActive(false);
			break;
		case selected_ui_in_slot_0.ressource_ui:
			empty_ui_holder.SetActive(false);
			base_ui_holder.SetActive(false);
			ressource_ui_holder.SetActive(true);
			break;
		default:
			empty_ui_holder.SetActive(true);
			base_ui_holder.SetActive(false);
			ressource_ui_holder.SetActive(false);
			break;
		}


	}

	//------------RESSOURCE -----------------------------------------------//
	//------------RESSOURCE -----------------------------------------------//
	//------------RESSOURCE -----------------------------------------------//
	//------------RESSOURCE -----------------------------------------------//
	


	public GameObject healthbar_progress_picture_holder;
	public GameObject ressource_headline_text;
	public float inverted_health;

	public GameObject ui_res_a_icon;
	public GameObject ui_res_b_icon;
//	public GameObject ui_res_c_icon;
	//public GameObject ui_res_default_icon;
	public GameObject ui_res_amount_text;
	public GameObject ui_active_collector_ants;
	public GameObject ui_assigned_ui_holder;


	public GameObject ui_res_assign_slider;
	public GameObject ui_res_curr_value_text;

	public void refresh_ressource_ui(){

		if(connected_res_to_ui >= 0 && GameObject.Find(vars.res_name + "_" + connected_res_to_ui) != null && ui_view_slot_0 == selected_ui_in_slot_0.ressource_ui){
			//HEALTHBAR
			float inverted_health = GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.health_percentage / 100.0f;
			if(inverted_health > 1.0f){inverted_health = 1.0f;}
			if(inverted_health < 0.0f){inverted_health = 0.0f;}
			healthbar_progress_picture_holder.gameObject.GetComponent<Image>().fillAmount = inverted_health;

			//HEADLINE
			ressource_headline_text.GetComponent<Text>().text = GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.ui_displayname_ressource + " [ID:" + connected_res_to_ui.ToString() +"]";


			//RESS TYPE ICON
			switch (GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res_type) {
			case vars.ressource_type.A:
				ui_res_a_icon.SetActive(true);
				ui_res_b_icon.SetActive(false);
			//	ui_res_c_icon.SetActive(false);
				break;
			case vars.ressource_type.B:
				ui_res_a_icon.SetActive(false);
				ui_res_b_icon.SetActive(true);
			//	ui_res_c_icon.SetActive(false);
				break;
			case vars.ressource_type.C:
				ui_res_a_icon.SetActive(false);
				ui_res_b_icon.SetActive(false);
			//	ui_res_c_icon.SetActive(true);
				break;
			case vars.ressource_type.default_type:
				ui_res_a_icon.SetActive(false);
				ui_res_b_icon.SetActive(false);
			//	ui_res_c_icon.SetActive(false);
				break;
			default:
				ui_res_a_icon.SetActive(false);
				ui_res_b_icon.SetActive(false);
			//	ui_res_c_icon.SetActive(false);
				break;
			}

			//RES CURRENT AVARIABLE RESSOURCE AMOUNT
			ui_res_amount_text.GetComponent<Text>().text = GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.current_harvest_amount.ToString() + " / " + GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.max_harvest.ToString();

			//RES CURRENT ACTIVE COLLECTOR ANTS
			ui_active_collector_ants.GetComponent<Text>().text = GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants.ToString() + " / "+ GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.max_collector_ants.ToString();



			//WENN RESSOURCE NICHT CONNECTED DANN DEN SLIDER NICHT ANZEIGEN!!!!!!!!!!!

			if(GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().is_node_connected && !ui_assigned_ui_holder.gameObject.activeSelf){
				ui_assigned_ui_holder.SetActive(true);
			}else{
				ui_assigned_ui_holder.SetActive(false);
			}



		}


	}

	//------------BASE -----------------------------------------------//
	//------------BASE -----------------------------------------------//
	//------------BASE -----------------------------------------------//
	//------------BASE -----------------------------------------------//





	public GameObject ant_scout_selected_image;
	public GameObject ant_collector_selected_image;
	public GameObject ant_attack_selected_image;
	public GameObject curr_ants;
	public GameObject costs_a_text;
	public GameObject costs_b_text;
	public GameObject prod_slider;
//	public GameObject costs_c_text;
	private  float costs_res_a;
	private float costs_res_b;
//	private float costs_res_c;
	public int ants_to_produce;
	
	private float final_costs_res_a;
	private float final_costs_res_b;
//	private float final_costs_res_c;

	public GameObject curr_slider_value_text;

	public enum selected_ant_type
	{
		nothing, scout, collector, attack
	}

	public selected_ant_type curr_sel_type;



	
	public void toggle_patheditmode(){
		vars.is_in_patheditmode = !vars.is_in_patheditmode;
		if(vars.is_in_patheditmode){
			//		pem_btn_text.GetComponent<Text>().text = "LEAVE PATHEDITMODE";
		}else{
			//		pem_btn_text.GetComponent<Text>().text = "ENTER PATHEDITMODE";
		}
	}



	public void select_nothing(){
		curr_sel_type = selected_ant_type.nothing;
		clear_base_ui_values();
	}
	public void select_scout(){
		curr_sel_type = selected_ant_type.scout;
		clear_base_ui_values();
	}
	public void select_collecotr(){
		curr_sel_type = selected_ant_type.collector;
		clear_base_ui_values();
	}
	public void select_attack(){
		curr_sel_type = selected_ant_type.attack;
		clear_base_ui_values();
	}



	public void clear_base_ui_values(){
		//costs text = 0
		init_base_ui_with_new_selection();
		refresh_base_ui();
	}

	public void init_base_ui_with_new_selection(){
		switch (curr_sel_type) {
		case selected_ant_type.nothing:
			ant_scout_selected_image.SetActive(false);
			ant_collector_selected_image.SetActive(false);
			ant_attack_selected_image.SetActive(false);

			costs_res_a = vars.costs_nothing_ants.costs_res_a;
			costs_res_b = vars.costs_nothing_ants.costs_res_b;
		//	costs_res_c = vars.costs_nothing_ants.costs_res_c;
			break;
		case selected_ant_type.scout:
			ant_scout_selected_image.SetActive(true);
			ant_collector_selected_image.SetActive(false);
			ant_attack_selected_image.SetActive(false);
			costs_res_a = vars.costs_scout_ants.costs_res_a;
			costs_res_b = vars.costs_scout_ants.costs_res_b;
		//	costs_res_c = vars.costs_scout_ants.costs_res_c;
			break;
		case selected_ant_type.collector:
			ant_scout_selected_image.SetActive(false);
			ant_collector_selected_image.SetActive(true);
			ant_attack_selected_image.SetActive(false);
			costs_res_a = vars.costs_collector_ants.costs_res_a;
			costs_res_b = vars.costs_collector_ants.costs_res_b;
		//	costs_res_c = vars.costs_collector_ants.costs_res_c;
			break;
		case selected_ant_type.attack:
			ant_scout_selected_image.SetActive(false);
			ant_collector_selected_image.SetActive(false);
			ant_attack_selected_image.SetActive(true);
			costs_res_a = vars.costs_attack_ants.costs_res_a;
			costs_res_b = vars.costs_attack_ants.costs_res_b;
		//	costs_res_c = vars.costs_attack_ants.costs_res_c;
			break;
		default:
			break;
		}

		base_ui_calc_costs(prod_slider.GetComponent<Slider>().value);
		refresh_base_ui();
	}





	public void base_ui_calc_costs(float value){
		int v = (int)value;
		ants_to_produce = v;

	
		final_costs_res_a = Mathf.Abs( costs_res_a*value);
		final_costs_res_b = Mathf.Abs( costs_res_b*value);
		//final_costs_res_c = Mathf.Abs( costs_res_c*value);





		curr_slider_value_text.GetComponent<Text>().text = v.ToString();

		refresh_base_ui();
	}

	public void apply_buy(){

		if(ants_to_produce > 0){
		if(GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage >= final_costs_res_a){
			if(GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage >= final_costs_res_b){
				//if(GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage >= final_costs_res_c){
					switch (curr_sel_type) {
					case selected_ant_type.nothing:
					break;
					case selected_ant_type.scout:
						GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_scout_ants += ants_to_produce;
						GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage -= final_costs_res_a;
						GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage -= final_costs_res_b;
					//	GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage -= final_costs_res_c;
						break;
					case selected_ant_type.collector:
						GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants += ants_to_produce;
						GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage -= final_costs_res_a;
						GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage -= final_costs_res_b;
					//	GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage -= final_costs_res_c;
						break;
					case selected_ant_type.attack:
						GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants += (int)ants_to_produce;
						GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage -= final_costs_res_a;
						GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage -= final_costs_res_b;
					//	GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage -= final_costs_res_c;
						break;
					default:
					break;
					}



				//}// ende c
			}//ende b
		}//ende a

		}else if(ants_to_produce < 0){
			switch (curr_sel_type) {
			case selected_ant_type.scout:
				if(GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_scout_ants >= Mathf.Abs(ants_to_produce)){

					GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_scout_ants += ants_to_produce;
					GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage += final_costs_res_a;
					GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage += final_costs_res_b;
				//	GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage += final_costs_res_c;
				}
				break;

			case selected_ant_type.collector:
				if(GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants >= Mathf.Abs(ants_to_produce)){
					GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants += ants_to_produce;
					GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage += final_costs_res_a;
					GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage += final_costs_res_b;
				//	GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage += final_costs_res_c;
				}
				break;


			case selected_ant_type.attack:
				if(GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants >= Mathf.Abs(ants_to_produce)){
					GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants += ants_to_produce;
					GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage += final_costs_res_a;
					GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage += final_costs_res_b;
				//	GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage += final_costs_res_c;
				}
				break;

			default:
			break;
			}










		}

		refresh_base_ui();

	}

	public void refresh_base_ui(){

//schauen was selectiert ist und dann die btn highlighten
	
	//curr ants setzten

		//neue sliderwerte setzten

		switch (curr_sel_type) {
		case selected_ant_type.nothing:
		curr_ants.GetComponent<Text>().text = "nothing";
		break;
		case selected_ant_type.scout:
		curr_ants.GetComponent<Text>().text = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_scout_ants.ToString();
		break;
		case selected_ant_type.collector:
		curr_ants.GetComponent<Text>().text = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants.ToString();
		break;
		case selected_ant_type.attack:
		curr_ants.GetComponent<Text>().text = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants.ToString();
		break;
		default:
			break;
		} 	

		if(GameObject.Find (vars.base_name) == null){
			costs_a_text.GetComponent<Text>().text = final_costs_res_a.ToString();
			costs_b_text.GetComponent<Text>().text = final_costs_res_b.ToString();
			//costs_c_text.GetComponent<Text>().text = final_costs_res_c.ToString();
		}else{
		
			if(ants_to_produce >= 0){

				if(ants_to_produce > 0){
				costs_a_text.GetComponent<Text>().text = "-" + final_costs_res_a.ToString() + " / " + GameObject.Find (vars.base_name).GetComponent<base_manager>().res_a_storage.ToString();
				costs_b_text.GetComponent<Text>().text = "-" + final_costs_res_b.ToString()+ " / " + GameObject.Find (vars.base_name).GetComponent<base_manager>().res_b_storage.ToString();
				//costs_c_text.GetComponent<Text>().text = "-" + costs_res_c.ToString()+ " / " + GameObject.Find (vars.base_name).GetComponent<base_manager>().res_c_storage.ToString();
				}else{
					costs_a_text.GetComponent<Text>().text = "" + final_costs_res_a.ToString() + " / " + GameObject.Find (vars.base_name).GetComponent<base_manager>().res_a_storage.ToString();
					costs_b_text.GetComponent<Text>().text = "" + final_costs_res_b.ToString()+ " / " + GameObject.Find (vars.base_name).GetComponent<base_manager>().res_b_storage.ToString();
					//costs_c_text.GetComponent<Text>().text = "" + costs_res_c.ToString()+ " / " + GameObject.Find (vars.base_name).GetComponent<base_manager>().res_c_storage.ToString();

				}

				if(final_costs_res_a > GameObject.Find (vars.base_name).GetComponent<base_manager>().res_a_storage){costs_a_text.GetComponent<Text>().color = Color.red;
				}else{costs_a_text.GetComponent<Text>().color = Color.black;}
				if(final_costs_res_b > GameObject.Find (vars.base_name).GetComponent<base_manager>().res_b_storage){costs_b_text.GetComponent<Text>().color = Color.red;
				}else{costs_b_text.GetComponent<Text>().color = Color.black;}
				//	if(final_costs_res_c > GameObject.Find (vars.base_name).GetComponent<base_manager>().res_c_storage){costs_c_text.GetComponent<Text>().color = Color.red;
				//	}else{costs_c_text.GetComponent<Text>().color = Color.black;}
			}else{

				costs_a_text.GetComponent<Text>().text = "+" + final_costs_res_a.ToString() + " / " + GameObject.Find (vars.base_name).GetComponent<base_manager>().res_a_storage.ToString();
				costs_b_text.GetComponent<Text>().text = "+" + final_costs_res_b.ToString()+ " / " + GameObject.Find (vars.base_name).GetComponent<base_manager>().res_b_storage.ToString();
				//costs_c_text.GetComponent<Text>().text = "+" + costs_res_c.ToString()+ " / " + GameObject.Find (vars.base_name).GetComponent<base_manager>().res_c_storage.ToString();


				if(final_costs_res_a > GameObject.Find (vars.base_name).GetComponent<base_manager>().res_a_storage){costs_a_text.GetComponent<Text>().color = Color.black;
				}else{costs_a_text.GetComponent<Text>().color = Color.green;}
				if(final_costs_res_b > GameObject.Find (vars.base_name).GetComponent<base_manager>().res_b_storage){costs_a_text.GetComponent<Text>().color = Color.black;
				}else{costs_b_text.GetComponent<Text>().color = Color.green;}
			//	if(final_costs_res_c > GameObject.Find (vars.base_name).GetComponent<base_manager>().res_c_storage){costs_a_text.GetComponent<Text>().color = Color.black;
			//	}else{costs_c_text.GetComponent<Text>().color = Color.green;}
			}










		}


			




	}





}
