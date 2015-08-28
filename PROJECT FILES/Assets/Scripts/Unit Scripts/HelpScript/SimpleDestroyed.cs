using UnityEngine;
using System.Collections;

public class SimpleDestroyed : MonoBehaviour {
	// Update is called once per frame
	void destroyObject() {
		gameObject.SetActive(false);
		Destroy(gameObject, 0.1f);
	}
}