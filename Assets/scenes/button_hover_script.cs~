using UnityEngine;
using System.Collections;

public class button_hover_script : MonoBehaviour {


	public Vector3 non_scale = new Vector3(1.0f,1.0f,1.0f);
	public Vector3 hover_scale = new Vector3(1.2f,1.2f,1.2f);
	public float speed = 2f;
	public bool hover_state = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	

		if(hover_state){
			this.transform.localScale =	Vector3.Lerp(non_scale, hover_scale, Time.deltaTime*speed);
		}else{
			this.transform.localScale =	Vector3.Lerp(hover_scale, non_scale, Time.deltaTime*speed);
		}



	}

	public void leavehover(){
	
		hover_state = true;
	}

	public void enter_hover(){
		hover_state = false;
	}
}
