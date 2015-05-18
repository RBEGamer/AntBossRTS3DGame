using UnityEngine;
using System.Collections;

public class selection_circle : MonoBehaviour {

	private Material ln_mat;
	private Texture ln_text;


	public enum circle_type
	{
		wp_inner, wp_outher, res_outher
	}

	LineRenderer ln;

	public circle_type ctype;

	private float radius;
	private float line_width;
	private Color ca = Color.red , cb = Color.red;
	public Vector3 cirlce_offset;
  public Vector3 additional_circle_offset;
	public float theta_scale = 0.1f;
  public Vector3 circle_middle;
  public bool circle_enabled;
//  public bool 
	// Use this for initialization
	void Start () {
		//check if linerenderer exists
			if(this.gameObject.GetComponent<LineRenderer>() == null){		this.gameObject.AddComponent<LineRenderer>();     this.gameObject.GetComponent<LineRenderer>().enabled = false;}ln = this.GetComponent<LineRenderer>();
	
		//create Material
		ln_mat = new Material(Shader.Find("Diffuse"));
		ln_mat.shader = Shader.Find("Diffuse");
	//	ln_mat.mainTexture = ln_text;
		ln_mat.color = Color.red;
		this.GetComponent<Renderer>().material = ln_mat;
		//disable shadows for the linerenderer
		ln.receiveShadows = false;
		ln.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
	//create TExture
	//	ln_text = new Texture();
	//	ln_text.
	}



/*
  public bool is_point_in_circle(Vector2 _p2d)
  {
  
    float d = Vector2.Distance(_p2d, circle_middle);
    if (d < radius){
      return true;
    }else{
      return false;
    }
  }
  */
  public bool is_point_in_circle(Vector3 _p3d)
  {
 
    Vector2 _p = new Vector2(_p3d.x, _p3d.z);
    Vector2 cm = new Vector2(circle_middle.x, circle_middle.z);
    float d = Vector2.Distance(_p, cm);
  //  Debug.Log("_p : " + _p + " cm : " + cm + " D:" + d);
   
    if (d < radius)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

	// Update is called once per frame
	void FixedUpdate () {

		circle_middle = cirlce_offset + additional_circle_offset;

    /*
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			Debug.Log (is_point_in_circle (hit.point));
		}
   */

		

		if (circle_enabled) {
  
			get_circle_information ();

     //  circle_middle = cirlce_offset;
			if (this.GetComponent<LineRenderer>().enabled == false) { this.GetComponent<LineRenderer>().enabled = true; }

      float size = ((2.0f * Mathf.PI) / theta_scale) + 1;
     // ln.material = new Material(Shader.Find("Particles/Additive"));
      ln.SetColors(ca, cb);
      ln.SetWidth(line_width, line_width);
      ln.SetVertexCount((int)size);
      int i = 0;
      for (float theta = 0f; theta < 2f * Mathf.PI; theta += 0.1f)
      {
        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);
        Vector3 pos = new Vector3(x, 0.0f, y);
        //calculated_r = Vector3.Distance(obj_around.transform.position+cirlce_offset,pos+ obj_around.transform.position+cirlce_offset);
        ln.SetPosition(i, pos + cirlce_offset + additional_circle_offset);
        i += 1;
      }
    }
    else
    {
      if (this.GetComponent<LineRenderer>().enabled == true) { this.GetComponent<LineRenderer>().enabled = false; }
    }
 
	





	}
	//is pos in circle

	//




	private void get_circle_information(){
		switch (ctype) {
		case circle_type.wp_inner:
			radius = vars.minimum_way_point_distance;
			line_width = vars.waypoint_node_connection_line_width;
			ln_mat.color = vars.waypoint_circle_color;
			break;
		case circle_type.wp_outher:
			radius = vars.way_point_interaction_circle_radius;
			line_width = vars.waypoint_node_connection_line_width;
			ln_mat.color = vars.waypoint_circle_color;
			break;
		case circle_type.res_outher:
			radius = vars.res_interaction_radius;
			line_width = vars.res_interaction_circle_width;
			ln_mat.color = vars.ressource_circle_color;
			break;
		default:
			radius = 0.0f;
			line_width = 0.0f;
			break;
		}
	}
}
