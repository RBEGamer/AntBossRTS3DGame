using UnityEngine;
using System.Collections;

public class level_selecotr : MonoBehaviour {


	private int selected_level = 1;

	public GameObject mission_one_desc;
	public GameObject mission_two_desc;
	public GameObject mission_three_desc;

	public GameObject map_animation_prefab;



	public void load_level(){

		Destroy(GameObject.Find(map_animation_prefab.name));

		if(selected_level == 1){
			GameObject.Find("UI_BUTTON_MANAGER").GetComponent<menu_button_manager>().load_level_one();
		}else if(selected_level == 2){
			GameObject.Find("UI_BUTTON_MANAGER").GetComponent<menu_button_manager>().load_level_two();
		}else if(selected_level == 3){
			GameObject.Find("UI_BUTTON_MANAGER").GetComponent<menu_button_manager>().load_level_three();
		}else if(selected_level == 4){
			GameObject.Find("UI_BUTTON_MANAGER").GetComponent<menu_button_manager>().load_level_four();
		}else if(selected_level == 5){
			GameObject.Find("UI_BUTTON_MANAGER").GetComponent<menu_button_manager>().load_level_five();
		}
	}

	public void select_level(int id){
		selected_level = id;

		if(selected_level == 1){
			mission_one_desc.SetActive(true);
			mission_two_desc.SetActive(false);
			mission_three_desc.SetActive(false);
		}else if(selected_level == 2){
			mission_one_desc.SetActive(false);
			mission_two_desc.SetActive(true);
			mission_three_desc.SetActive(false);
		}else if(selected_level == 3){
			mission_one_desc.SetActive(false);
			mission_two_desc.SetActive(false);
			mission_three_desc.SetActive(true);
		}




	}
	// Use this for initialization
	void Start () {
		select_level(1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
