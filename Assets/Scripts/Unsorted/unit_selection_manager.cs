using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class unit_selection_manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//refresh_unit_group_selection_ui();
	}




	public Sprite empty_group_holder;
	public Sprite[] group_icons;
	public Sprite tmp_group_icon;
	private bool[] group_states = new bool[18];


	public void map_group_to_slot_0(int group_id){
		if(GameObject.Find("unit_selection_btn_" + group_id.ToString()).GetComponent<Image>().sprite == empty_group_holder){
			create_new_group();
		}
			

		/*foreach (UnitGroupFriendly item in GameObject.Find(vars.UnitGroupUIManager).GetComponent<UIUnitFighterSelection>().unitGroupList) {
			item.OnUnselected();
		}*/
		foreach (UnitGroupFriendlyScript item in GameObject.Find(vars.UnitGroupUIManager).GetComponent<UIUnitFighterSelection>().unitGroupList) {
			item.OnUnselected();
		}



		if(group_states[group_id-1]){
			// TMP GROUP
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().is_saved_group = true;
			int offset = GameObject.Find(vars.UnitGroupUIManager).GetComponent<UIUnitFighterSelection>().unitGroupList.Count;

			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().sug = GameObject.Find(vars.base_name).GetComponent<UnitGroupCache>().unitGroupsSaved[group_id-offset-1];
		}else{
			//NICHT TMP GROUP
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().is_saved_group = false;
			GameObject.Find(vars.UnitGroupUIManager).GetComponent<UIUnitFighterSelection>().unitGroupList[group_id-1].OnSelected();
		}

		refresh_unit_group_selection_ui();


		//if(GameObject.Find(vars.UnitGroupUIManager).GetComponent<UIUnitFighterSelection>().unitGroupList.Count >= group_id-1){
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().connected_unit_to_ui = group_id-1;
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().slot_0_set_unit();

			




	}


	public void create_new_group(){
		/*foreach (UnitGroupFriendly item in GameObject.Find(vars.UnitGroupUIManager).GetComponent<UIUnitFighterSelection>().unitGroupList) {
			item.OnUnselected();
		}*/
		foreach (UnitGroupFriendlyScript item in GameObject.Find(vars.UnitGroupUIManager).GetComponent<UIUnitFighterSelection>().unitGroupList) {
			item.OnUnselected();
		}
		if((GameObject.Find(vars.base_name).GetComponent<UnitGroupCache>().unitGroupsSaved.Count+GameObject.Find(vars.UnitGroupUIManager).GetComponent<UIUnitFighterSelection>().unitGroupList.Count) < 18){
			SavedUnitGroup svg = GameObject.Find(vars.base_name).GetComponent<UnitGroupCache>().createNewGroup();
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().sug = svg;
			//GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().connected_unit_to_ui = group_id;
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().slot_0_set_unit();
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().is_saved_group = true;
			Debug.Log("group added");
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().show_upgrade_ui = false;
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().manage_upgrade_ui();
		refresh_unit_group_selection_ui();
		}
	}




	public  void refresh_unit_group_selection_ui(){

		for (int i = 1; i < 19; i++) {
			group_states[i-1] = false;
			GameObject.Find("unit_selection_btn_" + i.ToString()).GetComponent<Image>().sprite = empty_group_holder;
			GameObject.Find("unit_selection_btn_" + i.ToString()).GetComponent<Button>().interactable = true;
		}
		
		
		int counter =0 ;
		/*foreach (UnitGroupFriendly group in GameObject.Find(vars.UnitGroupUIManager).GetComponent<UIUnitFighterSelection>().unitGroupList){
			group_states[counter] = false;
			counter++;
			GameObject.Find("unit_selection_btn_" + counter.ToString()).GetComponent<Image>().sprite = group_icons[counter-1];
			GameObject.Find("unit_selection_btn_" + counter.ToString()).GetComponent<Button>().interactable = true;
		}*/
		foreach (UnitGroupFriendlyScript group in GameObject.Find(vars.UnitGroupUIManager).GetComponent<UIUnitFighterSelection>().unitGroupList){
			group_states[counter] = false;
			counter++;
			GameObject.Find("unit_selection_btn_" + counter.ToString()).GetComponent<Image>().sprite = group_icons[counter-1];
			GameObject.Find("unit_selection_btn_" + counter.ToString()).GetComponent<Button>().interactable = true;
		}

		//Debug.Log(GameObject.Find(vars.base_name).GetComponent<UnitGroupCache>().unitGroupsSaved.Count);
		//
		//int counter =0 ;
		foreach (SavedUnitGroup tgroup in GameObject.Find(vars.base_name).GetComponent<UnitGroupCache>().unitGroupsSaved){
			group_states[counter] = true;
			counter++;
			GameObject.Find("unit_selection_btn_" + counter.ToString()).GetComponent<Image>().sprite = tmp_group_icon;
			GameObject.Find("unit_selection_btn_" + counter.ToString()).GetComponent<Button>().interactable = true;
		}

	}

	// Update is called once per frame
	void Update () {
	
		refresh_unit_group_selection_ui();

	}
}
