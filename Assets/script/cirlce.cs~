using UnityEngine;
using System.Collections;

public class cirlce : MonoBehaviour {





	public float maximal_radius = vars.way_point_interaction_circle_radius;
	public float minimal_radius = vars.minimum_way_point_distance;
	public float theta_scale = 0.1f; 
	public bool show_circle;
	public float line_width = 0.03f;
	public Vector3 cirlce_offset;
	public Transform obj_around;
	private LineRenderer lineRenderer1;
	private LineRenderer lineRenderer2;
	public Color ca = Color.red , cb = Color.red;
	public float calculated_r;
	GameObject cursor_pos;

	public cirlce(LineRenderer ln1, LineRenderer ln2, Transform tr, GameObject _cursor_pos){
		lineRenderer1 = ln1;
		lineRenderer2 = ln2;
		obj_around = tr;
		cursor_pos = _cursor_pos;
	}

	


	public bool draw_point_on_circle(Vector3 ppos){
		bool is_inside_circle;
		bool is_oudside_wp;
		Vector3 op = obj_around.transform.position;
		Vector2 yl_op = new Vector2 (op.x + cirlce_offset.x, op.z + cirlce_offset.z); //frome 3d vector to 2D for the set po
		Vector2 yl_ppos = new Vector3 (ppos.x, ppos.z);
		float dist_pp_ppos = Vector2.Distance (yl_op, yl_ppos);




		//Debug.Log (dist_pp_ppos);
		if (dist_pp_ppos >= maximal_radius) {
			//punkt liegt ausserhalb
			is_inside_circle = false;
		} else {
		//punkt liegt innerhalb des kreises
			is_inside_circle = true;
		}

		if(dist_pp_ppos <= minimal_radius){
			is_oudside_wp = false;
		}else{

			is_oudside_wp = true;
		}

		Vector3 tmp_op = new Vector3 (op.x, op.y, op.z);
		Vector3 tmp_ppos = new Vector3 (ppos.x, op.y, ppos.z); // here is the y pos the y pos of the waypoint !!!

		Vector3 p;

		//if (dist_pp_ppos < 1f) {
			//tmp_ppos = Vector3.Lerp (tmp_op, tmp_ppos, radius / (tmp_op - tmp_ppos).magnitude);
			//p = Vector3.Lerp (tmp_op, tmp_ppos, 100 / (tmp_op - tmp_ppos).magnitude);
		//} else {
		p = Vector3.Lerp (tmp_op, tmp_ppos, maximal_radius / (tmp_op - tmp_ppos).magnitude);
		//}
		 
	
		Debug.DrawLine (tmp_ppos, op);


		if (is_inside_circle && is_oudside_wp) {


				cursor_pos.gameObject.SetActive(true);
				cursor_pos.transform.position = p;



			return true;

		} else {

			//cursor_pos.transform.position = this.transform.position;
				cursor_pos.gameObject.SetActive(true);

			return false;
		}
	

	

	}



	public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
	{
		Vector3 P = x * Vector3.Normalize(B - A) + A;
		return P;
	}

	public void draw_circle_outline(){
		if(!lineRenderer1.enabled){lineRenderer1.enabled = true;}

		float size = ((2.0f * Mathf.PI) / theta_scale) +1;
		lineRenderer1.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer1.SetColors(ca, cb);
		lineRenderer1.SetWidth(line_width, line_width);
		lineRenderer1.SetVertexCount((int)size);		
		int i = 0;
		for(float theta = 0f; theta < 2f * Mathf.PI; theta += 0.1f) {
			float x = maximal_radius*Mathf.Cos(theta);
			float y = maximal_radius*Mathf.Sin(theta);	
			Vector3 pos = new Vector3(x,0.0f, y);

			calculated_r = Vector3.Distance(obj_around.transform.position+cirlce_offset,pos+ obj_around.transform.position+cirlce_offset);
			lineRenderer1.SetPosition(i, pos+ obj_around.transform.position+cirlce_offset);
			i+=1;
		}
	}



	public void disbale_circle(){
		if(lineRenderer1.enabled){lineRenderer1.enabled = false;}
		if(lineRenderer2.enabled){lineRenderer2.enabled = false;}
	
	}


	public void draw_circle_inline(){
		if(!lineRenderer2.enabled){lineRenderer2.enabled = true;}

		float size = ((2.0f * Mathf.PI) / theta_scale) +1;
		lineRenderer2.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer2.SetColors(ca, cb);
		lineRenderer2.SetWidth(line_width, line_width);
		lineRenderer2.SetVertexCount((int)size);		
		int i = 0;
		for(float theta = 0f; theta < 2f * Mathf.PI; theta += 0.1f) {
			float x = minimal_radius*Mathf.Cos(theta);
			float y = minimal_radius*Mathf.Sin(theta);	
			Vector3 pos = new Vector3(x,0.0f, y);
			
			calculated_r = Vector3.Distance(obj_around.transform.position+cirlce_offset,pos+ obj_around.transform.position+cirlce_offset);
			lineRenderer2.SetPosition(i, pos+ obj_around.transform.position+cirlce_offset);
			i+=1;
		}
	}


}
