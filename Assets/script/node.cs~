using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class node : MonoBehaviour {

	public Vector3 node_pos;
	public int node_id;
	public List<int> neighbours;
	public List<wp_edges> node_edges;
	public bool visited;
	public float distance;
	public int ancestor;
	public bool tagged;

	public bool discoveres_by_scout = false;
	public bool is_base_node;

	public void calc_neighbour_distance(){
		
		node_edges.Clear ();
		foreach (int n in neighbours) {
			Vector3 neighbour_pos =  GameObject.Find(vars.wp_node_name + "_" + n).GetComponent<node>().node_pos;
			float dist = Vector3.Distance(node_pos, neighbour_pos);
			//Debug.Log("from node " + node_id +  " to " + n + " : Distance = " + dist);
			int edge_id = node_edges.Count;
			wp_edges wp_tmp = new wp_edges(edge_id  ,node_id, n, dist);
			node_edges.Add(wp_tmp);
			//node_edges[edge_id].debug_edge_info();
		}
		
		
	}
	
	
	
	//get edge to neigbour
	
	
	public float get_edge_weight(int neig){
		
	//	bool tmp = false;
		
		foreach (wp_edges ed in node_edges) {
			if(ed.dest_id == neig && ed.source_id == this.node_id){
			//	tmp = true;
				return ed.weight;
			}
		//	Debug.Log(ed.dest_id + "  ->  "+neig + "     :    " + ed.source_id + "   ->  " + this.node_id +"    W:"+ed.weight);
		}

		return -1f;
		
	}


	public GameObject wp_node_tower_mesh;
	public GameObject wp_node_base_mesh;




	public GameObject inner_circle_object;
	public GameObject outher_cirlce_object;
	 cirlce c;
	public Vector3 v;
	public GameObject cursor;
  	public Vector3 mesh_line_offset = new Vector3(0f, 1f, 0f);
	public bool is_selected;
	public int prev_node;
	public bool is_last_node;
	public bool is_first_node;
	public bool can_be_selected; //
	public bool is_mouse_in_node_range;
	public Vector3 curr_pos_in_circle;
  	public GameObject mesh_line;
	public GameObject click_collider;
	public bool call_const = false;


	public void node_const(Vector3 _node_pos, int _node_id, int _prev_node, bool fic = true){
	
		//is_first_inferface_connected = fic;
		prev_node = _prev_node;
		node_id = _node_id;
		call_const = true;
		node_pos = _node_pos;
		this.transform.position = node_pos;
		this.name = vars.wp_node_name + "_" + _node_id;
		discoveres_by_scout = false;
	}





	void Awake(){
		distance = Mathf.Infinity;
	}


	// Use this for initialization
	void Start () {
		if(call_const){
			c = new cirlce (inner_circle_object.GetComponent<LineRenderer>(), outher_cirlce_object.GetComponent<LineRenderer>(), this.transform, cursor);
		}

	
	

	}



	public bool check_if_pos_in_cirlce_at_point(Vector3 _point){
		return c.draw_point_on_circle (_point);
	
	}


	// Update is called once per frame
	void FixedUpdate () {

			c.cirlce_offset = v;


		if(!is_base_node){


			if(discoveres_by_scout){
				wp_node_tower_mesh.gameObject.SetActive(true);
				wp_node_base_mesh.gameObject.SetActive(true);
			}else{
				wp_node_tower_mesh.gameObject.SetActive(false);
				wp_node_base_mesh.gameObject.SetActive(true);
			}

		}else{
			wp_node_tower_mesh.gameObject.SetActive(false);
			wp_node_base_mesh.gameObject.SetActive(true);
		}


      if (node_id > 0)
      {
		Vector3 _pre_node_pos = GameObject.Find(vars.path_manager_name).GetComponent<pathmanager>().get_node_pos(prev_node);
        mesh_line.gameObject.GetComponent<LineRenderer>().SetPosition(0, _pre_node_pos + mesh_line_offset);
        mesh_line.gameObject.GetComponent<LineRenderer>().SetPosition(1, node_pos + mesh_line_offset);
        mesh_line.gameObject.GetComponent<LineRenderer>().SetWidth(vars.waypoint_node_connection_line_width, vars.waypoint_node_connection_line_width);
        mesh_line.gameObject.GetComponent<LineRenderer>().material =  new Material(Shader.Find("Particles/Additive"));
        //mesh_line.gameObject.GetComponent<LineRenderer>().material = new Material(Shader.Find("Standart"));
        mesh_line.gameObject.GetComponent<LineRenderer>().SetColors(Color.blue, Color.blue);
      }
			// wenn in is_in_patheditmode dann schaue ob bereits einer selektier wurde -> wenn ja wir der selektierte deselektiert
//		c.draw_point_on_circle (GameObject.Find("cursor").transform.position);

			if (is_selected) {

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					is_mouse_in_node_range = c.draw_point_on_circle (hit.point);
					curr_pos_in_circle = hit.point;
				}
				c.draw_circle_inline ();
				c.draw_circle_outline ();		
			} else {
				c.disbale_circle ();
			}




	}










}
