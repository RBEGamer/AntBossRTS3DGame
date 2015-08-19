using UnityEngine;
using System.Collections;

public class upgrade_description : MonoBehaviour {

	public enum Tier
	{
		tier_1, tier_2, tier_3, tier_4
	}

	public bool active;
	public string upgrade_desc = "";
	public string upgrade_headline = "";
	public Sprite upgrade_icon;
	public vars.upgrade_type upgrade_type;
	public vars.upgrade_values upgrade_add_to_value; // +=
	public int costs_res_a_buy = 0;
	public int costs_res_b_buy = 0;
	public int costs_res_c_buy = 0;

	public int costs_res_a_research = 0;
	public int costs_res_b_research = 0;
	public int costs_res_c_research = 0;


	public bool erfroscht = false;
	public float increase_value = 0.0f;

	// Use this for initialization
	void Start () {
		taken = false;
		this.name = "upgrade_desc_" + upgrade_type.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
