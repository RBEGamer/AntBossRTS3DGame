using UnityEngine;
using System.Collections;

public class map_animation_loader : MonoBehaviour {


	public GameObject map_animation_prefab;
	// Use this for initialization
	void Start () {
		if(GameObject.Find(map_animation_prefab.name) == null){
			GameObject igo = (GameObject)Instantiate(map_animation_prefab, new Vector3(0.0f,0.0f,0.0f), new Quaternion(0.0f,0.0f,0.0f,1.0f));

		}
		DontDestroyOnLoad(GameObject.Find(map_animation_prefab.name));
	}
	
	// Update is called once per frame
	void Update () {
	




	}
}
