using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerScript : MonoBehaviour {
	public List<RouteScript> spawnerRoutes;

	public List<GameObject> availableUnitGroups;
	public float[] unitSpawnChances;
	public List<GameObject> unitGroupList;


	public float spawnIntervall = 2.0f;
	public float currentSpawnTimer = 0.0f;

	public float TotalSpawnTime = 0.0f;


	public int spawnerHealth = 10;

	void Start () {
		int i = 0;
		foreach(GameObject enemygroup in availableUnitGroups) {
			for(int j = 0; j < unitSpawnChances[i]; j++) {
				unitGroupList.Add(enemygroup);
			}
			i++;
		}

		foreach(RouteScript route in spawnerRoutes) {

			if(route.isOccupied == false) {
				List<UnitGroupEnemy> listAvailableGroups = new List<UnitGroupEnemy>();
				foreach(GameObject group in unitGroupList) {
					UnitGroupEnemy temp = group.GetComponent<UnitGroupEnemy>();
					if(temp.possibleRoutes.Contains(route.gameObject.name)) {
						listAvailableGroups.Add (temp);
					}
				}


				int x = Random.Range (0, listAvailableGroups.Count);

				while(TotalSpawnTime < listAvailableGroups[x].spawnTimeLimit) {
					x = Random.Range (0, listAvailableGroups.Count);
				}

				GameObject newgroup = Instantiate(listAvailableGroups[x].gameObject, transform.position, Quaternion.identity) as GameObject;
				newgroup.GetComponent<UnitGroupEnemy>().setRoute(route);
				route.isOccupied = true;
			}
		}
	}

	 public void receiveDamage(int damage) {
		spawnerHealth -= damage;
		if(spawnerHealth <= 0) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		currentSpawnTimer += Time.deltaTime;
		TotalSpawnTime += Time.deltaTime;

		if(currentSpawnTimer > spawnIntervall) {
			currentSpawnTimer = 0.0f;
			foreach(RouteScript route in spawnerRoutes) {
				if(route.isOccupied == false) {
					List<UnitGroupEnemy> listAvailableGroups = new List<UnitGroupEnemy>();
					foreach(GameObject group in unitGroupList) {
						UnitGroupEnemy temp = group.GetComponent<UnitGroupEnemy>();
						if(temp.possibleRoutes.Contains(route.gameObject.name)) {
							listAvailableGroups.Add (temp);
						}
					}
					
					int x = Random.Range (0, listAvailableGroups.Count);
					
					while(TotalSpawnTime < listAvailableGroups[x].spawnTimeLimit) {
						x = Random.Range (0, listAvailableGroups.Count);
					}
					
					GameObject newgroup = Instantiate(listAvailableGroups[x].gameObject, transform.position, Quaternion.identity) as GameObject;
					newgroup.GetComponent<UnitGroupEnemy>().setRoute(route);
					route.isOccupied = true;
				}
			}
		}
	}
	
}
