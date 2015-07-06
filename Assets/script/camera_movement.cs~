using UnityEngine;
using System.Collections;

public class camera_movement : MonoBehaviour {


  public Transform final_camera_transform;
  public Transform camera_pos_offset_init_transform;

	public Transform camera_focus_point;

  //public const string comment = "GENERAL CAMERA SETTINGS";
  public bool enable_all_camera_interations; //set true if the game is in pause menu or sth else
  public bool enable_zoom;
  public bool enable_movement;
  public bool enable_rotation;
  public bool enable_mouse;
  public bool invert_input;
  public bool enable_dynamic_slope;
  public bool enable_touch_input;
  public bool enable_keyboard;


  private Vector3 circle_rotation_point;
  public float current_roation_angle; //hier ist der winkel der drehung angegen von 0 bis 360°
  public float camera_view_radius; //hier ist der radius der drehung angegeben je grösser desto
  public float circle_height;
  private Vector3 circle_middle_point;
  public Vector3 camera_offset;
  public float zoom_step;
  public float zoom_step_min;
  public float zoom_step_max;
  private Vector3 slope_vector;
  private bool can_zoom_in, can_zoom_out;

  private int intert_input_value;
  private Vector2 current_mouse_pos;
  //public const string comment = "PC CAMERA SETTINGS";
  public float zoom_speed_multiplier_pc_mouse;
  public float zoom_speed_multiplier_pc_keyboard;
  public float screen_boarder_to_move;
  public float camera_movement_speed_pc_keyboard;
  public float camera_movement_speed_pc_mouse;
  public float camera_rotation_speed_pc;
  //public const string comment = "MOBILE PLATTFORM SETTINGS";
  public float zoom_speed_multiplier_mobile;
  public float camera_movement_speed_mobile;
  public float rotation_speed_multiplier_mobile;
  //public const string comment = "DYNAMIC CAMERA SLOPE SETTINGS";
  public float dynamic_slope_start_zoom;
  public float dynamic_slope_end_height;
  private float dynamic_slope_interval;
  private float current_height;
  private Vector2 last_mouse_pos;
  private Vector2 delta_mouse_pos;
  //private Vector3 circle_level_vector;
  //private float slope_vector_angle;


  // Use this for initialization
  void Start()
  {
	this.name = vars.main_camera_script_holder_name;
    can_zoom_in = true;
    can_zoom_out = true;
    current_height = circle_height;
    last_mouse_pos = Input.mousePosition;
    //camera_offset = camera_pos_offset_init_transform.transform.position;
  }

