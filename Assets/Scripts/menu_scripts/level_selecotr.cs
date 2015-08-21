using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class level_selecotr : MonoBehaviour {


	private int selected_level = 1;

	public GameObject mission_one_desc;
	public GameObject mission_two_desc;
	public GameObject mission_three_desc;

	public GameObject map_animation_prefab;


	public Sprite btn1_normal, btn1_pushed, btn1_over;
	public Sprite btn2_normal, btn2_pushed, btn2_over;
	public Sprite btn3_normal, btn3_pushed, btn3_over;


	public GameObject btn1,btn2,btn3;
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
			btn1.GetComponent<Image>().sprite = btn1_pushed;
			btn2.GetComponent<Image>().sprite = btn2_normal;
			btn3.GetComponent<Image>().sprite = btn3_normal;
		}else if(selected_level == 2){
			mission_one_desc.SetActive(false);
			mission_two_desc.SetActive(true);
			mission_three_desc.SetActive(false);
			btn2.GetComponent<Image>().sprite = btn2_pushed;
			btn1.GetComponent<Image>().sprite = btn1_normal;
			btn3.GetComponent<Image>().sprite = btn3_normal;
		}else if(selected_level == 3){
			mission_one_desc.SetActive(false);
			mission_two_desc.SetActive(false);
			mission_three_desc.SetActive(true);
			btn3.GetComponent<Image>().sprite = btn3_pushed;
			btn1.GetComponent<Image>().sprite = btn1_normal;
			btn2.GetComponent<Image>().sprite = btn2_normal;
		}
	}




	public void mouse_hover(int id){
		if(selected_level == id){
		}else{

			if(id == 1){
				btn1.GetComponent<Image>().sprite = btn1_over;
			}else if(id == 2){
				btn2.GetComponent<Image>().sprite = btn2_over;
			}else if(id == 3){
				btn3.GetComponent<Image>().sprite = btn3_over;
			}
		}
	}


	public void exit_mouse(int id){
		if(selected_level == id){
		}else{
			if(id == 1){
				btn1.GetComponent<Image>().sprite = btn1_normal;
			}else if(id == 2){
				btn2.GetComponent<Image>().sprite = btn2_normal;
			}else if(id == 3){
				btn3.GetComponent<Image>().sprite = btn3_normal;
			}
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
