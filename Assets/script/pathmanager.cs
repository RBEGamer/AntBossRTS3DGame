using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class pathmanager : MonoBehaviour {
  /*
   TODO:
   * add next_node prev_node LIST<T>
   * add instance linerenderer
   
   * 
   * BUGS:
   * manchmal wir der cursor nicht angezeigt es kann aber gesetzt werden
   
   
   */


	public List<GameObject> nodes;
	public int node_index;
	public GameObject node_template;

	public bool pedit_toggle;

	public bool set_new;


	public int saved_node_id;
	// Use this for initialization
	void Start () {
		saved_node_id = -1;
		nodes = new List<GameObject> ();
		pedit_toggle = true;
		set_new = false;
		Vector3 pos = GameObject.Find ("base").transform.position;
		int tmp_id = nodes.Count;
		GameObject tmp = (GameObject)Instantiate (node_template, pos, Quaternion.identity);
		nodes.Add (tmp);
		nodes[tmp_id].gameObject.GetComponent<node>().node_const(pos ,tmp_id, -1, true);
		nodes[tmp_id].gameObject.GetComponent<node> ().is_first_node = true;
		nodes[tmp_id].gameObject.GetComponent<node> ().is_first_inferface_connected = true;
		Debug.Log("start");
	}
	
	// Update is called once per frame
	void Update () {



		if (vars.is_in_patheditmode ) {

			if(pedit_toggle){
				deselect_all_nodes ();
				enable_node_colliders ();
				pedit_toggle = false;

			}

	
			if(count_selected_nodes() == 0 && Input.GetMouseButtonDown(0)){
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				//Debug.Log(hit.collider.gameObject);
				
				foreach (GameObject n in nodes) {
					if(hit.collider.gameObject == n.GetComponent<node>().click_collider.gameObject){
						deselect_all_nodes ();
						disable_node_colliders();
						n.GetComponent<node>().is_selected = true;
						saved_node_id = n.GetComponent<node>().node_id;
							
						//Debug.Log(n.name);		
					}//ende hit.collider
				}//end for ecach
				
			}//ende raycast
			}// ende count == 0
	
			

		
			//& Input.GetMouseButtonDown(0)
			else if(count_selected_nodes() == 1 ){
			
				//Debug.Log (nodes[saved_node_id].GetComponent<node>().check_connection_state());
				//Debug.Log(nodes[saved_node_id].GetComponent<node>().is_mouse_in_node_range);
				Vector3 new_pos = nodes[saved_node_id].GetComponent<node>().curr_pos_in_circle; //get current mouse pos
				//Debug.Log(check_if_pos_inside_another_node(new_pos, saved_node_id));

				Debug.Log(nodes[saved_node_id].GetComponent<node>().is_mouse_in_node_range && nodes[saved_node_id].GetComponent<node>().check_connection_state() && check_if_pos_inside_another_node(new_pos, saved_node_id));

				if(nodes[saved_node_id].GetComponent<node>().is_mouse_in_node_range && nodes[saved_node_id].GetComponent<node>().check_connection_state() && check_if_pos_inside_another_node(new_pos, saved_node_id) && Input.GetMouseButtonDown(0) ){
					Debug.Log("selected : "+get_selected_node());
					Debug.Log("node kann hier gesetzt werden : "+ new_pos);
					add_node(new_pos, saved_node_id);
					nodes[saved_node_id].GetComponent<node>().cursor.SetActive(false); //disable the cursor
					nodes[saved_node_id].GetComponent<node>().cursor.transform.position = nodes[saved_node_id].GetComponent<node>().transform.position-new Vector3(0f,-1f,0f); //set the invisible cursor the node pos
					//disable_node_colliders();

					enable_node_colliders();
					deselect_all_nodes();
       //   complete_path_node_information();

				}

			
			}




		
		} else {

			if(!pedit_toggle){
				deselect_all_nodes();
			disable_node_colliders();
			}

			pedit_toggle = true;
		
		}//ende is_in_pathnode

	}






	public void complete_path_node_information(){

    int nid = 0;
    int mid = 0;

    int nprev = 0;
    int mprev = 0;

    int mnext = 0;
		foreach (GameObject n in nodes) {

      nid = n.gameObject.GetComponent<node>().node_id;
      nprev = n.gameObject.GetComponent<node>().prev_node;

      foreach (GameObject m in nodes)
      {
        mid = m.gameObject.GetComponent<node>().node_id;
        mprev = m.gameObject.GetComponent<node>().prev_node;


        if (mid == nprev) { mnext = nid; } else { }// mnext = -2; }
        m.gameObject.GetComponent<node>().next_node = mnext;


      }



		}




	}





  public Vector3 get_node_pos(int _nid)
  {


    return nodes[_nid].gameObject.GetComponent<node>().node_pos;




  }






	public bool check_if_pos_inside_another_node(Vector3 ckpos, int _node_id){
		/*
		bool res = false;
		foreach (GameObject n in nodes) {
			if(n.gameObject.GetComponent<node>().node_id == _node_id){
				//wenn dieser node getest werden soll
			}else{
				if(n.gameObject.GetComponent<node>().check_if_pos_in_cirlce_at_point(ckpos)){
					res = true;
				}
			}
			return res;


		}
	*/	
	//-> hier alle checken ausser die tmp
		return true;
	}

	public void enable_node_colliders(){
		foreach (GameObject n in nodes) {
			n.gameObject.GetComponent<node>().click_collider.SetActive(true);
		}
	}


	public void disable_node_colliders(){
		foreach (GameObject n in nodes) {
			n.gameObject.GetComponent<node>().click_collider.SetActive(false);
		}
	}


	public void deselect_all_nodes(){
		foreach (GameObject n in nodes) {
			n.gameObject.GetComponent<node>().is_selected = false;
		}
	
	}


	public int count_selected_nodes(){
		int cont = 0;
		foreach (GameObject n in nodes) {
			if(n.gameObject.GetComponent<node>().is_selected){
				cont++;
			}	
		}
		return cont;
	}





	public void add_node(Vector3 pos, int prev_node_id){
		int current_selected_node = get_selected_node ();
		if (current_selected_node >= 0) {
			int tmp_id = nodes.Count;
			GameObject tmp = (GameObject)Instantiate (node_template, pos, Quaternion.identity);
			nodes.Add (tmp);
			nodes[tmp_id].gameObject.GetComponent<node>().node_const(pos, tmp_id ,prev_node_id, true);
      nodes[prev_node_id].gameObject.GetComponent<node>().next_node = tmp_id;// set here the nextnode id
			//nodes[tmp_id].gameObject.GetComponent<node>().node_const(pos ,tmp_id, get_selected_node (), true);
		} else {
			Debug.LogError("NODE KONNTE NICHT ERSTELLT WERDEN KA KEINER SELEKTOER WURDE");
		}
	}





	public int get_selected_node(){
		int tmp;
		foreach (GameObject n in nodes) {
			if(n.gameObject.GetComponent<node>().is_selected && n.gameObject.GetComponent<node>().is_mouse_in_node_range){
				return n.gameObject.GetComponent<node>().node_id;
			}
		}
		return -1;
	}

}
