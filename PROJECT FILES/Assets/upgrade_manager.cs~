using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class upgrade_manager : MonoBehaviour {


	public List<upgrade_description> upgrade_list = new List<upgrade_description>();
	// Use this for initialization
	void Start () {
		this.name = vars.upgrade_manager_name;
		upgrade_list.Clear();
		foreach (GameObject item in GameObject.FindGameObjectsWithTag(vars.upgrade_tag_name)) {
			upgrade_list.Add(item.GetComponent<upgrade_description>());
		}

		Debug.Log(upgrade_list.Count.ToString() + " UPGRADES LOADED");
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}
