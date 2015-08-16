using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SerializeField]
public class wp_manager : MonoBehaviour
{


	public GameObject waypoint_prefab;


	public enum wp_mode
	{
		verschieben,
		adden,
		deleten,
		selecten,
		none,
		connecten
	}

	public class dijkstra_node
	{
		public int node_id;
		public List<wp_edge> node_edges;
		public List<int> neighbours;
		public float distance;
		public bool visited;
		public bool tagged;
		public int ancestor;

		public dijkstra_node ()
		{
			node_id = -1;
			node_edges = new List<wp_edge> ();
			neighbours = new List<int> ();
			distance = Mathf.Infinity;
			visited = false;
			tagged = false;
			ancestor = -1;
			Debug.Log("added nijkstra_node : " + node_id.ToString());
		}

		public dijkstra_node (int nid, List<wp_edge> ede, List<int> neig, float dist, bool v, bool t, int a)
		{
			node_id = -1;
			node_edges.AddRange (ede);
			neighbours.AddRange (neig);
			distance = dist;
			visited = v;
			tagged = t;
			ancestor = a;
			Debug.Log("added nijkstra_node : " + node_id.ToString());
			log_info();
		}

		public dijkstra_node (int nodeid)
		{
			node_id = nodeid;
			node_edges = new List<wp_edge> ();
			neighbours = new List<int> ();
			distance = Mathf.Infinity;
			visited = false;
			tagged = false;
			ancestor = -1;
			Debug.Log("added nijkstra_node : " + node_id.ToString());
		}

		public void log_info(){

			string tmp = "EDGES : ";
			foreach (wp_edge item in node_edges) {
				tmp += " ; " + item.source_id.ToString() + "->" + item.dest_id.ToString();
			}
			tmp += " NEIGHBOURS : ";
			foreach (int item in neighbours) {
				tmp += " ; " + item.ToString();
			}

			Debug.Log(" NODE INFO : id:" + node_id.ToString() + " " + tmp + "  Distance : " + distance.ToString() + " Visited : " + visited.ToString() + " ANCESTOR : " + ancestor.ToString());
		}
	}



	public class wp_edge : MonoBehaviour {
		
		public int source_id;
		public int dest_id;
		public float weight;
		public float edge_id;
		
		public wp_edge(int _edge_id, int _source_id, int _dest_id, float _weight){
			source_id = _source_id;
			dest_id = _dest_id;
			weight = _weight;
			edge_id = _edge_id;
			debug_edge_info();
		}
		public void debug_edge_info(){
			Debug.Log ("ADDED EDGE FROM :" + source_id + " TO " + dest_id + " WITH THE WEIGHT : "+ weight);
		}
	}



	private List<dijkstra_node> dijkstra_node_list = new List<dijkstra_node> ();

	//hier wird die edgelist in einen dijkstra_node konvertiert ein node beinhaltet: die eigene id, liste der nachbarn, list der edges zu den nachbarn mit der länge
	private void convert_edgelist_to_dijkstra_node_list ()
	{
		Debug.Log ("convert edgelist :" + edgelist.Count.ToString ());
		//alle 
		List<int> individual_nodes = new List<int> ();
		individual_nodes.Clear ();
		dijkstra_node_list.Clear (); //clear list
		//hier teilen wir die edges in die nodes auf
		foreach (wp_edge n in edgelist) {
			bool war_drinnen = false;
			//adde alle anfgangspunkte
			foreach (int inode in individual_nodes) {
				if (inode == n.source_id) {
					war_drinnen = true;
				}
			}
			if (!war_drinnen) {
				individual_nodes.Add (n.source_id);
			}
		}

		foreach (wp_edge n in edgelist) {
			bool war_drinnen = false;
			//adde alle anfgangspunkte
			foreach (int inode in individual_nodes) {
				if (inode == n.dest_id) {
					war_drinnen = true;
				}
			}
			if (!war_drinnen) {
				individual_nodes.Add (n.dest_id);
			}
		}

		//hier für jeden gerade erzeugten node einen dijkstra_node erstellen dieser enthät alle infos für den graph
		foreach (int inode in individual_nodes) {
			dijkstra_node new_dnode = new dijkstra_node (inode);
			Debug.Log("new dnode : " + inode.ToString());
			//alle edges hinzufügen
			foreach (wp_edge edge in edgelist) {
				if (edge.source_id == inode) {
					new_dnode.node_edges.Add (edge);
					Debug.Log(" neigh : " + edge.dest_id);
					new_dnode.neighbours.Add (edge.dest_id);
				}
			}
			new_dnode.log_info();
			dijkstra_node_list.Add (new_dnode);
		}
		Debug.Log ("conversion complete : " + dijkstra_node_list.Count.ToString ());





		Dijkstra_Init(dijkstra_node_list, 1);



	


	}

