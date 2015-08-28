using UnityEngine;
using System.Collections;

public class WorkerTutorial : MonoBehaviour {

	public path_point preMadeWP;
	public WorkerScript workerScript;

	public void Start() {
		workerScript = GetComponent<WorkerScript>();
		workerScript.initializeWorker(preMadeWP);
	}


}
