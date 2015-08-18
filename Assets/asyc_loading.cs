using UnityEngine;
using System.Collections;

public class asyc_loading : MonoBehaviour {
	public bool sw;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(sw){
			Load("intro");
		}
	}


	public static void Load(string name) {
		GameObject go = new GameObject("LevelManager");
		asyc_loading instance = go.AddComponent<asyc_loading>();
		instance.StartCoroutine(instance.InnerLoad(name));
	}
	
	IEnumerator InnerLoad(string name) {
		//load transition scene
		Object.DontDestroyOnLoad(this.gameObject);
		Application.LoadLevel("Loading");

		while(Application.isLoadingLevel){
			Debug.Log(Time.deltaTime);
		}
		//wait one frame (for rendering, etc.)
		yield return null;
		
		//load the target scene
		Application.LoadLevel(name);
		Destroy(this.gameObject);
	}


}