	public List<wp_edge> edgelist;
	public int WP_COUNTER_MAIN = 1;
	public Vector3 selected_wp_pos;
	public Quaternion selected_wp_rot;
	public Vector3 selected_wp_scale;
	public int selected_wp_id;
	public wp_mode curr_wp_mode;
	RaycastHit hit;

	void Awake ()
	{
		WP_COUNTER_MAIN = 1;
		this.name = vars.path_manager_name;
	}
	// Use this for initialization
	void Start ()
	{
		disable_all_range_circles ();
		curr_wp_mode = wp_mode.selecten;
		foreach (GameObject n in GameObject.FindGameObjectsWithTag("node")) {
			n.GetComponent<path_point> ().is_selected = false;
		}
	}

	public int get_wp_id ()
	{

		WP_COUNTER_MAIN++;
		return WP_COUNTER_MAIN;

	}

	private int get_selected_wp ()
	{
		foreach (GameObject n in GameObject.FindGameObjectsWithTag("node")) {
			if (n.GetComponent<path_point> ().is_selected) {
				selected_wp_id = n.GetComponent<path_point> ().waypoint_id;
				return n.GetComponent<path_point> ().waypoint_id;
			}
		}
		return -1;
	}
	
	private int get_selected_count ()
	{
		int counter = 0;
		foreach (GameObject n in GameObject.FindGameObjectsWithTag("node")) {
			if (n.GetComponent<path_point> ().is_selected) {
				counter++;
			}
		}
		return counter;
	}
	
	private void deselect_all_waypoints ()
	{
		foreach (GameObject n in GameObject.FindGameObjectsWithTag("node")) {
			n.GetComponent<path_point> ().is_selected = false;
			map_wp_to_ui (-1);
		}
		selected_wp_id = -1;
	}

	private void disable_range_cirlce_on_slelected (int id)
	{
		if (id > 0) {
			GameObject.Find ("node_" + id).GetComponent<path_point> ().disable_range_cycle ();
		}
	}

	private void enable_range_cirlce_on_slelected (int id)
	{
		if (id > 0) {
			GameObject.Find ("node_" + id).GetComponent<path_point> ().enable_range_cycle ();
		}
	}

	private void disable_all_range_circles ()
	{
		foreach (GameObject n in GameObject.FindGameObjectsWithTag("node")) {
			n.GetComponent<path_point> ().disable_range_cycle ();
		}
	}

	private void enable_all_range_circles ()
	{
		foreach (GameObject n in GameObject.FindGameObjectsWithTag("node")) {
			n.GetComponent<path_point> ().enable_range_cycle ();
		}
	}

	private void disable_collider_on_slelected (int id)
	{
		if (id > 0) {
			GameObject.Find ("node_" + id).GetComponent<path_point> ().disabled_collider ();
		}
	}

	private void enable_collider_on_selected (int id)
	{
		if (id > 0) {
			GameObject.Find ("node_" + id).GetComponent<path_point> ().enable_collider ();
		}
	}
	
	private void select_waypoint_with_id (int id)
	{
		GameObject.Find ("node_" + id).GetComponent<path_point> ().is_selected = true;
		selected_wp_pos = GameObject.Find ("node_" + id).transform.position;
		selected_wp_rot = GameObject.Find ("node_" + id).transform.rotation;
		selected_wp_scale = GameObject.Find ("node_" + id).transform.localScale;
	}

