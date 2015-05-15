using UnityEngine;
using System.Collections;

public class sleep_pos_manager : MonoBehaviour {


	public Vector3 pos;
	// Use this for initialization
	void Start () {
		this.name = vars.sleep_pos_manager_name;
		pos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
