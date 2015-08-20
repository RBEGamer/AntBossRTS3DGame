using UnityEngine;
using System.Collections;

public class TutorialResetRessources : MonoBehaviour {
	public base_manager baseManager;

	public int startARessource;
	public int startBRessource;
	public int startAttacker;
	public int startScouts;
	public int startCollector;
	// Use this for initialization
	void Start () {
		baseManager.res_a_storage = startARessource;
		baseManager.res_b_storage = startBRessource;
		baseManager.bought_scout_ants = startScouts;
		baseManager.bought_collector_ants = startCollector;
		baseManager.bought_attack_ants = startAttacker;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
