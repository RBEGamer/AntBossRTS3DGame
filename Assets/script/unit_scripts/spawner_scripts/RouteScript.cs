using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[ExecuteInEditMode]  
public class RouteScript : MonoBehaviour {
	public List<GameObject> wayPointObjects;
	public bool isOccupied = false;

	//public Color routeColor;

	public bool clickToShowRoute;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Selection.activeGameObject == this.gameObject) {
			if(wayPointObjects.Count > 1) {
				float colorstep = 1.0f/(float)wayPointObjects.Count;

				for(int i = 0; i < wayPointObjects.Count-1; i++) {
					
					Debug.DrawRay(wayPointObjects[i].transform.position,
					              wayPointObjects[i+1].transform.position - wayPointObjects[i].transform.position, 
					              new Color(1 - i*colorstep,1 - i*colorstep,1 - i*colorstep),
					              2.0f);
				}
			}
		}
	}
}


