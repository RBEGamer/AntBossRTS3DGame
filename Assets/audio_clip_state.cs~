using UnityEngine;
using System.Collections;

public class audio_clip_state : MonoBehaviour {

	public bool is_playing = false;
	public bool is_looping = false;


	public vars.audio_playback_type type;

	private AudioSource asc;
	// Use this for initialization
	void Awake () {
		type = vars.audio_playback_type.none;
		DontDestroyOnLoad(this);

		if(this.gameObject.GetComponent<AudioSource>() == null){
			this.gameObject.AddComponent<AudioSource>();
		}
	

	}


	public void start(vars.audio_name audio_file){
		asc = this.gameObject.GetComponent<AudioSource>();
		AudioClip ac ;
		switch (audio_file) {

		case vars.audio_name.bgmusic:
			 ac = (AudioClip)Resources.Load(vars.audio_clip_info_bgmusic.audio_clip_path);
			type = vars.audio_clip_info_bgmusic.ptype;
			asc.volume = vars.audio_clip_info_bgmusic.volume;
			asc.priority = vars.audio_clip_info_bgmusic.priority;
			asc.pitch = vars.audio_clip_info_bgmusic.pitch;
			break;

		case vars.audio_name.ui_click:
			ac = (AudioClip)Resources.Load(vars.audio_clip_info_uiclick.audio_clip_path);
			type = vars.audio_clip_info_uiclick.ptype;
			asc.volume = vars.audio_clip_info_uiclick.volume;
			asc.priority = vars.audio_clip_info_uiclick.priority;
			asc.pitch = vars.audio_clip_info_uiclick.pitch;
			break;
		default:
			ac = null;
			type = vars.audio_playback_type.none;
			asc.priority = 0;
			asc.volume = 0.0f;
			asc.pitch = 1.0f;
			break;
		}


		this.name = "audio_playback_" + ac.name;

		asc.clip = ac;

		if(type == vars.audio_playback_type.music){
			asc.loop = true;
		}else{
			asc.loop = false;
		}


		asc.Play();

	}
	// Update is called once per frame
	void FixedUpdate () {
	if(asc != null){
		is_playing = asc.isPlaying;
		is_looping = asc.loop;

			if(!asc.isPlaying || type == vars.audio_playback_type.none){
				Destroy(this);
			}

		}
	}
}
