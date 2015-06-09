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


	public GameObject scout_ant_prefab;
	public int last_added_wp;
	public List<GameObject> nodes;
	public int node_index;
	public GameObject node_template;
	public bool pedit_toggle;
	public bool set_new;
	public int saved_node_id;

	private RaycastHit hit;


	// Use this for initialization
	void Start () {
		this.name = vars.path_manager_name;
		saved_node_id = -1;
		nodes = new List<GameObject> ();
		pedit_toggle = true;
		set_new = false;
		Vector3 pos = GameObject.Find (vars.base_name).transform.position;
		int tmp_id = nodes.Count;
		GameObject tmp = (GameObject)Instantiate (node_template, pos, Quaternion.identity);
		nodes.Add (tmp);
		nodes[tmp_id].gameObject.GetComponent<node>().node_const(pos ,tmp_id, tmp_id, true);
		nodes[tmp_id].gameObject.GetComponent<node> ().is_first_node = true;
	///	nodes[tmp_id].gameObject.GetComponent<node> ().is_first_inferface_connected = true;
	//	Debug.Log("start");
		nodes[tmp_id].gameObject.GetComponent<node>().calc_neighbour_distance();
		nodes[tmp_id].gameObject.GetComponent<node>().discoveres_by_scout = true;
		nodes[tmp_id].gameObject.GetComponent<node>().is_base_node = true;
		last_added_wp = tmp_id;
	}
	
	// Update is called once per frame
	void Update () {


    //add all ressources to list
  //  ressources.Clear();
  //  ressources.AddRange(GameObject.FindGameObjectsWithTag(vars.res_tag));

		if (vars.is_in_patheditmode ) {

			if(pedit_toggle){
				deselect_all_nodes ();
				enable_node_colliders ();
				pedit_toggle = false;

			}

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit)) {
				//Debug.Log(hit.collider.gameObject);
			//	Debug.Log(hit.normal);

			}
			Debug.DrawLine(-20*hit.point, hit.normal*20, Color.red);



			if(count_selected_nodes() == 0 && Input.GetMouseButtonDown(0)){



			if (Physics.Raycast (ray, out hit)) {
				//Debug.Log(hit.collider.gameObject);
				//	Debug.Log(hit.collider.gameObject.transform.rotation);
				foreach (GameObject n in nodes) {
						//SCHAUEN OB DAS DER BASENODE IST DENN DANN WIRD DER COLLIDER DER BASE GENOMMEN ANSTATT DER DES WPs
						if(!n.GetComponent<node>().is_base_node){

					if(hit.collider.gameObject == n.GetComponent<node>().click_collider.gameObject && n.GetComponent<node>().discoveres_by_scout){
						deselect_all_nodes ();
						disable_node_colliders();
						n.GetComponent<node>().is_selected = true;
						saved_node_id = n.GetComponent<node>().node_id;							
						//Debug.Log(n.name);	
					}//ende hit.collider

						}else{


							if(hit.collider.gameObject == GameObject.Find(vars.base_name).gameObject && n.GetComponent<node>().discoveres_by_scout){
								deselect_all_nodes ();
								disable_node_colliders();
								n.GetComponent<node>().is_selected = true;
								saved_node_id = n.GetComponent<node>().node_id;							
								//Debug.Log(n.name);	
							}//ende hit.collider


						}



				}//end for ecach
				
			}//ende raycast
			}// ende count == 0
	
			

		
			//& Input.GetMouseButtonDown(0)
			else if(count_selected_nodes() == 1 ){
			
				//Debug.Log (nodes[saved_node_id].GetComponent<node>().check_connection_state());
				//Debug.Log(nodes[saved_node_id].GetComponent<node>().is_mouse_in_node_range);
				Vector3 new_pos = get_node_with_intern_node_id(saved_node_id).curr_pos_in_circle; //get current mouse pos
				//Debug.Log(check_if_pos_inside_another_node(new_pos, saved_node_id));

//				Debug.Log(nodes[saved_node_id].GetComponent<node>().is_mouse_in_node_range  && check_if_pos_inside_another_node(new_pos, saved_node_id));

				if(get_node_with_intern_node_id(saved_node_id).is_mouse_in_node_range  && check_if_pos_inside_another_node(new_pos, saved_node_id) && Input.GetMouseButtonDown(0) ){
//					Debug.Log("selected : "+get_selected_node());
//					Debug.Log("node kann hier gesetzt werden : "+ new_pos);
					add_node(new_pos, saved_node_id, hit);
					get_node_with_intern_node_id(saved_node_id).cursor.SetActive(false); //disable the cursor
					get_node_with_intern_node_id(saved_node_id).cursor.transform.position = get_node_with_intern_node_id(saved_node_id).node_pos-new Vector3(0f,-1f,0f); //set the invisible cursor the node pos
					//disable_node_colliders();
					get_node_with_intern_node_id(saved_node_id).cursor.transform.rotation =  hit.collider.transform.rotation;
					enable_node_colliders();
					deselect_all_nodes();
        			complete_path_node_information();

				}else{
				//	nodes[saved_node_id].GetComponent<node>().is_selected = false;
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

   foreach (GameObject n in nodes) {
   	
			node nclass = n.gameObject.GetComponent<node>();

			nclass.neighbours.Clear();

		//	if(!nclass.neighbours.Contains(nclass.node_id)){
		//		nclass.neighbours.Add(nclass.node_id);
		//	}

			if(!nclass.neighbours.Contains(nclass.prev_node) && nclass.prev_node != nclass.node_id){
				nclass.neighbours.Add(nclass.prev_node);
			}

		
			List<int> nneig = nclass.neighbours;

			foreach (int s in nneig) {
				if(!nodes[s].GetComponent<node>().neighbours.Contains(nclass.node_id)){
					nodes[s].GetComponent<node>().neighbours.Add(nclass.node_id);
				}
			}

		

		}//ende foreach


		foreach (GameObject n in nodes) {
			node nclass = n.gameObject.GetComponent<node>();
			nclass.calc_neighbour_distance();
		}


		//GameObject.Find("ant").GetComponent<ant_path_walker>().node_added();


		//foreach (GameObject a in GameObject.FindGameObjectsWithTag("ant")) {
		//	a.gameObject.GetComponent<wp_path_manager>().add_nodes();
		//}
	}





  public Vector3 get_node_pos(int _nid)
  {


    //return nodes[_nid].gameObject.GetComponent<node>().node_pos;
		return get_node_with_intern_node_id(_nid).node_pos;



  }


	public node get_node_with_intern_node_id(int _nid){
		/*
		foreach (var n in nodes) {
			if(n.GetComponent<node>().node_id == _nid){

				return n.GetComponent<node>();
			}
		}

*/
		return GameObject.Find(vars.wp_node_name + "_" + _nid).GetComponent<node>();

	//	return null;
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


      if (n.gameObject.GetComponent<node>().connected_with_res && n.gameObject.GetComponent<node>().connected_res_id >= 0)
      {
        n.gameObject.GetComponent<node>().click_collider.SetActive(false);
      }
      else
      {
        n.gameObject.GetComponent<node>().click_collider.SetActive(true);
      }
		
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





	public void add_node(Vector3 pos, int prev_node_id, RaycastHit hit){
		int current_selected_node = get_selected_node ();
		if (current_selected_node >= 0) {
			int tmp_id = nodes.Count;
			GameObject tmp = (GameObject)Instantiate (node_template, pos, Quaternion.identity);
			tmp.gameObject.GetComponent<node>().node_const(pos, tmp_id ,prev_node_id, true);
			tmp.gameObject.GetComponent<node>().is_base_node = false;
			tmp.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.down, hit.normal);
			nodes.Add (tmp);

			//nodes[tmp_id].gameObject.GetComponent<node>().node_const(pos ,tmp_id, get_selected_node (), true);
			last_added_wp = tmp_id;



      //MANAGE RES -> connect to res with node

			foreach (GameObject r in GameObject.FindGameObjectsWithTag(vars.res_tag))
      {

        if (!r.GetComponent<ressource>().is_node_connected && r.GetComponent<ressource>().circle_holder.gameObject.GetComponent<selection_circle>().is_point_in_circle(pos) && r.GetComponent<ressource>().circle_holder.gameObject.GetComponent<selection_circle>().enabled)
        {
          			r.gameObject.GetComponent<ressource>().is_node_connected = true;
					get_node_with_intern_node_id(tmp_id).connected_with_res = true;
					get_node_with_intern_node_id(tmp_id).connected_res_id = r.GetComponent<ressource>().ressource_id;
					get_node_with_intern_node_id(tmp_id).node_pos = r.gameObject.GetComponent<ressource>().ressource_pos;
        }

      }





			Instantiate(scout_ant_prefab);
		} else {
			Debug.LogError("NODE KONNTE NICHT ERSTELLT WERDEN KA KEINER SELEKTOER WURDE");
		}
	}





	public int get_selected_node(){
		//int tmp;
		foreach (GameObject n in nodes) {
			if(n.gameObject.GetComponent<node>().is_selected && n.gameObject.GetComponent<node>().is_mouse_in_node_range){
				return n.gameObject.GetComponent<node>().node_id;
			}
		}
		return -1;
	}

}
