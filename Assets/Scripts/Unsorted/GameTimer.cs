using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {


	public static float curr_time;
	public bool count_dir_negative;

	public void reset_timer(){
		GameTimer.curr_time = 0.0f;

	}

	// Use this for initialization
	void Start () {
		reset_timer();
	}
	
	// Update is called once per frame
	void Update () {
		if(curr_time >= float.MaxValue && !count_dir_negative){reset_timer();}
		if(curr_time <= float.MinValue && count_dir_negative){reset_timer();}
		GameTimer.curr_time += Time.deltaTime;
	}
}
