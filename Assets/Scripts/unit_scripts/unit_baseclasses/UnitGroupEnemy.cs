using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitGroupEnemy : UnitGroupBase {


	public List<string> possibleRoutes;

	public RouteScript myRoute;
	public float spawnTimeLimit = 0.0f;
	public float productionTime = 0.0f;
	public int startingWayPoint = -1;
	// Use this for initialization
	void Awake () {
		myCollider = GetComponent<CapsuleCollider>();
		myRenderer = gameObject.GetComponent<Renderer>();

		unitGroupFaction = UnitFaction.EnemyFaction;

		if(unitGroupFaction == UnitFaction.EnemyFaction || unitGroupFaction == UnitFaction.NeutralFaction) {
			myMeshRenderer = gameObject.GetComponent<MeshRenderer>();
			myMeshRenderer.enabled = false;
		}
		if(startingWayPoint != -1 && myRoute != null) {
			foreach(UnitBase unit in myUnitList) {
				Debug.Log ("TEST");
				unit.currentRoute = myRoute;
				myRoute.isOccupied = true;
				unit.currentWayPoint = startingWayPoint - 1;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		cleanUp();

		foreach(UnitBase unit in myUnitList) {
			unit.currentRoute = myRoute;
			if(startingWayPoint != -1) {
				unit.currentWayPoint = startingWayPoint - 1;
			}
		}
	}
	
	public void cleanUp() {
		for(int i = myUnitList.Count - 1; i >= 0; i--) {
			if (myUnitList[i] == null)
			{
				myUnitList.RemoveAt(i);
			}
		}
		
		
		for(int i = enemiesInGroupRange.Count - 1; i >= 0; i--) {
			if (enemiesInGroupRange[i] == null)
			{
				enemiesInGroupRange.RemoveAt(i);
			}
		}
		if (myUnitList.Count == 0)
		{
			myRoute.isOccupied = false;
			Destroy(gameObject);
		}
	}

	public void setRoute(RouteScript route) {
		myRoute = route;
		foreach(UnitBase unit in myUnitList) {
			unit.currentRoute = route;
		}
	}

}
