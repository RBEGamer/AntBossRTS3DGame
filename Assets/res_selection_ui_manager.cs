﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class res_selection_ui_manager : MonoBehaviour {


	public Sprite res_icon_ui_type_a;
	public Sprite res_icon_ui_type_b;
	//public Sprite res_icon_ui_type_c;
	public Sprite res_icon_ui_none;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	for (int i = 0; i < 18; i++) {
			GameObject.Find("res_selection_btn_" + (i+1).ToString()).GetComponent<Image>().sprite = res_icon_ui_none;
		}
		int counter = 0;
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {
			counter++;	



			if(n.GetComponent<ressource>().res_type == vars.ressource_type.A){
			GameObject.Find("res_selection_btn_" + counter.ToString()).GetComponent<Image>().sprite = res_icon_ui_type_a;
			}else if(n.GetComponent<ressource>().res_type == vars.ressource_type.B){
				GameObject.Find("res_selection_btn_" + counter.ToString()).GetComponent<Image>().sprite = res_icon_ui_type_b;
		//	}else if(n.GetComponent<ressource>().res_type == vars.ressource_type.C){
		//		GameObject.Find("res_selection_btn_" + counter.ToString()).GetComponent<Image>().sprite = res_icon_ui_type_c;
			}


		}



	}




	public void select_res(int count){

	}


}
