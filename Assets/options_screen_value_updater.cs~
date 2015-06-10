using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class options_screen_value_updater : MonoBehaviour {

	public GameObject master_vol_slider;
	public GameObject effect_vol_slider;
	public GameObject music_vol_slider;


	
	// Update is called once per frame
	void Start () {
	

		if(GameObject.Find(vars.sound_manager_name) != null){
			master_vol_slider.GetComponent<Slider>().value = GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().master_volume;
			effect_vol_slider.GetComponent<Slider>().value = GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().effect_volume;
			music_vol_slider.GetComponent<Slider>().value = GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().music_volume;
		}
	}
}
