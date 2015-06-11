using UnityEngine;
using System.Collections;

public class audio_clip_state : MonoBehaviour {

	public bool is_playing = false;
	public bool is_looping = false;
	private float saved_vol  =1.0f;

	public vars.audio_playback_type type;

	private AudioSource asc;
	private AudioClip ac;

	// Use this for initialization
	void Awake () {

	//	type = vars.audio_playback_type.none;
		DontDestroyOnLoad(this);

		if(this.gameObject.GetComponent<AudioSource>() == null){
			this.gameObject.AddComponent<AudioSource>();
		}
		asc = this.gameObject.GetComponent<AudioSource>();

	}


	public void start(vars.audio_name audio_file){
	
		asc = this.gameObject.GetComponent<AudioSource>();
		switch (audio_file) {

		case vars.audio_name.bgmusic:
			 ac = (AudioClip)Resources.Load(vars.audio_clip_info_bgmusic.audio_clip_path);
			type = vars.audio_clip_info_bgmusic.ptype;
			saved_vol = vars.audio_clip_info_bgmusic.volume;
			asc.priority = vars.audio_clip_info_bgmusic.priority;
			asc.pitch = vars.audio_clip_info_bgmusic.pitch;

			break;

		case vars.audio_name.ui_click:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_uiclick.audio_clip_path);
			type = vars.audio_clip_info_uiclick.ptype;
			saved_vol = vars.audio_clip_info_uiclick.volume ;
			asc.priority = vars.audio_clip_info_uiclick.priority;
			asc.pitch = vars.audio_clip_info_uiclick.pitch;
			break;
		default:
			ac = null;
			type = vars.audio_playback_type.none;
			asc.priority = 0;
			saved_vol = 0.0f;
			asc.pitch = 1.0f;
			break;
		}

		asc.volume = saved_vol;
		manage_vol();
		this.name = "audio_playback_" + ac.name;

		asc.clip = ac;

		if(type == vars.audio_playback_type.music){
			asc.loop = true;
		}else{
			asc.loop = false;
		}
	
		asc.enabled = true;
		asc.Play();

	}



	public void manage_vol(){

		float master_vol = 1.0f;
		switch (type) {
		case vars.audio_playback_type.effect:
			master_vol = GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().effect_volume;
			break;
		case vars.audio_playback_type.music:
			master_vol = GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().music_volume;
			break;
			
		default:
			master_vol = 0.0f;
			break;
		}

		//Debug.Log(saved_vol);
		asc.volume = saved_vol * master_vol * GameObject.Find(vars.sound_manager_name).GetComponent<sound_manager>().master_volume;





	}
	// Update is called once per frame
	void Update () {
		if(this.GetComponent<AudioSource>() != null){
		//	Debug.Log(true);
		
		is_playing = asc.isPlaying;
		is_looping = asc.loop;

		manage_vol();
		
			if((!asc.isPlaying && type == vars.audio_playback_type.effect) || type == vars.audio_playback_type.none){
				Destroy(this);
			}

	}
	}
}
