using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class ui_manager : MonoBehaviour {


	//------------MENU -----------------------------------------------//
	//------------MENU -----------------------------------------------//
	//------------MENU -----------------------------------------------//
	//------------MENU -----------------------------------------------//

	public bool is_in_menu = false;
	public GameObject pause_menu_holder;

	public void toggle_menu(){
		is_in_menu = !is_in_menu;
		pause_menu_holder.SetActive(is_in_menu);

		if(is_in_menu){
			Time.timeScale = 0.0f;
		}else{
			Time.timeScale = 1.0f;
		}

	}

	public void goto_main_menu(){
		Application.LoadLevel(vars.main_menu_scene_name);
	}

	public void goto_level_selection(){
		Application.LoadLevel(vars.mission_selection_scene_name);
	}


	public void manage_menu(){

		if(Input.GetKeyDown(vars.key_pause_menu)){
			toggle_menu();
		}

	}



	//------------VARS -----------------------------------------------//
	//------------VARS -----------------------------------------------//
	//------------VARS -----------------------------------------------//
	//------------VARS -----------------------------------------------//
	


	public int connected_unit_to_ui = -1;
	public int connected_res_to_ui = -1;
	public int uirc = 0;
	public int uirct = 50;
	// Use this for initialization
	void Start () {
		this.name = vars.ui_manager_name;
		Time.timeScale = 1.0f;
		//	refresh_ressource_ui();
		//	pem_btn_text.GetComponent<Text>().text = "ENTER PATHEDITMODE";
		vars.is_in_patheditmode = false;
		uirc = 0;
		
		curr_sel_type = selected_ant_type.nothing;
		clear_base_ui_values();
		pause_menu_holder.SetActive(false);
		is_in_menu = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		manage_menu();
		manage_view();
		refresh_ressource_ui();
		//	refresh_base_ui();
		//	if(uirc > uirct){
		//		uirc = 0;
		//refresh_ressource_ui();
		//	}else{
		//		uirc++;
		//	}
		ant_produce_query_task();
		manage_unit_ui();
		manage_upgrade_ui();
	}
	
	
	
	
	public enum selected_ui_in_slot_0
	{
		empty_ui, base_ui, ressource_ui, unit_ui
	}
	
	public selected_ui_in_slot_0 ui_view_slot_0;
	
	
	public GameObject empty_ui_holder;
	public GameObject base_ui_holder;
	public GameObject ressource_ui_holder;
	public GameObject unit_ui_holder;

	
	
	public void slot_0_set_empty(){
		ui_view_slot_0 = selected_ui_in_slot_0.empty_ui;
		show_upgrade_ui = false;
		vars.is_in_patheditmode = false;
		show_upgrade_ui = false;
		manage_view();
	
	}
	
	public void slot_0_set_base(){
		ui_view_slot_0 = selected_ui_in_slot_0.base_ui;
		show_upgrade_ui = false;
		manage_view();
	}
	
	public void slot_0_set_ressource(){
		show_upgrade_ui = false;
		if(!vars.is_in_patheditmode){
			ui_view_slot_0 = selected_ui_in_slot_0.ressource_ui;

		}
		manage_view();
	}

	public void slot_0_set_unit(){
		show_upgrade_ui = false;
		ui_view_slot_0 = selected_ui_in_slot_0.unit_ui;
		vars.is_in_patheditmode = false;
	}
	
	private void manage_view(){
		
		switch (ui_view_slot_0) {
			
		case selected_ui_in_slot_0.empty_ui:
			empty_ui_holder.SetActive(true);
			base_ui_holder.SetActive(false);
			ressource_ui_holder.SetActive(false);
			unit_ui_holder.SetActive(false);
			break;
		case selected_ui_in_slot_0.base_ui:
			empty_ui_holder.SetActive(false);
			base_ui_holder.SetActive(true);
			ressource_ui_holder.SetActive(false);
			unit_ui_holder.SetActive(false);
			break;
		case selected_ui_in_slot_0.ressource_ui:
			empty_ui_holder.SetActive(false);
			base_ui_holder.SetActive(false);
			ressource_ui_holder.SetActive(true);
			unit_ui_holder.SetActive(false);
			break;
		case selected_ui_in_slot_0.unit_ui:
			empty_ui_holder.SetActive(false);
			base_ui_holder.SetActive(false);
			ressource_ui_holder.SetActive(false);
			unit_ui_holder.SetActive(true);
			break;
		default:
			empty_ui_holder.SetActive(true);
			base_ui_holder.SetActive(false);
			ressource_ui_holder.SetActive(false);
			unit_ui_holder.SetActive(false);
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
	
	

	public GameObject upgrade_res_button_0;
	public GameObject upgrade_res_button_1;
	public GameObject upgrade_res_button_2;


	public Sprite ant_icon_loaded;
	public Sprite ant_icon_unloaded;
	public Sprite ant_icon_none;

	public void refresh_ressource_ui(){








		if(connected_res_to_ui >= 0 && GameObject.Find(vars.res_name + "_" + connected_res_to_ui) != null && ui_view_slot_0 == selected_ui_in_slot_0.ressource_ui){
		


			GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().is_selected_by_res_manager = true;

			//clear all button images
			for (int i = 0; i < 10; i++) {
				GameObject.Find("ant_destroy_btn_" + (i+1).ToString()).GetComponent<Image>().sprite = ant_icon_none;
			}
			
			//show ant ichons on buttons
			int counter =0 ;
			foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.collector_ant_tag)) {
				if(n.GetComponent<collector_ant>().connected_ressource == connected_res_to_ui){
				counter++;
				if(n.GetComponent<collector_ant>().ant_bite_size > 0){
					GameObject.Find("ant_destroy_btn_" + counter.ToString()).GetComponent<Image>().sprite = ant_icon_loaded;
				}else{
					GameObject.Find("ant_destroy_btn_" + counter.ToString()).GetComponent<Image>().sprite = ant_icon_unloaded;
				}
			}
			}




			//HEALTHBAR
			float inverted_health = GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.health_percentage / 100.0f;
			if(inverted_health > 1.0f){inverted_health = 1.0f;}
			if(inverted_health < 0.0f){inverted_health = 0.0f;}
			healthbar_progress_picture_holder.gameObject.GetComponent<Image>().fillAmount = inverted_health;
			
			//HEADLINE
			//ressource_headline_text.GetComponent<Text>().text = GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.ui_displayname_ressource + " [ID:" + connected_res_to_ui.ToString() +"]";
			
			
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
			}

			if(!GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().is_node_connected && ui_assigned_ui_holder.gameObject.activeSelf){
				ui_assigned_ui_holder.SetActive(false);
			}
			
			
		}
		
		
	}







	public int res_ants_to_assign =0 ;

	public void apply_ant_assign(){
		if(connected_res_to_ui >= 0 && GameObject.Find(vars.res_name + "_" + connected_res_to_ui) != null && ui_view_slot_0 == selected_ui_in_slot_0.ressource_ui){

			if(res_ants_to_assign > 0){
		GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants += res_ants_to_assign;
		GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants -= res_ants_to_assign;
		//WTF THIS CODE FOR THE BASE PART WTF
		//GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage += vars.costs_collector_ants.costs_res_a * res_ants_to_assign;
		//GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage += vars.costs_collector_ants.costs_res_b * res_ants_to_assign;
			}else if (res_ants_to_assign < 0){

				GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants -= Mathf.Abs( res_ants_to_assign);
				GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants += Mathf.Abs( res_ants_to_assign);

				GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage += vars.costs_collector_ants.costs_res_a * Mathf.Abs(res_ants_to_assign);
				GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage += vars.costs_collector_ants.costs_res_b * Mathf.Abs(res_ants_to_assign);

			}
	}
}

	public void res_calc_assign(float value){
		res_ants_to_assign = 0;
		if(connected_res_to_ui >= 0 && GameObject.Find(vars.res_name + "_" + connected_res_to_ui) != null && ui_view_slot_0 == selected_ui_in_slot_0.ressource_ui){
		
		int v = (int)value;
		

		int tca = GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants;//TARGET ANTS
		int mca = GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.max_collector_ants;//TARGET ANTS

		int caca = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants; //current avariable collection ants

		if(value > 0){ //adden
			//schauen ob die maximale anzahl für die ameisen der ressource errreciht ist
			if(tca + v > mca){
			value = mca - tca;
			}
				//schauen ob die maximale anzahl die anzahl der gekaufen ameisen übersteigt
			if(value > caca){
				value = caca;
			}
				//wert zuweisen damit er abgefragt werden kann wenn der btn gedrückt wird
			res_ants_to_assign = (int)value;

			}else if(value < 0){ //sub

				if(tca - Mathf.Abs(v) < 0){
					value = tca - Mathf.Abs(v) + value;
				}


				res_ants_to_assign = (int)value;
			}

		//-> +- current_target
		//-> wenn grösser als max dann auf das maximum stellen sofern verfügbar
		//-> kleiner als 0 (min) dann auf 0 stellen

		}
	}



	public void clear_full_ants(){
	foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.collector_ant_tag)) {

			if(n.GetComponent<collector_ant>().ant_bite_size > 0 && n.GetComponent<collector_ant>().connected_ressource == connected_res_to_ui && GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants >= 1){


				GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants += 1;
				GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants -= 1;



				n.GetComponent<collector_ant>().ant_state = collector_ant.ant_activity_state.destroy;
			}
		}
	}



	public void clear_selected_ant(int ant_count){

		if( GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants >= 1){
		GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants += 1;
		GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants -= 1;
		}

	}

	public void clear_all_ants(){


		GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants +=GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants;
		GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants = 0;

		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.collector_ant_tag)) {
			if( n.GetComponent<collector_ant>().connected_ressource == connected_res_to_ui){
				n.GetComponent<collector_ant>().ant_state = collector_ant.ant_activity_state.destroy;
			}
		}
	}
	
	//------------BASE -----------------------------------------------//
	//------------BASE -----------------------------------------------//
	//------------BASE -----------------------------------------------//
	//------------BASE -----------------------------------------------//


	public Sprite scout_ant_icon;
	public Sprite collector_ant_icon;
	public Sprite attack_ant_icon;
	public Sprite none_ant_icon;

	enum ant_types
	{
		scout, collector, attack
	}


	struct ant_query_info
	{
		public ant_types type;
		public float waititme;
		public float max_waittime;
	}


	 List<ant_query_info> ant_produce_query = new List<ant_query_info>();


	public void quit_produce_task(int queray_pos){

		//schauen welcher typ -> ressourcen wieder drauf -> aus der liste entferenn

		if(ant_produce_query[queray_pos].type == ant_types.scout){
			GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage += vars.costs_scout_ants.costs_res_a;
			GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage += vars.costs_scout_ants.costs_res_b;
			//GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage += vars.costs_scout_ants.costs_res_c;
		}else if(ant_produce_query[queray_pos].type == ant_types.collector){
			GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage += vars.costs_collector_ants.costs_res_a;
			GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage += vars.costs_collector_ants.costs_res_b;
			//GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage += vars.costs_collector_ants.costs_res_c;
		}else if(ant_produce_query[queray_pos].type == ant_types.attack){
			GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage += vars.costs_attack_ants.costs_res_a;
			GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage += vars.costs_attack_ants.costs_res_b;
			//GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage += vars.costs_attack_ants.costs_res_c;
		}

		ant_produce_query.RemoveAt(queray_pos);
	}

	void ant_produce_query_task(){

		if(ui_view_slot_0 == selected_ui_in_slot_0.base_ui){
		//alle btns weiss
			for (int i = 0; i < 12; i++) {

				if(i > ant_produce_query.Count){
			GameObject.Find("ant_prod_query_status_slot_" + i.ToString()).GetComponent<Image>().sprite = none_ant_icon;
				}
			GameObject.Find("ant_prod_query_status_slot_" + i.ToString()).gameObject.transform.FindChild("ant_prod_query_status_slot_progressbar").GetComponent<Image>().fillAmount = 0.0f;

		}
		}

	




		for (int i = 0; i < ant_produce_query.Count; i++)
		{
		
			//Update time
			ant_query_info ant_time_tmp = ant_produce_query[i];
			ant_time_tmp.waititme -= Time.deltaTime;
			ant_produce_query[i] = ant_time_tmp;
		

			//SHOW Produce state
			if(ui_view_slot_0 == selected_ui_in_slot_0.base_ui){
				GameObject.Find("ant_prod_query_status_slot_" + i.ToString()).gameObject.transform.FindChild("ant_prod_query_status_slot_progressbar").GetComponent<Image>().fillAmount = ant_produce_query[i].waititme / ant_produce_query[i].max_waittime;
			}


			if(ui_view_slot_0 == selected_ui_in_slot_0.base_ui){
				switch (ant_produce_query[i].type) {
				case ant_types.scout:
				GameObject.Find("ant_prod_query_status_slot_" + i.ToString()).GetComponent<Image>().sprite = scout_ant_icon;
				break;
				case ant_types.collector:
					GameObject.Find("ant_prod_query_status_slot_" + i.ToString()).GetComponent<Image>().sprite = collector_ant_icon;
					break;
				case ant_types.attack:
					GameObject.Find("ant_prod_query_status_slot_" + i.ToString()).GetComponent<Image>().sprite = attack_ant_icon;
					break;
				default:
				break;
				}
					}


			if(ant_produce_query[i].waititme <= 0.0f){
				switch (ant_produce_query[i].type) {
				case ant_types.scout:
				GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_scout_ants += 1;
				break;
				case ant_types.collector:
				GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants += 1;
				break;
				case ant_types.attack:
				GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants += 1;
				break;
				default:
				//	GameObject.Find("ant_prod_query_status_slot_" + i.ToString()).GetComponent<Image>().sprite = none_ant_icon;
				break;
				}
				ant_produce_query.RemoveAt(i);
			}

		}


	

	}







	public GameObject costs_a_text;
	public GameObject costs_b_text;

	//	public GameObject costs_c_text;
	private  float costs_res_a;
	private float costs_res_b;
	//	private float costs_res_c;
	public int ants_to_produce;
	
	private float final_costs_res_a;
	private float final_costs_res_b;
	//	private float final_costs_res_c;
	


	public GameObject upgrade_base_button_0;
	public GameObject upgrade_base_button_1;
	public GameObject upgrade_base_button_2;


