using UnityEngine;

public class Temporary : MonoBehaviour {

	public float duration = 5;

	void Start() {
		Destroy(gameObject, duration);
	}

}
