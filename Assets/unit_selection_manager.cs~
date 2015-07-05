using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class unit_selection_manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


	public Sprite empty_group_holder;
	public Sprite[] group_icons;
	public Sprite tmp_group_icon;
	public bool[] group_states = new bool[18];


	public void map_group_to_slot_0(int group_id){


		if(GameObject.Find(vars.UnitGroupUIManager).GetComponent<UnitGroupUIManager>().unitGroupList.Count >= group_id-1){
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().connected_unit_to_ui = group_id;
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().slot_0_set_unit();

			int counter = 0;
			foreach (UnitGroupFriendly item in GameObject.Find(vars.UnitGroupUIManager).GetComponent<UnitGroupUIManager>().unitGroupList) {

				if(counter == group_id-1){
				item.OnSelected();
					break;
				}
				counter++;

			}

		}

	}


	public void create_new_group(){
		if((GameObject.Find(vars.base_name).GetComponent<UnitGroupCache>().unitGroupsSaved.Count+GameObject.Find(vars.UnitGroupUIManager).GetComponent<UnitGroupUIManager>().unitGroupList.Count) < 18){
			SavedUnitGroup svg = GameObject.Find(vars.base_name).GetComponent<UnitGroupCache>().createNewGroup();
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().sug = svg;
			//GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().connected_unit_to_ui = group_id;
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().slot_0_set_unit();
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().is_saved_group = true;
			Debug.Log("group added");
		}
	}




	// Update is called once per frame
	void Update () {
	
		for (int i = 1; i < 19; i++) {
			group_states[i-1] = false;
			GameObject.Find("unit_selection_btn_" + i.ToString()).GetComponent<Image>().sprite = empty_group_holder;
		}





		//
		int counter =0 ;
		foreach (SavedUnitGroup group in GameObject.Find(vars.base_name).GetComponent<UnitGroupCache>().unitGroupsSaved){
			group_states[counter] = true;
			counter++;
			GameObject.Find("unit_selection_btn_" + counter.ToString()).GetComponent<Image>().sprite = tmp_group_icon;


		}


	}
}
