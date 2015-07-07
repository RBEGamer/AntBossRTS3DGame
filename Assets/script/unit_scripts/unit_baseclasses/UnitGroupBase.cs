 // UnitGroup
// 
// Represents a unit group
// Controls units
//
//
// last change:  2015/06/06
//				Basic combat behaviour
//				2015/05/31
//               first iteration - Kevin 
// 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitGroupBase : MonoBehaviour {

	// ---------------------------
	// General
	// ---------------------------
	public UnitFaction unitGroupFaction;
	public string unitGroupName;
	public List<UnitBase> myUnitList;

	protected Renderer myRenderer;
	protected MeshRenderer myMeshRenderer;
	protected CapsuleCollider myCollider;

	// ---------------------------
	// Unit control
	// ---------------------------
	public Vector3 currentFormationPivot = -Vector3.zero;

	// ---------------------------
	// Combat variables
	// ---------------------------
	//protected UnitBase unitGroupTarget = null;
	protected GameObject unitGroupTarget = null;
	//public List<UnitBase> unitInGroupRange;
	public List<GameObject> enemiesInGroupRange;

	// ---------------------------
	// UnitGroup attributes
	// ---------------------------
	public float currentCombatCooldown = 0.0f;
	public float attackspeed = 1.0f;

	public float damage = 10;
	public float health = 100;
	public float movementspeed = 70;
	public float regeneration = 0;

	public float attackrange = 1.4f;
	public float visionRange = 5.0f;

	public int numUnitsInGroup = 0;

	// ---------------------------
	// Enemy stuff
	//---------------------------
	public bool isPatrol = false;

	// Use this for initialization
	void Awake () {
		myCollider = GetComponent<CapsuleCollider>();
		myRenderer = gameObject.GetComponent<Renderer>();
		
		if(unitGroupFaction == UnitFaction.EnemyFaction || unitGroupFaction == UnitFaction.NeutralFaction) {
			myMeshRenderer = gameObject.GetComponent<MeshRenderer>();
			myMeshRenderer.enabled = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
		cleanUp();
	}

	public void addEnemyToRange(GameObject enemy) {
		if(!enemiesInGroupRange.Contains(enemy)) {
			enemiesInGroupRange.Add (enemy);
		}
	}
	
	public void removeEnemyFromRange(GameObject enemy) {
		bool last = true;
		foreach(UnitBase t in myUnitList) {
			if(t.enemiesInRange.Contains(enemy)) {
				last = false;
			}
		}
		if(last) {
			enemiesInGroupRange.Remove(enemy);
		}
	}

	public void setAttributes(string name, float atkspeed, float newdamage, float newhealth, float movementspeed, float newattackrange, float newvisionrange) {
		unitGroupName = name;

		attackspeed = atkspeed;
		
		damage = newdamage;
		health = newhealth;
		movementspeed = movementspeed;
		
		attackrange = newattackrange;
		visionRange = newvisionrange;
	}
	
	public void setTargetEnemy(GameObject enemy) {
		transform.position = enemy.transform.position;
		unitGroupTarget = enemy;
		foreach (UnitBase t in myUnitList)
		{
			t.setTarget(enemy, 1000);
		}
	}
	
	// helper function
	public UnitBase findNearestUnit(Vector3 destination) {
		UnitBase nearestUnit = myUnitList[0];
		foreach (UnitBase t in myUnitList)
		{
			if (Vector3.Distance(nearestUnit.transform.position, destination) > Vector3.Distance(t.transform.position, destination))
			{
				nearestUnit = t;
			}
		}
		
		return nearestUnit;
	}

	public void addUnit(UnitBase unit) {
		if (!myUnitList.Contains(unit)) {
			myUnitList.Add(unit);
			numUnitsInGroup++;
		}
		
	}
	
	public void removeUnit(UnitBase unit) {
		if (myUnitList.Contains(unit)) {
			myUnitList.Remove(unit);
		}
	}
	
	// keep order in list
	public void cleanUp()
	{
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
	}
}
