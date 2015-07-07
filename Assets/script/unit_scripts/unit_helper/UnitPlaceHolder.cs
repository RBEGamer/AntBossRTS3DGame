using UnityEngine;
using System.Collections;

public class UnitPlaceHolder : MonoBehaviour {

	public GameObject unitPrefab;
	// Use this for initialization
	void Awake () {
		GameObject newObject = (GameObject)Instantiate(unitPrefab, transform.position, Quaternion.identity) as GameObject;
		if(newObject != null) {
			newObject.GetComponent<UnitBase>().setUnitGroup(transform.parent.gameObject.GetComponent<UnitGroupBase>());
			newObject.transform.parent = transform.parent;
		}
		//Debug.Log(newObject.name);
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
