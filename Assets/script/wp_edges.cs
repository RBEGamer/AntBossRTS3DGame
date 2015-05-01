﻿using UnityEngine;
using System.Collections;

public class wp_edges : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public int source_id;
	public int dest_id;
	public float weight;
	public float edge_id;


	public wp_edges(int _edge_id, int _source_id, int _dest_id, float _weight){
		source_id = _source_id;
		dest_id = _dest_id;
		weight = _weight;
		edge_id = _edge_id;
		//debug_edge_info();
	}


	public void debug_edge_info(){

		Debug.Log ("EDGE FROM :" + source_id + " TO " + dest_id + " WITH THE WEIGHT : "+ weight);
	}
}
