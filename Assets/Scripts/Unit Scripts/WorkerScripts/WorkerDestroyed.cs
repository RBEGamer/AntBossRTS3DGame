using UnityEngine;
using System.Collections;

public class WorkerDestroyed : MonoBehaviour {

	public WorkerScript workerScript;

	void destroyObject() {
		workerScript.targetRessource.res.current_collection_ants -=1;
		workerScript.targetRessource.res.target_collection_ants -= 1;
		//workerScript.targetRessource.current_ants_working_on_this_res -= 1;
		gameObject.SetActive(false);
		Destroy(gameObject, 0.1f);
	}
}
