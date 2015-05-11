using UnityEngine;
using System.Collections;

public class plabe_creator : MonoBehaviour {
	public Vector3 start_pos;
	public Vector3 end_pos;

	private Vector3 half_start;
	private Vector3 half_end;


	public Vector3 normal;
	// Use this for initialization


	public bool swtich_points;
	void Update () {
		
		MeshFilter mf = this.GetComponent<MeshFilter> ();
		Mesh plane = new Mesh ();
		mf.mesh = plane;
		
		float w = 1.0f;


		if (start_pos.z >= end_pos.z) {
			half_start = -start_pos / 2;
			half_end = -end_pos / 2;		
		
		} else {
			half_start = -end_pos / 2;
			half_end = -start_pos / 2;
		}

		Vector3 v0 = half_start - new Vector3 (w / 2, 0f, 0f) ;
		Vector3 v1 = half_start + new Vector3 (w / 2, 0, 0f) ;
		Vector3 v2 = half_end - new Vector3 (w / 2, 0f, 0f) ;
		Vector3 v3 = half_end + new Vector3 (w / 2, 0f, 0f) ;
		
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
