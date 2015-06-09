using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class sound_manager : MonoBehaviour {



public	GameObject audio_player_template;


	AudioClip bgmusic;
	// Use this for initialization
	void Awake () {
		this.name = vars.sound_manager_name;
		DontDestroyOnLoad(this);
		create_audio_source(vars.audio_name.bgmusic);
	}



	public void create_audio_source(vars.audio_name audio_file){
		GameObject tmp;
		tmp = audio_player_template;
		audio_player_template.GetComponent<audio_clip_state>().start(audio_file);

		Instantiate(tmp,new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
	}



	void FixedUpdate(){
		foreach (GameObject n in GameObject.FindGameObjectsWithTag(vars.audio_clip_tag)) {
			if(n.GetComponent<audio_clip_state>() != null){
			}else{
				Destroy(n.gameObject);
			}

		}
	}

}