  // Update is called once per frame
  void FixedUpdate()
  {

    if ((Application.isEditor || !Application.isMobilePlatform) && enable_all_camera_interations && !GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().is_in_menu)
    {

      if (enable_keyboard)
      {
        //zoom by keyboard
        if (Input.GetKey(KeyCode.Plus) && can_zoom_in) { zoom_step -= 1 * zoom_speed_multiplier_pc_keyboard * Time.deltaTime; }
        if (Input.GetKey(KeyCode.Minus) && can_zoom_out) { zoom_step += 1 * zoom_speed_multiplier_pc_keyboard * Time.deltaTime; }
        //camera_movement by keyboard
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) { move_camera(1, camera_movement_speed_pc_keyboard); } //up
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) { move_camera(2, camera_movement_speed_pc_keyboard); } //down
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) { move_camera(3, camera_movement_speed_pc_keyboard); } //left
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) { move_camera(4, camera_movement_speed_pc_keyboard); } //right
        //camera_roation by pc
        if (Input.GetKey(KeyCode.Q) && enable_rotation) { current_roation_angle += camera_rotation_speed_pc * Time.deltaTime; }
        if (Input.GetKey(KeyCode.E) && enable_rotation) { current_roation_angle -= camera_rotation_speed_pc * Time.deltaTime; }
      }

      //camera movement by mouse
      if (enable_mouse)
      {
        current_mouse_pos = Input.mousePosition;//get mouse position
        delta_mouse_pos = current_mouse_pos - last_mouse_pos;
        last_mouse_pos = current_mouse_pos;
        //zoom by mousewheel
        if (!Input.GetMouseButton(2))
        {
          //zoom_step += (Input.mouseScrollDelta.x+Input.mouseScrollDelta.y) * zoom_speed_multiplier_pc_mouse * Time.deltaTime;
          zoom_step += Input.GetAxis("Mouse ScrollWheel") * zoom_speed_multiplier_pc_mouse * Time.deltaTime;
        }
        //rotation by mousewheel
        if (Input.GetMouseButton(2) && enable_rotation)
        {
          current_roation_angle += (delta_mouse_pos.x + delta_mouse_pos.y) * camera_rotation_speed_pc * Time.deltaTime;
        }
        //movement by mouse
				if (!Input.GetMouseButton(2) && !Application.isEditor)
        {
          if (current_mouse_pos.x < screen_boarder_to_move) { move_camera(3, camera_movement_speed_pc_mouse); }//left
          if (current_mouse_pos.x > Screen.width - screen_boarder_to_move) { move_camera(4, camera_movement_speed_pc_mouse); } //right
          if (current_mouse_pos.y < screen_boarder_to_move) { move_camera(2, camera_movement_speed_pc_mouse); } //down
          if (current_mouse_pos.y > Screen.height - screen_boarder_to_move) { move_camera(1, camera_movement_speed_pc_mouse); }//up
        }
      }//ende input mouse button
    }



    //W
    //camera_offset += new Vector3(0.05f*intert_input_value*Mathf.Sin(current_roation_angle*Mathf.Deg2Rad),0,-0.05f*intert_input_value*Mathf.Cos(current_roation_angle*Mathf.Deg2Rad));


    //S
    //camera_offset += new Vector3(-0.05f*intert_input_value*Mathf.Cos(current_roation_angle*Mathf.Deg2Rad),0,-0.05f*intert_input_value*Mathf.Sin(current_roation_angle*Mathf.Deg2Rad));
    if ((Application.isMobilePlatform || Application.isEditor) && enable_touch_input && enable_all_camera_interations)
    {
      Touch[] touches = Input.touches;
      if (touches.Length > 0)
      {
        //move singel toche
        if (touches.Length == 1 && enable_movement)
        {
          if (touches[0].phase == TouchPhase.Moved)
          {
            Vector2 tmp_delta = touches[0].deltaPosition;
            float pos_y = tmp_delta.x * -intert_input_value;
            float pos_x = tmp_delta.y * -intert_input_value;

            move_camera(1, pos_x * camera_movement_speed_mobile); //for up and down direction the can be pos_x > 0  && pos_x < 0
            move_camera(3, pos_y * camera_movement_speed_mobile);
          }
        }// t == 1
        //zoom double touche
        if (touches.Length == 2 && enable_zoom)
        {
          Touch touch_finger_1 = touches[0];
          Touch touch_finger_2 = touches[1];
          Vector2 delta_touch_finger_1 = touch_finger_1.position - touch_finger_1.deltaPosition;
          Vector2 delta_touch_finger_2 = touch_finger_2.position - touch_finger_2.deltaPosition;
          float mag_prev_touch = (delta_touch_finger_1 - delta_touch_finger_2).magnitude;
          float mag_delta_touch = (touch_finger_1.position - touch_finger_2.position).magnitude;
          float delta_mag = mag_delta_touch - mag_prev_touch;

          //check if we can zoom in or out
          if (zoom_speed_multiplier_mobile * delta_mag * -intert_input_value * Time.deltaTime > 0 && can_zoom_in)
          {
            zoom_step += zoom_speed_multiplier_mobile * delta_mag * intert_input_value * Time.deltaTime;
          }
          if (zoom_speed_multiplier_mobile * delta_mag * -intert_input_value * Time.deltaTime < 0 && can_zoom_out)
          {
            zoom_step += zoom_speed_multiplier_mobile * delta_mag * intert_input_value * Time.deltaTime;
          }

        }// t == 2



        //rotation by touch
        if (touches.Length == 3 && enable_rotation)
        {
          Touch touch_finger_1 = touches[0];
          Touch touch_finger_2 = touches[1];
          Vector2 delta_touch_finger_1 = touch_finger_1.position - touch_finger_1.deltaPosition;
          Vector2 delta_touch_finger_2 = touch_finger_2.position - touch_finger_2.deltaPosition;
          float mag_prev_touch = (delta_touch_finger_1 - delta_touch_finger_2).magnitude;
          float mag_delta_touch = (touch_finger_1.position - touch_finger_2.position).magnitude;
          float delta_mag = mag_delta_touch - mag_prev_touch;

          //check if we can zoom in or out
          if (zoom_speed_multiplier_mobile * delta_mag * -intert_input_value * Time.deltaTime > 0)
          {
            current_roation_angle += rotation_speed_multiplier_mobile * delta_mag * intert_input_value * Time.deltaTime;
          }
          if (zoom_speed_multiplier_mobile * delta_mag * -intert_input_value * Time.deltaTime < 0)
          {
            current_roation_angle += rotation_speed_multiplier_mobile * delta_mag * intert_input_value * Time.deltaTime;
          }

        }// t == 3

      }//ende t >0
    }//ende isMobile



    //some clamp stuff
    if (current_roation_angle > 360.0f) { current_roation_angle -= 360.0f; }
    if (current_roation_angle < 0.0f) { current_roation_angle += 360.0f; }
    if (camera_view_radius < 0.0f) { camera_view_radius = 0.0f; }
    if (zoom_step <= zoom_step_min) { zoom_step = zoom_step_min; can_zoom_out = true; can_zoom_in = false; }
    if (zoom_step >= zoom_step_max) { zoom_step = zoom_step_max; can_zoom_out = false; can_zoom_in = true; }
    if (zoom_step > zoom_step_min && zoom_step < zoom_step_max) { can_zoom_out = true; can_zoom_in = true; }
    if (!invert_input) { intert_input_value = -1; } else { intert_input_value = 1; }
    //Mathf.Clamp(current_roation_angle,0.0f,360.0f);

    //Here is the dynamic slope code
    if (dynamic_slope_start_zoom > zoom_step && enable_dynamic_slope && dynamic_slope_start_zoom > zoom_step_min)
    { // is the dyn slope aktive and in the rage( see settings)
      dynamic_slope_interval = ((zoom_step - zoom_step_min) * 100) / (dynamic_slope_start_zoom - zoom_step_min); //so first we calculate the current percentage of the zoom_state so with this value 0-100 we can calulate the current height
      current_height = (circle_height) / 100 * dynamic_slope_interval; //now calculate the current cameraheight.. form the percentage zoom process
      if (current_height < dynamic_slope_end_height) { current_height = dynamic_slope_end_height; } //if the maximum cameraheight reaced we cant set the heigt further
    }
    else
    {
      current_height = circle_height;
    }


    //calculate the point on the circle track at the specific angle
    circle_rotation_point = new Vector3((Mathf.Cos(Mathf.Deg2Rad * current_roation_angle) * camera_view_radius) + camera_offset.x, current_height + circle_middle_point.y, (Mathf.Sin(Mathf.Deg2Rad * current_roation_angle) * camera_view_radius) + camera_offset.z);
    //calculate the center of the circle with the 3 point method // you can also use 2 vectors in a 90° angle and calculate the intersection
    Vector2 circle_point_a = new Vector3(Mathf.Cos(Mathf.Deg2Rad * 0.0f) * camera_view_radius, Mathf.Sin(Mathf.Deg2Rad * 0.0f) * camera_view_radius);
    Vector2 circle_point_b = new Vector3(Mathf.Cos(Mathf.Deg2Rad * 45.0f) * camera_view_radius, Mathf.Sin(Mathf.Deg2Rad * 45.0f) * camera_view_radius);
    Vector2 circle_point_c = new Vector3(Mathf.Cos(Mathf.Deg2Rad * 90.0f) * camera_view_radius, Mathf.Sin(Mathf.Deg2Rad * 90.0f) * camera_view_radius);
    //so now we habe the 3 points on the circle now we can calculate the x/y  coords
    float A11 = circle_point_a.x - circle_point_b.x;
    float A12 = circle_point_a.y - circle_point_b.y;
    float A21 = circle_point_a.x - circle_point_c.x;
    float A22 = circle_point_a.y - circle_point_c.y;
    float B1 = A11 * (circle_point_a.x + circle_point_b.x) + A12 * (circle_point_a.y + circle_point_b.y);
    float B2 = A21 * (circle_point_a.x + circle_point_c.x) + A22 * (circle_point_a.y + circle_point_c.y);
    float D = 2 * (A11 * A22 - A21 * A12);
    float Dx = B1 * A22 - B2 * A12;
    float Dy = A11 * B2 - A21 * B1;
    //float x=Dx/D, y=Dy/D; //here are the final x/y coordinates in a further step we add the height information (Y Axis) to create the final position Vector
    circle_middle_point = new Vector3(Dx / D, 0.0f, Dy / D) + camera_offset;
    //now we calculate the slope form the center of the circle to the point on the circle(at the specific angle)
    slope_vector = circle_rotation_point - circle_middle_point;
    //so now we add a zoom wuhuuuu we move the object(camera) along the slope vector
    final_camera_transform.position = new Vector3(slope_vector.x * zoom_step, slope_vector.y * zoom_step, slope_vector.z * zoom_step) + camera_offset;
    //now we have the slopevector so we can calculate the angle of the slope relativ to the circle we need this to rotate the object(camera) into the right direction
    //slope_vector
		camera_focus_point.transform.position = circle_middle_point;
    //circle_level_vector
    //float dot_product = Vector3.Dot(circle_middle_point,slope_vector); //this is the dot Product funktion its easier to use this
    //dot_product = dot_product/ (circle_middle_point.magnitude * slope_vector.magnitude);
    //slope_vector_angle = 180-(Mathf.Acos(dot_product)*Mathf.Rad2Deg);
    //final_camera_transform.rotation = Quaternion.AngleAxis(slope_vector_angle, Vector3.right);
    final_camera_transform.LookAt(circle_middle_point);//its a simplet funktion to rotate an objet to an spcific vector... this is our circle_middlepoint
    //some debug stuff
    //Debug.DrawLine (circle_middle_point, circle_rotation_point, Color.cyan);

    //this.transform.position = final_camera_transform.position;
    //this.transform.rotation = final_camera_transform.rotation;

  }





  public void move_camera(int dir, float _move_speed)
  {

    switch (dir)
    {
      case 1: //W
        camera_offset += new Vector3(0.05f * intert_input_value * Mathf.Cos(current_roation_angle * Mathf.Deg2Rad) * _move_speed * Time.deltaTime, 0, 0.05f * intert_input_value * Mathf.Sin(current_roation_angle * Mathf.Deg2Rad) * _move_speed * Time.deltaTime);
        break;
      case 2: //S
        camera_offset += new Vector3(-0.05f * intert_input_value * Mathf.Cos(current_roation_angle * Mathf.Deg2Rad) * _move_speed * Time.deltaTime, 0, -0.05f * intert_input_value * Mathf.Sin(current_roation_angle * Mathf.Deg2Rad) * _move_speed * Time.deltaTime);
        break;
      case 3: //A
        camera_offset += new Vector3(-0.05f * intert_input_value * Mathf.Sin(current_roation_angle * Mathf.Deg2Rad) * _move_speed * Time.deltaTime, 0, 0.05f * intert_input_value * Mathf.Cos(current_roation_angle * Mathf.Deg2Rad) * _move_speed * Time.deltaTime);
        break;
      case 4: //D
        camera_offset += new Vector3(0.05f * intert_input_value * Mathf.Sin(current_roation_angle * Mathf.Deg2Rad) * _move_speed * Time.deltaTime, 0, -0.05f * intert_input_value * Mathf.Cos(current_roation_angle * Mathf.Deg2Rad) * _move_speed * Time.deltaTime);
        break;
      default:
        break;
    }//ende scwitch
  }//ende move camera
}
