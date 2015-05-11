using UnityEngine;
using System.Collections;

public class wp_visible_way : MonoBehaviour {




	public Vector3 start_pos;
	public Vector3 end_pos;
	public bool visible;
	//private LineRenderer ln;
	public Vector3 offset_start;
	public Vector3 offset_end;
	private Material ln_mat;
	public Texture ln_text;

	private Vector3 half_start;
	private Vector3 half_end;
	
	
	public Vector3 normal;

	// Use this for initialization
	void Start () {
	//	if(this.gameObject.GetComponent<LineRenderer>() == null){		this.gameObject.AddComponent<LineRenderer>();     this.gameObject.GetComponent<LineRenderer>().enabled = false;}ln = this.GetComponent<LineRenderer>();

		ln_mat = new Material (Shader.Find("Diffuse"));
		ln_mat.shader = Shader.Find ("Diffuse");
		ln_mat.SetTexture ("_MainTex", ln_text);


		//this.gameObject.AddComponent (ln_mat);
		//this.gameObject.GetComponent<Material> (). = ln_mat;
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		
		float dist = Mathf.Abs (Vector3.Distance (start_pos, end_pos));
		if (visible ) { // dist > vars.minimum_way_point_distance
		//	ln.enabled = true;



			if (dist < 1) {
				dist = 1.0f;
			}
			ln_mat.SetTextureScale ("_MainTex", new Vector2 ( 1.0f,dist));



			create_plane();
		//	ln.materials[0] = ln_mat;
			this.GetComponent<Renderer> ().enabled = true;
		this.GetComponent<Renderer> ().material = ln_mat;

			//this.ga
			//ln.material = ln_mat;
	
	
			

			//this.GetComponent<MeshFilter> ().mesh = plane;
			

		
		} else {
		//	ln.enabled = false;
			this.GetComponent<Renderer> ().enabled = false;
		}



	}











	private void create_plane(){


		MeshFilter mf = this.GetComponent<MeshFilter> ();
		Mesh plane = new Mesh ();
		mf.mesh = plane;
		
		float w = 1.0f;


//HIER _START_POS ANLEGEN


		Vector3 _start_pos = end_pos;
		Vector3 _end_pos = start_pos;

		Vector3 _offset_start = new Vector3 (offset_start.x, offset_start.y, offset_start.z+ w/2);
		Vector3 _offset_end = new Vector3 (offset_end.x, offset_end.y, offset_end.z+ w/2);
		

	

		if (_start_pos.z >= _end_pos.z) {
			half_start = -_start_pos / 2;
			half_end = -_end_pos / 2;		
			
		} else {
			half_start = -_end_pos / 2;
			half_end = -_start_pos / 2;
		}



		Vector3 v0 = half_start - new Vector3 (w/2, 0f, 0f) +_offset_start;
		Vector3 v1 = half_start + new Vector3 (w/2, 0, 0f) +_offset_start;
		Vector3 v2 = half_end - new Vector3 (w/2, 0f, 0f) +_offset_end;
		Vector3 v3 = half_end + new Vector3 (w / 2, 0f, 0f) +_offset_end;
		


		Vector3[] vertices = new Vector3[4]{
			v0,v1,v2,v3
		};
		
		int[] triangles = new int[6];
		triangles [3] = 0;
		triangles [4] = 2;
		triangles [5] = 1;
		
		triangles [0] = 2;
		triangles [1] = 3;
		triangles [2] = 1;
		
		
		Vector3[] normals = new Vector3[4];
		normals [0] = normal;
		normals [1] = normal;
		normals [2] =normal;
		normals [3] = normal;
		
		
		Vector2[] uvs = new Vector2[4];
		uvs [0] = new Vector2 (0, 0);
		uvs [1] = new Vector2 (1, 0);
		uvs [2] = new Vector2 (0, 1);
		uvs [3] = new Vector2 (1 , 1);
		
		
		plane.vertices = vertices;
		plane.triangles = triangles;
		plane.normals = normals;
		plane.uv = uvs;
		plane.name = "tmp_plane";

	}
}
