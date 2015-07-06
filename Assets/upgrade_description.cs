using UnityEngine;
using System.Collections;

public class upgrade_description : MonoBehaviour {


	public bool active;
	public string upgrade_desc = "";
	public string upgrade_headline = "";
	public Sprite upgrade_icon;
	public vars.upgrade_type upgrade_type;
	public vars.upgrade_values upgrade_add_to_value; // +=
	public int costs_res_a = 0;
	public int costs_res_b = 0;
	public int costs_res_c = 0;
	public bool taken = false;


	// Use this for initialization
	void Start () {
		taken = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
