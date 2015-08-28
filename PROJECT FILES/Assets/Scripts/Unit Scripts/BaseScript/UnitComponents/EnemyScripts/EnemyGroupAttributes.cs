using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGroupAttributes : MonoBehaviour {
	public List<string> possibleRoutes;
	
	public RouteScript routeScript;
	public bool isPatrol;
	public float productionTime;
	public float spawnTimeLimit = 0.0f;
	public int startingWaypoint = -1;
}
