using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class wp_path_manager : MonoBehaviour {


	/*
	 * check ob start und zeil node ein gültiger node ist
	 * immer checken ob alle nodes initialiert wurden
	 * 
	 * */

	public int start_node_id;
	public int ziel_node_id;
	public int node_count;
	public List<GameObject> nodes;


	public float hard_exit = 15.0f;
	public float currhe = 0 ;


	public List<int> final_path;
	public bool path_changed = false;
	public int currend_selected_node = -1;
	// Use this for initialization
	void Start () {
		nodes.Clear ();
		nodes.AddRange(GameObject.FindGameObjectsWithTag ("wp_node"));
		node_count = nodes.Count;

	
	}
	
	// Update is called once per frame
	void Update () {

		if (path_changed) {
			path_changed = false;
			compute_path ();

		}

//		Debug.Log (get_node_with_lowest_distance ());

	}//ende update





	void compute_path(){


		nodes.Clear ();

		this.nodes = GameObject.Find(vars.path_manager_name).GetComponent<pathmanager>().nodes;
		//nodes.AddRange(GameObject.FindGameObjectsWithTag (vars.wp_node_tag));
		node_count = nodes.Count;
		
		if (node_count > 1) {
			
			//VORARBEIT:
			set_all_to_unvisited (); // alle nodes auf unbesucht setzten
			set_distance_to_infinite (); //alle distanzen der nodes auf unendlich setzten
			set_ancestor_to_null (); // setzte alle node vorgänger auf null
			set_all_untag ();
			
			
			get_node_component (start_node_id).distance = 0; //setzte sie distance des startnode auf 0 denn von A -> A ist 0
			get_node_component (start_node_id).ancestor = start_node_id; //der nachbar des startnodes ist der startnode selber
			
			
			//solange bis alle nodes besucht wurden
			while (!check_if_all_visited()) {

				if(check_if_all_visited()){
					Debug.Log("all visited");
					break;
					
				}



				currend_selected_node = get_node_with_lowest_distance (); //schaue welcher node die niedrigste distanz hat
				//node_id_tmp = get_node_with_lowest_distance();
			//	mark_node_as_tagged (currend_tagged_node); //markeire diesen
			//	mark_node_as_visited(currend_tagged_node); //setzte diesen auf besucht
				
				get_node_component(currend_selected_node).visited = true;


				
				List<int> current_node_neighbours = get_node_component(currend_selected_node).neighbours;
				
				//current_node_neighbours.AddRange();
				string tmp = "";
				foreach (int dn in current_node_neighbours) {
					tmp += "  ,"+dn+"  ";
	
}
				//Debug.Log("added " + tmp + " neighbours ");
				//für alle unbesuchten nachbarn
				foreach (int neighbour in current_node_neighbours) {
					if(!get_node_component(neighbour).visited){

					// A -> nachbarn = sum = Aweight + egdeweight
					//wenn sum < B weight
					float currnw = get_node_component(neighbour).distance;
					float currcw = get_node_component(currend_selected_node).distance;
					float currew =  get_node_component(currend_selected_node).get_edge_weight(neighbour);

						if(currew < 0){Debug.Log("ERR Edgeweight < 0 : " +currew);

						break;
					}



					float sum = currcw+currew;
					Debug.Log("SUM : " +sum + "     node dist : " + currnw);
					if(sum < currnw){
							Debug.Log("set nachbar to:" + currend_selected_node);
						get_node_component(neighbour).distance = sum;
							get_node_component(neighbour).ancestor = currend_selected_node;

					}


					
					//addiere distanz und kantengewichtung
					//wenn diese summe geringer ist als das aktuelle
					//  -> setzten die summe als distanz des neigbours
					//  -> und setzte den taaged node als vorgängeer
					
					
					//}else{
					//	Debug.Log("knoten wurde schon besucht");
					}
				}//ende forcehc
				
				
				
				
				currhe += Time.deltaTime*0.5f;if (currhe > hard_exit){Debug.Log("Zeitüberschreitung");currhe = 0; break;} //damit der vorgang abgebrochen wir wenn es zu öange sauert
			}//ende while







			//hier fertug

			//wo startknoten == startknoten

			final_path.Clear();

			int curr_id_fp = ziel_node_id;
			//solange bis man beim startnode angkommen ist
			while (	get_node_component(curr_id_fp).ancestor != 	get_node_component(curr_id_fp).node_id) {


				final_path.Add(curr_id_fp);
				curr_id_fp = get_node_component(curr_id_fp).ancestor;




				currhe += Time.deltaTime*0.5f;if (currhe > hard_exit){Debug.Log("Zeitüberschreitung");currhe = 0; break;} //damit der vorgang abgebrochen wir wenn es zu öange sauert
			}
			final_path.Add(start_node_id);


			string tmpfp = "";
			foreach (int dn in final_path) {
				tmpfp += "  ->"+dn+"  ";
				
			}
			Debug.Log("FINALPATH : " + tmpfp);
			
		}//if > 0
		
		
		
		
		
		
		
		currhe = 0;//setzte die zeit zurück

	}


								//hier den node mit der geringsten distance und unbesucht
	private int  get_node_with_lowest_distance(){
		int saved_node_id = -1;
		float saved_dist = Mathf.Infinity;

		foreach (GameObject n in nodes) {

			if(n.GetComponent<node>().distance < saved_dist && !n.GetComponent<node>().visited){
				saved_dist = n.GetComponent<node>().distance;
				saved_node_id = n.GetComponent<node>().node_id;
				Debug.Log("the node with the lowest distance is : " + saved_node_id);
				return saved_node_id;
			}


		}

		//node_id_tmp = saved_node_id;
		return saved_node_id;
	}




	private void mark_node_as_tagged(int id){
		foreach (GameObject n in nodes) {
			if(n.GetComponent<node>().node_id == id){
				n.GetComponent<node>().tagged = true;
				Debug.Log("der node "+ id + "wurde markiert");
			}
		}
	}


	private void mark_node_as_visited(int id){
		foreach (GameObject n in nodes) {
			if(n.GetComponent<node>().node_id == id){
				n.GetComponent<node>().visited = true;
				Debug.Log("der node " + id + " wurde auf besucht gesetzt");
			}
		}
	}


	private bool check_if_all_visited(){
		bool node_checked = true;
		foreach (GameObject n in nodes) {
			if(!n.GetComponent<node>().visited){
				node_checked = false;
			}
		}
		return node_checked;
	}



	private node get_node_component(int id){
		node tmp;
		foreach (GameObject n in nodes) {
			if(n.GetComponent<node>().node_id == id){
				 tmp = n.GetComponent<node>();
				return tmp;
			}
		}
		return null;
	}


	private void set_all_to_unvisited(){
		foreach (GameObject	 n in nodes) {
			n.GetComponent<node>().visited = false;
		}
	}

	private void set_all_to_visited(){
		foreach (GameObject	 n in nodes) {
			n.GetComponent<node>().visited = true;
		}
	}

	private void set_all_untag(){
		foreach (GameObject	 n in nodes) {
			n.GetComponent<node>().tagged = false;
		}
	}

	private void set_all_tagged(){
		foreach (GameObject	 n in nodes) {
			n.GetComponent<node>().tagged = true;
		}
	}


	private void set_distance_to_infinite(){
		foreach (GameObject	 n in nodes) {
			n.GetComponent<node>().distance = Mathf.Infinity;
		}
	}

	private void set_distance_to_zero(){
		foreach (GameObject	 n in nodes) {
			n.GetComponent<node>().distance = 0;
		}
	}

	private void set_ancestor_to_null(){
		foreach (GameObject	 n in nodes) {
			n.GetComponent<node>().ancestor = -1;
		}
	}

}
