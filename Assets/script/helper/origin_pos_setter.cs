using UnityEngine;
using System.Collections;

public class origin_pos_setter : MonoBehaviour {
	public bool use_ob;
	public float sy;
	// Use this for initialization
	void Awake () {
		float sx = 0.0f,sz = 0.0f;

		sy = this.transform.position.y;
		if(use_ob){

			sx = GameObject.Find("map_center_point").transform.position.x;
			sz = GameObject.Find("map_center_point").transform.position.z;
			


		this.transform.position = new Vector3(sx,sy, sz);


	}

	
	}
}
