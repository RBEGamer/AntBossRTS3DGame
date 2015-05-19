using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ui_manager : MonoBehaviour {


	public int connected_res_to_ui = -1;

	// Use this for initialization
	void Start () {
		this.name = vars.ui_manager_name;
	}
	
	// Update is called once per frame
	void Update () {

	}


	public GameObject slider_holder;
	public GameObject min_text_holder;
	public GameObject max_text_holder;
	public GameObject res_name_holder;


	public void set_ressource_ants_amounts(float value){
	//	slider_holder.GetComponent<Slider>().minValue = 0;
	//	slider_holder.GetComponent<Slider>().maxValue = von allen ressources die max ants - alle in gebrauch
		//get selected
		//value nach int
		//change value


		if(connected_res_to_ui >= 0 && GameObject.Find(vars.res_name + "_" + connected_res_to_ui) != null){
			Debug.Log("SET ANT RES :" + (int)value);
		
			GameObject.Find(vars.res_name + "_" + connected_res_to_ui).GetComponent<ressource>().set_target_ants((int)value);
			refresh_ressource_ui();
		}


	}

	public void refresh_ressource_ui(){
		//int max_ants, int min_ants =0
	}


}
