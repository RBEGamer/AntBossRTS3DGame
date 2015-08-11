using UnityEngine;
using System.Collections;

public class UnitPlaceHolder : MonoBehaviour {

	public GameObject unitPrefab;
	// Use this for initialization
	void Awake () {
		GameObject newObject = (GameObject)Instantiate(unitPrefab, transform.position, Quaternion.identity) as GameObject;
		if(newObject != null) {
			newObject.GetComponent<UnitScript>().unitGroupScript = transform.parent.gameObject.GetComponent<UnitGroupScript>();
			newObject.transform.parent = transform.parent;
		}
		//Debug.Log(newObject.name);
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