	private void disable_all_colliders ()
	{
		foreach (GameObject n in GameObject.FindGameObjectsWithTag("node")) {
			n.GetComponent<path_point> ().disabled_collider ();
		}
	}

	private void enable_all_colliders ()
	{
		foreach (GameObject n in GameObject.FindGameObjectsWithTag("node")) {
			n.GetComponent<path_point> ().enable_collider ();
		}
	}
	
	private void map_wp_to_ui (int wp_id)
	{
		GameObject.Find ("ui_manager").GetComponent<ui_manager> ().connected_wp_id = wp_id;
		GameObject.Find ("ui_manager").GetComponent<ui_manager> ().slot_0_set_waypoint();
		disable_all_range_circles ();
		enable_range_cirlce_on_slelected (wp_id);
	}
	
	private GameObject GetClickedGameObject ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			return hit.transform.gameObject;
		} else {
			return null;
		}
	}



	
	//hier werden die verbindungen angezeigt zb durch eine liste druch linerenderers oder so
	void refresh_edge_visuals ()
	{
		Debug.Log ("refresh visals");
		foreach (GameObject n in GameObject.FindGameObjectsWithTag("node")) {
			int sid = n.GetComponent<path_point> ().waypoint_id;
			Vector3 spos = new Vector3 (n.gameObject.transform.position.x, n.gameObject.transform.position.y + n.gameObject.transform.localScale.y, n.gameObject.transform.position.z);
			int pointcounter = 0;
			for (int i = 0; i < edgelist.Count; i++) {
				if (edgelist [i].source_id == sid) {
					pointcounter++;
				}
			}
			Debug.Log ("set vertex count to :" + (pointcounter * 3).ToString ());
			n.GetComponent<LineRenderer> ().SetVertexCount (pointcounter * 3);
			int pointcounter_pos = 0;
			for (int i = 0; i < edgelist.Count; i++) {
				if (edgelist [i].source_id == sid) {
					Vector3 dpos_original = GameObject.Find ("node_" + edgelist [i].dest_id.ToString ()).transform.position;
					Vector3 dscale = GameObject.Find ("node_" + edgelist [i].dest_id.ToString ()).transform.localScale; 
					Vector3 dpos = new Vector3 (dpos_original.x, dpos_original.y + dscale.y, dpos_original.z);

					Debug.Log ("ADD vertext point : from " + spos.ToString () + " to " + dpos.ToString ());
					//SET THE POINTS
					n.GetComponent<LineRenderer> ().SetPosition (pointcounter_pos, spos);
					pointcounter_pos++;
					n.GetComponent<LineRenderer> ().SetPosition (pointcounter_pos, dpos);
					pointcounter_pos++;
					n.GetComponent<LineRenderer> ().SetPosition (pointcounter_pos, spos);
					pointcounter_pos++;
				}
			}
		}
	}

	public void remove_connections ()
	{
		curr_wp_mode = wp_mode.none;
		if (selected_wp_id > 0) {
			List<wp_edge> edges_to_remove = new List<wp_edge> ();
			foreach (wp_edge n in edgelist) {
				if (n.dest_id == selected_wp_id || n.source_id == selected_wp_id) {
					edges_to_remove.Add (n);
				}
			}
			foreach (wp_edge n in edges_to_remove) {
				edgelist.Remove (n);
			}
			refresh_edge_visuals ();
		}
	}



	// Update is called once per frame
	void Update ()
	{
	

		//DEBUG
		if (curr_wp_mode == wp_mode.none) {
			curr_wp_mode = wp_mode.selecten;
			Debug.Log("none to selecten");
			disable_all_range_circles();
			disable_all_colliders ();
		}



		//WP DELTETEN auch hier wieder den wp in der base ausnehmen
		if (curr_wp_mode == wp_mode.deleten && selected_wp_id > 1) {
			if (GameObject.Find ("node_" + selected_wp_id.ToString ()).GetComponent<path_point> ().type == path_point.node_type.normal_node) {
				//alle edes löschen
				foreach (wp_edge n in edgelist) {
					if (n.dest_id == selected_wp_id || n.source_id == selected_wp_id) {
						edgelist.Remove (n);
					}
				}
				//WP Objekt löschen
				Destroy (GameObject.Find ("node_" + selected_wp_id.ToString ()));
				//alle linien neu anzeigen lassen
				refresh_edge_visuals ();
				//switch ui view to default
				GameObject.Find ("ui_manager").GetComponent<ui_manager> ().slot_0_set_empty();
			}
		}


		//WP ADDEN
		if (curr_wp_mode == wp_mode.adden) {
			disable_all_range_circles();
			//if(GameObject.Find ("node_" + selected_wp_id.ToString ()).GetComponent<path_point>().type == path_point.node_type.normal_node){
			if (waypoint_prefab != null) {
				enable_all_colliders ();
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				// Casts the ray and get the first game object hit
				if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
					Debug.Log (hit.collider);
					if (Input.GetMouseButtonDown (0) && hit.collider.gameObject.tag == "ground") {
						Debug.Log ("add wp process");
						//ressourcen checken und abziehen
						//deselect_all_waypoints (); //alle deselektieren
						GameObject sgo = (GameObject)Instantiate (waypoint_prefab, hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal)); //neue instanz erstellen
						sgo.GetComponent<path_point> ().is_selected = true; //den neu erstellten selektieren
						selected_wp_id = sgo.GetComponent<path_point> ().waypoint_id;
						map_wp_to_ui (selected_wp_id); //ui auf den neuen WP mappen
						curr_wp_mode = wp_mode.adden; //wieder in adden mode gehen
						//disable_all_colliders();
						//enable_collider_on_selected(selected_wp_id); //collider 
					} 
				}//ende raycast
			}
			//}

			if (Input.GetMouseButtonDown (1)) { //abbrechen
				Debug.Log("select btn 1");
				curr_wp_mode = wp_mode.none;
			}

		}


		//WP CONNECTEN
		if (curr_wp_mode == wp_mode.connecten && selected_wp_id > 0) {
			Debug.Log ("con 1");
			//	if (GameObject.Find ("node_" + selected_wp_id.ToString ()).GetComponent<path_point>().type == path_point.node_type.normal_node) {
			Debug.Log ("con 2");
			disable_collider_on_slelected (selected_wp_id);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				Debug.Log ("con 3");
				RaycastHit coll_hit;
				if (hit.collider.gameObject.tag == "node" && Vector3.Distance (hit.point, selected_wp_pos) < 3.0f) {
					Debug.Log ("con 4");
					if (Input.GetMouseButtonDown (0)) {
						Debug.Log ("con 5");
						//ist in reichweite
						//jetzt schauen ob
						int tmp_dest_id = hit.collider.gameObject.GetComponent<path_point> ().waypoint_id;
						int tmp_origin_id = selected_wp_id;
						bool war_was = false;

						List<wp_edge> edges_to_remove = new List<wp_edge> ();
						//edges_to_remove.Clear();
						foreach (wp_edge e in edgelist) {
							if ((e.dest_id == tmp_dest_id && e.source_id == tmp_origin_id) || (e.source_id == tmp_dest_id && e.dest_id == tmp_origin_id)) {
								war_was = true;
								edges_to_remove.Add (e);
								Debug.Log (" con 6 remove edje");
							}
						}



						//neue edge adden
						if (!war_was) {
							Debug.Log ("con 7 add edge");
							Vector3 sd = GameObject.Find ("node_" + tmp_dest_id.ToString ()).transform.position;
							Vector3 dd = GameObject.Find ("node_" + tmp_origin_id.ToString ()).transform.position;

							wp_edge tmp_edge = new wp_edge (edgelist.Count, tmp_origin_id, tmp_dest_id, Vector3.Distance (sd, dd));
							edgelist.Add (tmp_edge);



							//DEBUG
							wp_edge tmp_edge_test = new wp_edge (edgelist.Count, tmp_dest_id, tmp_origin_id, Vector3.Distance (sd, dd));
							edgelist.Add (tmp_edge_test);

							refresh_edge_visuals ();
							curr_wp_mode = wp_mode.selecten;
						} else {
							//remove edge
							foreach (wp_edge etr in edges_to_remove) {
								edgelist.Remove (etr);
							}

						}
						convert_edgelist_to_dijkstra_node_list ();
					}

				}

			}
			enable_collider_on_selected (selected_wp_id);
			if (Input.GetMouseButtonDown (1)) {
				curr_wp_mode = wp_mode.none;
			}
			//	}
		}


		//VERSCHIEBEN
		if (curr_wp_mode == wp_mode.verschieben && selected_wp_id > 0) {
			Debug.Log ("move 1");
			disable_collider_on_slelected (selected_wp_id);
			enable_range_cirlce_on_slelected (selected_wp_id);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				Debug.Log ("move 2");
				RaycastHit coll_hit;
				if (hit.collider.gameObject.tag == "ground" && Vector3.Distance (hit.point, selected_wp_pos) <= 10.0f) {
					Debug.Log ("move 3");
					Vector3 origin = new Vector3 (selected_wp_pos.x, selected_wp_pos.y + selected_wp_scale.y, selected_wp_pos.z);
					Vector3 dest = new Vector3 (hit.point.x, hit.point.y + selected_wp_scale.y, hit.point.z);
					if (Physics.Raycast (origin, dest, out coll_hit, 100)) {
						Debug.Log ("move 4");

						GameObject.Find ("node_" + selected_wp_id).gameObject.transform.position = hit.point;
						GameObject.Find ("node_" + selected_wp_id).gameObject.transform.rotation = Quaternion.FromToRotation (Vector3.up, hit.normal);
						//set new pos -> kosten abziehen
						if (Input.GetMouseButtonDown (0)) {
							Debug.Log ("move 5");
							enable_collider_on_selected (selected_wp_id);
							curr_wp_mode = wp_mode.none;
						
						}//ende getmouse
					}
			
				}
			}
			if (Input.GetMouseButtonDown (1)) {
				enable_collider_on_selected (selected_wp_id);
				GameObject.Find ("node_" + selected_wp_id).gameObject.transform.position = selected_wp_pos;
				GameObject.Find ("node_" + selected_wp_id).gameObject.transform.rotation = selected_wp_rot;
				curr_wp_mode = wp_mode.none;
			}
		}



		//SELECTEN
		if (Input.GetMouseButtonDown (0) && curr_wp_mode == wp_mode.selecten) {
			Debug.Log ("select 1");
			disable_all_range_circles();
			enable_all_colliders ();
			enable_range_cirlce_on_slelected (selected_wp_id);
			GameObject rayobj = GetClickedGameObject ();
			foreach (GameObject n in GameObject.FindGameObjectsWithTag("node")) {
				Debug.Log ("select 2");
				if (rayobj == n.gameObject && rayobj != null && n.GetComponent<path_point>().type == path_point.node_type.normal_node) {
					Debug.Log ("select 3");
					int wpid = rayobj.GetComponent<path_point> ().waypoint_id;
					deselect_all_waypoints ();
					select_waypoint_with_id (wpid);
					map_wp_to_ui (wpid);
					selected_wp_id = wpid;
					curr_wp_mode = wp_mode.selecten;
					Debug.Log ("wp_selected id:" + selected_wp_id.ToString ());
				}
			}	

		}else{
			if (Input.GetMouseButtonDown (1)) {
				Debug.Log("deselect all");
				deselect_all_waypoints ();
				disable_all_range_circles ();
			}
		} 
	}










	public float get_edge_weight (int this_node, int neig)
	{
		foreach (dijkstra_node item in dijkstra_node_list) {
			if (item.node_id == this_node) {
				foreach (wp_edge ed in item.node_edges) {
					if (ed.dest_id == neig && ed.source_id == this_node) {
						return ed.weight;
					}
					Debug.Log (ed.dest_id + "  ->  " + neig + "     :    " + ed.source_id + "   ->  " + this_node + "    W:" + ed.weight);
				}
			}
		}
		return -1f;
	}
	
	//hier den node mit der geringsten distance und unbesucht
	private int  get_node_with_lowest_distance ()
	{
		int saved_node_id = -1;
		float saved_dist = Mathf.Infinity;
		foreach (dijkstra_node n in dijkstra_node_list) {
			if (n.distance < saved_dist && !n.visited) {
				saved_dist = n.distance;
				saved_node_id = n.node_id;
				Debug.Log ("the node with the lowest distance is : " + saved_node_id);
				return saved_node_id;
			}
		}
		return saved_node_id;
	}

	private void mark_node_as_visited (int id)
	{
		for (int i = 0; i < dijkstra_node_list.Count; i++) {
			if (dijkstra_node_list [i].node_id == id) {
				dijkstra_node tmp = dijkstra_node_list [i];
				tmp.visited = true;
				dijkstra_node_list [i] = tmp;
				Debug.Log ("der node " + id + " wurde auf besucht gesetzt");
			}
		}
	}

	private void mark_node_as_unvisited (int id)
	{
		for (int i = 0; i < dijkstra_node_list.Count; i++) {
			if (dijkstra_node_list [i].node_id == id) {
				dijkstra_node tmp = dijkstra_node_list [i];
				tmp.visited = false;
				dijkstra_node_list [i] = tmp;
				Debug.Log ("der node " + id + " wurde auf besucht gesetzt");
			}
		}
	}
	
	private bool check_if_all_visited()
	{
		bool node_checked = true;
		foreach (dijkstra_node n in dijkstra_node_list) {
			if (!n.visited) {
				node_checked = false;
			}
		}
		return node_checked;
	}
	
	private dijkstra_node get_node_component (int id)
	{
		dijkstra_node tmp = new dijkstra_node ();
		for (int i = 0; i < dijkstra_node_list.Count; i++) {
			if (dijkstra_node_list [i].node_id == id) {
				tmp = dijkstra_node_list [i];
				return tmp;
			}
		}
		return tmp;
	}
	
	private void set_all_to_unvisited ()
	{
		for (int i = 0; i < dijkstra_node_list.Count; i++) {
			dijkstra_node tmp = dijkstra_node_list [i];
			tmp.visited = false;
			dijkstra_node_list [i] = tmp;
		}
	}
	
	private void set_all_to_visited ()
	{
		for (int i = 0; i < dijkstra_node_list.Count; i++) {
			dijkstra_node tmp = dijkstra_node_list [i];
			tmp.visited = true;
			dijkstra_node_list [i] = tmp;
		}
	}
	

	
	private void set_distance_to_infinite ()
	{
		for (int i = 0; i < dijkstra_node_list.Count; i++) {
			dijkstra_node tmp = dijkstra_node_list [i];
			tmp.distance = Mathf.Infinity;
			dijkstra_node_list [i] = tmp;
		}
	}
	
	private void set_distance_to_zero ()
	{
		for (int i = 0; i < dijkstra_node_list.Count; i++) {
			dijkstra_node tmp = dijkstra_node_list [i];
			tmp.distance = 0.0f;
			dijkstra_node_list [i] = tmp;
		}
	}
	
	private void set_ancestor_to_null ()
	{
		for (int i = 0; i < dijkstra_node_list.Count; i++) {
			dijkstra_node tmp = dijkstra_node_list [i];
			tmp.ancestor = 0;
			dijkstra_node_list [i] = tmp;
		}
	}

	private void set_node_distance_to_zero (int id)
	{
		for (int i = 0; i < dijkstra_node_list.Count; i++) {
			if (dijkstra_node_list [i].node_id == id) {
				dijkstra_node tmp = dijkstra_node_list [i];
				tmp.distance = 0;
				dijkstra_node_list [i] = tmp;
			}
		}
	}

	private void set_node_distance_to (int id, float to)
	{
		for (int i = 0; i < dijkstra_node_list.Count; i++) {
			if (dijkstra_node_list [i].node_id == id) {
				dijkstra_node tmp = dijkstra_node_list [i];
				tmp.distance = to;
				dijkstra_node_list [i] = tmp;
			}
		}
	}

	private void set_node_ancestor_to (int id, int to)
	{
		for (int i = 0; i < dijkstra_node_list.Count; i++) {
			if (dijkstra_node_list [i].node_id == id) {
				dijkstra_node tmp = dijkstra_node_list [i];
				tmp.distance = to;
				dijkstra_node_list [i] = tmp;
			}
		}
	}
	//set_distance //set ancestor









	public void Dijkstra_Init(List<dijkstra_node> graph, int startkonten){
		Debug.Log("Dijkstra_Init");
		dijkstra_node start_node_object = new dijkstra_node();
		for (int i = 0; i < graph.Count; i++) {
			dijkstra_node node_to_edit = graph[i];
			if(node_to_edit.node_id == startkonten){ //für den startknoten setzte die distanz zu sich selber auf 0 und sich selber als vorgänger
				node_to_edit.distance = 0.0f;
				node_to_edit.ancestor = startkonten;
				start_node_object = node_to_edit;
				node_to_edit.visited = false;
			}else{
				//für alle anderen knoten
				node_to_edit.distance = Mathf.Infinity;
				node_to_edit.ancestor = 0;
			}
			node_to_edit.visited = false;
			graph[i] = node_to_edit;
		}

		Dijkstra_Compute(graph, startkonten);

		for (int i = 0; i < graph.Count; i++) {
			dijkstra_node tmp = graph[i];
			GameObject.Find("node_" + tmp.node_id.ToString()).GetComponent<path_point>().add_path_to_node(Dijkstra_Resolve_Path(graph, startkonten, tmp.node_id));
		}
	}


	private void Dijkstra_Compute(List<dijkstra_node> nodes, int startkonten){
		Debug.Log("Dijkstra_Compute");
		int current_node = startkonten;
		//für alle nodes
		//foreach (dijkstra_node alle_nodes in nodes) {
			while (!check_if_all_visited()){ // die noch nicht besucht wurden
	
				mark_node_as_visited(current_node); // und auf besucht
				//für jeden nachbarn des aktuellen nodes
				foreach (int neig in get_node_component(current_node).neighbours) {
					if(!get_node_component(neig).visited){ //nur für alle unbesuchten
						float current_node_weight = get_node_component(current_node).distance;
						float edge_node_weight = get_edge_weight(current_node, neig);
						float kanten_summe = current_node_weight + edge_node_weight; //addiere das gewicht beider kanten
						if(kanten_summe < edge_node_weight){ //wenn sie summe kleiner ist als die des nachbarn
							set_node_ancestor_to(neig,current_node); //setzte dich selber als seinen vorgänger
							set_node_distance_to(neig, kanten_summe); //und setzte die ausgerechnete distanz als dizstanz von diesem
						}//ende kantensumme
					}//ende alle neig nicht visites
				}//ende fpr each neighbour
				current_node = get_node_with_lowest_distance(); //setzte den node mit der geringsten distanz auf aktuell
			}//end if !visites

//}//end for
	}

	private List<int> Dijkstra_Resolve_Path(List<dijkstra_node> nodes, int startknoten, int zielknoten){
		Debug.Log("Dijkstra_Resolve_Path :" + startknoten.ToString() + "->" + zielknoten.ToString());
		List<int> path = new List<int>();
		int curr_id_fp = zielknoten;

		while (get_node_component(curr_id_fp).ancestor != get_node_component(curr_id_fp).node_id) {
			path.Add (curr_id_fp);
			curr_id_fp = get_node_component (curr_id_fp).ancestor;
		}
		path.Add(startknoten); //den startkonoten hinzufügen
		path.Reverse(); //den pfad einmal umdrehen da
		return path;
	}












}

