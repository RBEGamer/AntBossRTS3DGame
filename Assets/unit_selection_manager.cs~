using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class unit_selection_manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


	public Sprite empty_group_holder;

	public void map_group_to_slot_0(int group_id){


	}


	public void create_new_group(){

	}



	public Sprite[] group_icons;
	// Update is called once per frame
	void Update () {
	
		for (int i = 1; i < 18; i++) {
			GameObject.Find("unit_selection_btn_" + i.ToString()).GetComponent<Image>().sprite = empty_group_holder;
		}

		int counter =0 ;
		foreach (UnitGroupFriendly group in GameObject.Find(vars.UnitGroupUIManager).GetComponent<UnitGroupUIManager>().unitGroupList){
			counter++;
			GameObject.Find("unit_selection_btn_" + counter.ToString()).GetComponent<Image>().sprite = group_icons[counter-1];

		}


	}
}
