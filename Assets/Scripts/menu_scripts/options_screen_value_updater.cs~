using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class options_screen_value_updater : MonoBehaviour {

	public GameObject master_vol_slider;
	public GameObject effect_vol_slider;
	public GameObject music_vol_slider;

	public float smav, sev, smuv;

	public void reset_values(){

		master_vol_slider.GetComponent<Slider>().value = smav;
		GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().master_volume = smav;
		effect_vol_slider.GetComponent<Slider>().value =sev;
		GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().effect_volume = sev;
		music_vol_slider.GetComponent<Slider>().value = smuv;
		GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().music_volume = smuv;

	}
	// Update is called once per frame
	void Start () {

		if(GameObject.Find(vars.sound_manager_name) != null){

			smav = GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().master_volume;
			sev = GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().effect_volume;
			smuv = GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().music_volume;


			master_vol_slider.GetComponent<Slider>().value = smav;
			effect_vol_slider.GetComponent<Slider>().value =sev;
			music_vol_slider.GetComponent<Slider>().value = smuv;
		}
	}
}
