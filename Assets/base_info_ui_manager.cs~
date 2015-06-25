using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class base_info_ui_manager : MonoBehaviour {

	public GameObject scout_ant_ui_text;
	public GameObject collector_ant_ui_text;
	public GameObject kaempfer_ant_ui_text;





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	

		if(GameObject.Find(vars.base_name) != null){

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
