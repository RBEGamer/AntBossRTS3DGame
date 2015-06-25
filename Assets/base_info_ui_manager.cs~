using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class base_info_ui_manager : MonoBehaviour {

	public GameObject scout_ant_ui_text;
	public GameObject collector_ant_ui_text;
	public GameObject kaempfer_ant_ui_text;
	public GameObject base_healthbar_progress_picture_holder;




	// Use this for initialization
	void Start () {
	
	}


	public void select_base(){
		GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().slot_0_set_base();
		GameObject.Find(vars.main_camera_script_holder_name).GetComponent<camera_movement>().camera_offset = new Vector3(GameObject.Find(vars.base_name).GetComponent<base_manager>().transform.position.x,GameObject.Find(vars.main_camera_script_holder_name).GetComponent<camera_movement>().camera_offset.y,GameObject.Find(vars.base_name).GetComponent<base_manager>().transform.position.z);
	}

	// Update is called once per frame
	void FixedUpdate () {
	

		if(GameObject.Find(vars.base_name) != null){



			float inverted_health = GameObject.Find(vars.base_name).GetComponent<base_manager>().base_health_percentage / 100.0f;
			if(inverted_health > 1.0f){inverted_health = 1.0f;}
			if(inverted_health < 0.0f){inverted_health = 0.0f;}
			base_healthbar_progress_picture_holder.gameObject.GetComponent<Image>().fillAmount = inverted_health;




			scout_ant_ui_text.GetComponent<Text>().text = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_scout_ants.ToString();
			collector_ant_ui_text.GetComponent<Text>().text = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_collector_ants.ToString();
			kaempfer_ant_ui_text.GetComponent<Text>().text = GameObject.Find(vars.base_name).GetComponent<base_manager>().bought_attack_ants.ToString();

		}else{
			scout_ant_ui_text.GetComponent<Text>().text = "0";
			collector_ant_ui_text.GetComponent<Text>().text = "0";
			kaempfer_ant_ui_text.GetComponent<Text>().text = "0";
		}




	}
}
