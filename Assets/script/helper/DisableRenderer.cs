using UnityEngine;
using System.Collections;

public class DisableRenderer : MonoBehaviour {
	public MeshRenderer myMeshRenderer;
	// Use this for initialization
	void Start () {
		myMeshRenderer = gameObject.GetComponent<MeshRenderer>();
		myMeshRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
