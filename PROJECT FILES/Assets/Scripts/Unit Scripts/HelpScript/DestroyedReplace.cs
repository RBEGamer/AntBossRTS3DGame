using UnityEngine;
using System.Collections;

public class DestroyedReplace : MonoBehaviour {

	public GameObject deathPrefab;

	// Update is called once per frame
	void destroyObject() {

		Instantiate(deathPrefab, transform.position, transform.rotation);

		gameObject.SetActive(false);
		Destroy(gameObject, 0.1f);
	}
}	