//	public GameObject wp_button;
	public enum selected_ant_type
	{
		nothing, scout, collector, attack
	}
	
	public selected_ant_type curr_sel_type;
	
	
	
	
	public void toggle_patheditmode(){
		vars.is_in_patheditmode = !vars.is_in_patheditmode;

		//Color c = wp_button.GetComponent<Button>().colors.normalColor;


		if(vars.is_in_patheditmode){
				//	pem_btn_text.GetComponent<Text>().text = "LEAVE PATHEDITMODE";
			//wp_button.GetComponent<Button>().colors.highlightedColor.r = 1.0f;
		}else{
		GameObject.Find(vars.path_manager_name).GetComponent<pathmanager>().deselect_all_nodes();
				//	pem_btn_text.GetComponent<Text>().text = "ENTER PATHEDITMODE";
//			wp_button.GetComponent<Button>().colors.normalColor = Color.blue;
		}
		//wp_button.GetComponent<Button>().colors = c
	}
	
	
	
	public void select_nothing(){
		curr_sel_type = selected_ant_type.nothing;
		clear_base_ui_values();
	}
	public void select_scout(){
		curr_sel_type = selected_ant_type.scout;
		clear_base_ui_values();
	}
	public void select_collector(){
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
	
			costs_res_a = vars.costs_nothing_ants.costs_res_a;
			costs_res_b = vars.costs_nothing_ants.costs_res_b;
			//	costs_res_c = vars.costs_nothing_ants.costs_res_c;
			break;
		case selected_ant_type.scout:

			costs_res_a = vars.costs_scout_ants.costs_res_a;
			costs_res_b = vars.costs_scout_ants.costs_res_b;
			//	costs_res_c = vars.costs_scout_ants.costs_res_c;
			break;
		case selected_ant_type.collector:

			costs_res_a = vars.costs_collector_ants.costs_res_a;
			costs_res_b = vars.costs_collector_ants.costs_res_b;
			//	costs_res_c = vars.costs_collector_ants.costs_res_c;
			break;
		case selected_ant_type.attack:

			costs_res_a = vars.costs_attack_ants.costs_res_a;
			costs_res_b = vars.costs_attack_ants.costs_res_b;
			//	costs_res_c = vars.costs_attack_ants.costs_res_c;
			break;
		default:
			break;
		}
		

		refresh_base_ui();
	}
	
	
	
	
	
	public void base_ui_calc_costs(float value){
		int v = (int)value;
		ants_to_produce = v;
		
		
		final_costs_res_a = Mathf.Abs( costs_res_a*value);
		final_costs_res_b = Mathf.Abs( costs_res_b*value);
		//final_costs_res_c = Mathf.Abs( costs_res_c*value);

		

		
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
						//GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_scout_ants += ants_to_produce;

						if(ant_produce_query.Count < 12){
							ant_query_info tmp_prid_ant;
							tmp_prid_ant.type = ant_types.scout;
							tmp_prid_ant.max_waittime = vars.costs_scout_ants.ant_query_waittime;
							tmp_prid_ant.waititme = vars.costs_scout_ants.ant_query_waittime;
							ant_produce_query.Add (tmp_prid_ant);
							GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage -= final_costs_res_a;
							GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage -= final_costs_res_b;
						}
						//	GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage -= final_costs_res_c;
						break;
					case selected_ant_type.collector:

						if(ant_produce_query.Count < 12){
							ant_query_info tmp_prid_ant;
							tmp_prid_ant.type = ant_types.collector;
							tmp_prid_ant.max_waittime = vars.costs_collector_ants.ant_query_waittime;
							tmp_prid_ant.waititme = vars.costs_scout_ants.ant_query_waittime;
							GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage -= final_costs_res_a;
							GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage -= final_costs_res_b;
						}
						//	GameObject.Find(vars.base_name).GetComponent<base_manager>().res_c_storage -= final_costs_res_c;
						break;
					case selected_ant_type.attack:
						if(ant_produce_query.Count < 12){
							ant_query_info tmp_prid_ant;
							tmp_prid_ant.type = ant_types.scout;
							tmp_prid_ant.max_waittime = vars.costs_attack_ants.ant_query_waittime;
							tmp_prid_ant.waititme = vars.costs_scout_ants.ant_query_waittime;
							GameObject.Find(vars.base_name).GetComponent<base_manager>().res_a_storage -= final_costs_res_a;
							GameObject.Find(vars.base_name).GetComponent<base_manager>().res_b_storage -= final_costs_res_b;
						}
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





	//------------UNIT -----------------------------------------------//
	//------------UNIT -----------------------------------------------//
	//------------UNIT -----------------------------------------------//
	//------------UNIT -----------------------------------------------//


	public SavedUnitGroup sug;
	public bool is_saved_group = false;


	public GameObject unit_group_info_health_text;
	public GameObject unit_group_info_attackrange_text;
	public GameObject unit_group_info_attackspeed_text;


	public Sprite empty_unit_ui_slot;
	public Sprite filled_unit_ui_slot;


	public GameObject unit_action_button_holder;
	public GameObject unit_command_button_holder;

	public void manage_unit_ui(){

		if(is_saved_group){
			unit_action_button_holder.SetActive(true);
			unit_command_button_holder.SetActive(false);
		}else{
			unit_action_button_holder.SetActive(false);
			unit_command_button_holder.SetActive(true);
		}


		if( ui_view_slot_0 == selected_ui_in_slot_0.unit_ui){


			for (int i = 0; i < 10; i++) {
				GameObject.Find("unit_destroy_btn_" + (i+1).ToString()).GetComponent<Image>().sprite = empty_unit_ui_slot;
			}


			if(is_saved_group){
			unit_group_info_health_text.GetComponent<Text>().text = sug.health.ToString();
			unit_group_info_attackrange_text.GetComponent<Text>().text = sug.attackrange.ToString();
			unit_group_info_attackspeed_text.GetComponent<Text>().text = sug.attackspeed.ToString();

			for (int i = 0; i < sug.numUnits; i++) {
				GameObject.Find("unit_destroy_btn_" + (i+1).ToString()).GetComponent<Image>().sprite = filled_unit_ui_slot;
			}
			}else{

				if(GameObject.Find(vars.UnitGroupUIManager).GetComponent<UnitGroupUIManager>().selectedUnitGroupBase.isSelected()){
				unit_group_info_health_text.GetComponent<Text>().text = GameObject.Find(vars.UnitGroupUIManager).GetComponent<UnitGroupUIManager>().selectedUnitGroupBase.health.ToString();
				unit_group_info_attackrange_text.GetComponent<Text>().text =  GameObject.Find(vars.UnitGroupUIManager).GetComponent<UnitGroupUIManager>().selectedUnitGroupBase.attackrange.ToString();
				unit_group_info_attackspeed_text.GetComponent<Text>().text =  GameObject.Find(vars.UnitGroupUIManager).GetComponent<UnitGroupUIManager>().selectedUnitGroupBase.attackspeed.ToString();
				
				for (int i = 0; i < GameObject.Find(vars.UnitGroupUIManager).GetComponent<UnitGroupUIManager>().selectedUnitGroupBase.myUnitList.Count; i++) {
					GameObject.Find("unit_destroy_btn_" + (i+1).ToString()).GetComponent<Image>().sprite = filled_unit_ui_slot;
				}

				}
			}






		}

	}


	public void remove_selected_unit_from_group(int id){
		if(sug.numUnits > 0 && is_saved_group){
			sug.numUnits--;
			GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants += 1;
		}else{
		}
	}

	public void remove_unit_group(){
		if(is_saved_group){
		int aig = GameObject.Find(vars.base_name).GetComponent<UnitGroupCache>().deleteUnitGroup(sug);
		GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants += aig;
		ui_view_slot_0 = selected_ui_in_slot_0.empty_ui;
		}
	}

	public void add_units_to_group(){
		if(sug.numUnits < 18 && GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants > 0 && is_saved_group){
			sug.numUnits++;
			GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants -= 1;
		}
	}

	public void spawn_unit_group(){
		if(is_saved_group && sug.numUnits > 0){
			GameObject.Find(vars.base_name).GetComponent<UnitGroupCache>().spawnUnitgroup(sug);
			GameObject.Find(vars.base_name).GetComponent<UnitGroupCache>().deleteUnitGroup(sug);

			is_saved_group = false;
			ui_view_slot_0 = selected_ui_in_slot_0.empty_ui;
		}

	}


	public void fill_group_with_units(){
		if(is_saved_group){
		int amount_to_add = 18 - sug.numUnits;
		if(amount_to_add > 0){
			int bought_ants = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants;
			if( (bought_ants - amount_to_add) >= 0){
				for (int i = 0; i < amount_to_add; i++) {
					add_units_to_group();
				}
			}else{
				for (int i = 0; i < bought_ants; i++) {
					add_units_to_group();
				}
			}
		}
	}
}



	//------------UPGRADE -----------------------------------------------//
	//------------UPGRADE -----------------------------------------------//
	//------------UPGRADE -----------------------------------------------//
	//------------UPGRADE -----------------------------------------------//

	public bool show_upgrade_ui = false;
	public GameObject upgrad_ui_holder;
	public Sprite empty_upgrad_ui_icon;

	public GameObject upgrade_ui_headline_text;
	public GameObject upgrade_ui_description_text;

	private upgrade_description[] view_specific_upgrades = new upgrade_description[9];
	private upgrade_description selected_upgrade = null;

	public void manage_upgrade_ui(){
		if(show_upgrade_ui){
			upgrad_ui_holder.SetActive(true);
		}else{
			upgrad_ui_holder.SetActive(false);
		}



		if(show_upgrade_ui){

		if(ui_view_slot_0 == selected_ui_in_slot_0.base_ui){
				for (int i = 0; i < 9; i++) {
					GameObject.Find("select_upgrade_slot_btn_" + i.ToString()).GetComponent<Image>().sprite = empty_upgrad_ui_icon;
					view_specific_upgrades[i] = null;
				}
				for (int i = 0; i < GameObject.Find(vars.upgrade_manager_name).GetComponent<upgrade_manager>().upgrade_list.Count; i++) {
					if(!GameObject.Find(vars.upgrade_manager_name).GetComponent<upgrade_manager>().upgrade_list[i].taken && GameObject.Find(vars.upgrade_manager_name).GetComponent<upgrade_manager>().upgrade_list[i].upgrade_type == vars.upgrade_type.ant_base){
						GameObject.Find("select_upgrade_slot_btn_" + i).GetComponent<Image>().sprite = GameObject.Find(vars.upgrade_manager_name).GetComponent<upgrade_manager>().upgrade_list[i].upgrade_icon;
						view_specific_upgrades[i] = GameObject.Find(vars.upgrade_manager_name).GetComponent<upgrade_manager>().upgrade_list[i];
					}
				}
		}




			if(ui_view_slot_0 == selected_ui_in_slot_0.ressource_ui){
				for (int i = 0; i < 9; i++) {
					GameObject.Find("select_upgrade_slot_btn_" + i.ToString()).GetComponent<Image>().sprite = empty_upgrad_ui_icon;
					view_specific_upgrades[i] = null;
				}


				for (int i = 0; i < GameObject.Find(vars.upgrade_manager_name).GetComponent<upgrade_manager>().upgrade_list.Count; i++) {
					if(GameObject.Find(vars.upgrade_manager_name).GetComponent<upgrade_manager>().upgrade_list[i].upgrade_type == vars.upgrade_type.ressources){
						GameObject.Find("select_upgrade_slot_btn_" + i).GetComponent<Image>().sprite = GameObject.Find(vars.upgrade_manager_name).GetComponent<upgrade_manager>().upgrade_list[i].upgrade_icon;
						view_specific_upgrades[i] = GameObject.Find(vars.upgrade_manager_name).GetComponent<upgrade_manager>().upgrade_list[i];
					}
				}



			}







		}//ende if
	}

	//0-3
	public void toggle_upgrade_window(int mapped_upgrade_slot){

		//GameObject.Find(vars.base_name).GetComponent<base_manager>().add_upgrade(null);
		show_upgrade_ui = !show_upgrade_ui;




	}

	public void buy_selected_upgrade(){
		Debug.Log("buy btn");

		//if(ui_view_slot_0 == selected_ui_in_slot_0.base_ui){
			if(selected_upgrade.gameObject != null){
			Debug.Log("selected not empty");


			if(GameObject.Find(vars.base_name).GetComponent<base_manager>().add_upgrade(ref selected_upgrade) && ui_view_slot_0 == selected_ui_in_slot_0.base_ui){
				show_upgrade_ui = false;
				selected_upgrade = null;
				upgrade_ui_headline_text.GetComponent<Text>().text = "NO UPGRADE SELECTED";
				upgrade_ui_description_text.GetComponent<Text>().text = "Please select an Upgrade on the left side";
				}else{
				Debug.Log("add failed");
				}


			
			if(GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().add_upgrade(ref selected_upgrade) && ui_view_slot_0 == selected_ui_in_slot_0.ressource_ui){
				show_upgrade_ui = false;
				selected_upgrade = null;
				upgrade_ui_headline_text.GetComponent<Text>().text = "NO UPGRADE SELECTED";
				upgrade_ui_description_text.GetComponent<Text>().text = "Please select an Upgrade on the left side";
			}else{
				Debug.Log("add failed");
			}




			}else{
				Debug.Log("selected empty");
			}

		//}




	}


	//0-8
	public void select_upgrade(int btn_id){
		//if array an pos i = 0
		if(view_specific_upgrades[btn_id] != null){
			upgrade_ui_headline_text.GetComponent<Text>().text = view_specific_upgrades[btn_id].upgrade_headline;
			upgrade_ui_description_text.GetComponent<Text>().text = view_specific_upgrades[btn_id].upgrade_desc;
			selected_upgrade = view_specific_upgrades[btn_id];
		}
	}






}
