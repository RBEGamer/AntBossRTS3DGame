using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class res_selection_ui_manager : MonoBehaviour {


	public Sprite res_icon_ui_type_a;
	public Sprite res_icon_ui_type_b;
	//public Sprite res_icon_ui_type_c;
	public Sprite res_icon_ui_none;
	// Use this for initialization
	void Start () {
		update_res_selection_ui();
	}



	public void update_res_selection_ui(){
    //Debug.Log("res updated");
		for (int i = 0; i < 18; i++) {
			GameObject.Find("res_selection_btn_" + (i+1).ToString()).GetComponent<Image>().sprite = res_icon_ui_none;
			GameObject.Find("res_selection_btn_" + (i+1).ToString()).GetComponent<Button>().interactable = false;
		}
		


		int counter = 0;
    foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag))
    {


      if (n.GetComponent<ressource>().is_node_connected)
      {
        counter++;
        GameObject.Find("res_selection_btn_" + (n.GetComponent<ressource>().ressource_id + 1).ToString()).GetComponent<Button>().interactable = true;
        if (n.GetComponent<ressource>().res_type == vars.ressource_type.A)
        {
          GameObject.Find("res_selection_btn_" + (n.GetComponent<ressource>().ressource_id + 1).ToString()).GetComponent<Image>().sprite = res_icon_ui_type_a;
        }
        else if (n.GetComponent<ressource>().res_type == vars.ressource_type.B)
        {
          GameObject.Find("res_selection_btn_" + (n.GetComponent<ressource>().ressource_id + 1).ToString()).GetComponent<Image>().sprite = res_icon_ui_type_b;
          //	}else if(n.GetComponent<ressource>().res_type == vars.ressource_type.C){
          //		GameObject.Find("res_selection_btn_" + counter.ToString()).GetComponent<Image>().sprite = res_icon_ui_type_c;
        }
      }
    }
	}
	
	
	
	public void select_res(int count){

		int wp_id = -1;
		Vector3 wp_pos = new Vector3();
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.res_tag)) {
			if( n.GetComponent<ressource>().ressource_id == count){
				wp_id =  n.GetComponent<path_point>().waypoint_id;
				wp_pos = n.gameObject.transform.position;
			}
		}
		Debug.Log(wp_id);
		float x = wp_pos.x;
		float z = wp_pos.z;
		GameObject.Find("audio_playback_manager").GetComponent<audio_playback_manager>().play_ressource_select_sound();
		GameObject.Find(vars.path_manager_name).GetComponent<wp_manager>().map_wp_to_ui(wp_id);

			GameObject.Find(vars.main_camera_script_holder_name).GetComponent<camera_movement>().camera_offset = new Vector3(x,GameObject.Find(vars.main_camera_script_holder_name).GetComponent<camera_movement>().camera_offset.y,z);
		
	}



	// Update is called once per frame
	void FixedUpdate () {
	
}
}
