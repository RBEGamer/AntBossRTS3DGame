﻿using UnityEngine;
using System.Collections;

public class selection_circle : MonoBehaviour {

	public enum circle_type
	{
		wp_inner, wp_outher, res_outher
	}

	LineRenderer ln;

	public circle_type ctype;

	private float radius;
	private float line_width;
	public Color ca = Color.red , cb = Color.red;
	public Vector3 cirlce_offset;
	public float theta_scale = 0.1f; 
	// Use this for initialization
	void Start () {
		//check if linerenderer exists
		if(this.gameObject.GetComponent<LineRenderer>() == null){
			Debug.Log("lol");
			this.gameObject.AddComponent<LineRenderer>();
		}
		ln = this.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	



		get_circle_information();


		float size = ((2.0f * Mathf.PI) / theta_scale) +1;
		ln.material = new Material(Shader.Find("Particles/Additive"));
		ln.SetColors(ca, cb);
		ln.SetWidth(line_width, line_width);
		ln.SetVertexCount((int)size);		
		int i = 0;
		for(float theta = 0f; theta < 2f * Mathf.PI; theta += 0.1f) {
			float x = radius*Mathf.Cos(theta);
			float y = radius*Mathf.Sin(theta);	
			Vector3 pos = new Vector3(x,0.0f, y);
			
			//calculated_r = Vector3.Distance(obj_around.transform.position+cirlce_offset,pos+ obj_around.transform.position+cirlce_offset);
			ln.SetPosition(i, pos+cirlce_offset);
			i+=1;
		}


	}



	//is pos in circle

	//




	private void get_circle_information(){
		switch (ctype) {
		case circle_type.wp_inner:
			radius = vars.minimum_way_point_distance;
			line_width = vars.waypoint_node_connection_line_width;
			break;
		case circle_type.wp_outher:
			radius = vars.way_point_interaction_circle_radius;
			line_width = vars.waypoint_node_connection_line_width;
			break;
		case circle_type.res_outher:
			radius = vars.res_interaction_radius;
			line_width = vars.res_interaction_circle_width;
			break;
		default:
			radius = 0.0f;
			line_width = 0.0f;
			break;
		}
	}
}
