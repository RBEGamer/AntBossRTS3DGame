using UnityEngine;
using System.Collections;

public class wp_visible_way : MonoBehaviour {




	public Vector3 start_pos;
	public Vector3 end_pos;
	public bool visible;
	private LineRenderer ln;
	public Vector3 offset_start;
	public Vector3 offen_end;
	public Material ln_mat;


	// Use this for initialization
	void Start () {
		if(this.gameObject.GetComponent<LineRenderer>() == null){		this.gameObject.AddComponent<LineRenderer>();     this.gameObject.GetComponent<LineRenderer>().enabled = false;}ln = this.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	


		float dist = Mathf.Abs (Vector3.Distance (start_pos, end_pos));
		if (visible && dist > vars.minimum_way_point_distance) {
			ln.enabled = true;



			if (dist < 1) {
				dist = 1.0f;
			}

			ln_mat.SetTextureScale ("_MainTex", new Vector2 (dist, 1.0f));

			//this.ga
			//ln.material = ln_mat;
			ln.SetPosition (0, start_pos+offset_start);
			ln.SetPosition (1, end_pos+offen_end);


		
		} else {
			ln.enabled = false;
		}



	}
}
