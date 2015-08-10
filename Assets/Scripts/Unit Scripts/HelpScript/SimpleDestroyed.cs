using UnityEngine;
using System.Collections;

public class SimpleDestroyed : MonoBehaviour {
	// Update is called once per frame
	void destroyObject() {
		Destroy(gameObject);
	}
}