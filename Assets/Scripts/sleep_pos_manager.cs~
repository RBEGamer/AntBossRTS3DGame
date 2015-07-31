using UnityEngine;
using System.Collections;

public class sleep_pos_manager : MonoBehaviour {


	private Vector3 pos;
	public Vector3 pos_offset;
	public bool random_sleep_pos;
	// Use this for initialization
	void Start () {
		this.name = vars.sleep_pos_manager_name;
		pos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public Vector3 get_sleeping_pos(){
		if(random_sleep_pos){
			return pos+pos_offset+ new Vector3(Random.Range(0f,3f),0.0f, Random.Range(0f,3f));
		}else{
			return pos+pos_offset;
		}


	}
}
