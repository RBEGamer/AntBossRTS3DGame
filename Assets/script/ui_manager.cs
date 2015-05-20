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
		refresh_ressource_ui();
		pem_btn_text.GetComponent<Text>().text = "ENTER PATHEDITMODE";
		vars.is_in_patheditmode = false;
		uirc = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(uirc > uirct){
			uirc = 0;
			refresh_ressource_ui();
		}else{
			uirc++;
		}
	
	}


	public GameObject slider_holder;
	public GameObject min_text_holder;
	public GameObject max_text_holder; // max 5 
	public GameObject res_name_holder;
	public GameObject avariable_ants_holder;
	public GameObject current_text_holder;


	public GameObject pem_btn_text;



	public void toggle_patheditmode(){
		vars.is_in_patheditmode = !vars.is_in_patheditmode;
		if(vars.is_in_patheditmode){
		pem_btn_text.GetComponent<Text>().text = "LEAVE PATHEDITMODE";
		}else{
		pem_btn_text.GetComponent<Text>().text = "ENTER PATHEDITMODE";
		}
	}















	public void set_ressource_ants_amounts(float value){
		if(connected_res_to_ui >= 0 && GameObject.Find(vars.res_name + "_" + connected_res_to_ui) != null){
		
		//hier kontrolle ob noch ants verfügbar sind

		GameObject.Find(vars.base_name).GetComponent<base_manager>().calc_avariable_collecotr_ats();
		 int ava = GameObject.Find(vars.base_name).GetComponent<base_manager>().avariable_collector_ants;
		 int cv = GameObject.Find(vars.ressource_manager_name).GetComponent<ressource_manager>().count_target_ant_amount(); //falsch!!!! eine funktion die alle arget zusammenrechnet
		int nv = cv - GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants + (int)value;
		//aktueller 
		//Debug.Log(nv);
		if(nv < ava){

		if(connected_res_to_ui >= 0 && GameObject.Find(vars.res_name + "_" + connected_res_to_ui) != null){
			Debug.Log("SET ANT RES :" + (int)value);
			GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().set_target_ants((int)value);
			refresh_ressource_ui();
		}

		}
		}
	}

	public void refresh_ressource_ui_slider(){
		if(connected_res_to_ui >= 0 && GameObject.Find(vars.res_name + "_" + connected_res_to_ui) != null){
			slider_holder.GetComponent<Slider>().value = GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants;
		}
	}


	public void refresh_ressource_ui(){

		GameObject.Find(vars.base_name).GetComponent<base_manager>().calc_avariable_collecotr_ats();
		avariable_ants_holder.GetComponent<Text>().text = " ACA :" + GameObject.Find(vars.base_name).GetComponent<base_manager>().avariable_collector_ants.ToString();

		//int max_ants, int min_ants =0
		if(connected_res_to_ui >= 0 && GameObject.Find(vars.res_name + "_" + connected_res_to_ui) != null){
			current_text_holder.GetComponent<Text>().text = GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.target_collection_ants.ToString();
			max_text_holder.GetComponent<Text>().text = GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.max_collector_ants.ToString();
			min_text_holder.GetComponent<Text>().text = "0";
			slider_holder.GetComponent<Slider>().minValue = 0;
			slider_holder.GetComponent<Slider>().maxValue = GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res.max_collector_ants;



			//SET NAME
			string name_pre = "";
			string name_post = " (" + connected_res_to_ui + ")";
			switch (GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().res_type) {
			case vars.ressource_type.A:
				res_name_holder.GetComponent<Text>().text =  name_pre+ vars.ui_displayname_ressource_type_a +name_post;
				break;
			case vars.ressource_type.B:
				res_name_holder.GetComponent<Text>().text =  name_pre+ vars.ui_displayname_ressource_type_b+name_post;
				break;
			case vars.ressource_type.C:
				res_name_holder.GetComponent<Text>().text =  name_pre+ vars.ui_displayname_ressource_type_c+name_post;
				break;
			case vars.ressource_type.default_type:
				res_name_holder.GetComponent<Text>().text =  name_pre+ vars.ui_displayname_ressource_type_default+name_post;
				break;
			default:
				res_name_holder.GetComponent<Text>().text =  name_pre+ vars.ui_displayname_no_ressource_selected_text+name_post;
			break;
			}
		}else{
			res_name_holder.GetComponent<Text>().text =   vars.ui_displayname_no_ressource_selected_text;
		}
	

	}


}
