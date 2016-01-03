using UnityEngine;
using System.Collections;

public class script_cache : MonoBehaviour {


	public static base_manager base_manager_cache;
	public static UnitGroupCache unit_group_chache_cache;
	public static upgrade_manager upgrade_manager_cache;

    public static Canvas main_canvas;
	// Update is called once per frame
	void Awake () {
		script_cache.base_manager_cache = GameObject.Find(vars.base_name).GetComponent<base_manager>();
		script_cache.unit_group_chache_cache = GameObject.Find(vars.base_name).GetComponent<UnitGroupCache>();
		script_cache.upgrade_manager_cache = GameObject.Find(vars.upgrade_manager_name).GetComponent<upgrade_manager>();
        script_cache.main_canvas = GameObject.Find(vars.main_canvas_name).GetComponent<Canvas>();
	}
}
