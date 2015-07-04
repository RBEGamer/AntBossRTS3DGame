using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct SavedUnitGroup {
	public Vector3 position;
	public string unitGroupName;

	public int numUnits;
	public int[] unitHealths;

	public float attackspeed;
	
	public float damage;
	public float health;
	public float movementspeed;
	
	public float attackrange;
	public float visionRange;

	public void initialize() {
		position = Vector3.zero;

		attackspeed = 1;
		movementspeed = 5;
		
		damage = 10;
		health = 100;
		
		attackrange = 5;
		visionRange = 5;
		
		numUnits = 0;
	}
}


public class UnitGroupCache : MonoBehaviour {

	public GameObject unitGroupPrefab;
	public GameObject unitPrefab;

	public List<SavedUnitGroup> unitGroupsSaved;
	public static int unitGroupCounter = 1;
	
	// Use this for initialization
	void Start () {
		// TEST 
		unitGroupsSaved = new List<SavedUnitGroup>();
		SavedUnitGroup newUnitGroup = createNewGroup();
		newUnitGroup.numUnits = 2;
		spawnUnitgroup(newUnitGroup);
		// TEST
	}


	void Update() {
		Debug.Log(unitGroupsSaved.Count);

		// TEST
		if(Input.GetKeyDown(KeyCode.H)) {
			spawnUnitgroup(unitGroupsSaved[unitGroupsSaved.Count-1]);
		}
		//
	}

	// creates new(empty) group
	public SavedUnitGroup createNewGroup() {
		SavedUnitGroup newUnitGroup = new SavedUnitGroup();
		newUnitGroup.initialize();
		newUnitGroup.unitGroupName = "Standard Ants " + unitGroupCounter.ToString();
		newUnitGroup.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
		unitGroupCounter++;
		unitGroupsSaved.Add (newUnitGroup);

		return newUnitGroup;
	}

	// deletes existing group
	// returns num units
	public int deleteUnitGroup(SavedUnitGroup group) {
		if(unitGroupsSaved.Contains(group)) {
			unitGroupsSaved.Remove (group);
		}

		return group.numUnits;
	}

	// deletes a unit from a group
	public void deleteUnitFromGroup(SavedUnitGroup group) {
		if(group.numUnits > 0) {
			group.numUnits--;
		}
	}

	// adds a unit group(on retreat)
	public void addUnitGroup(UnitGroupBase group) {
		SavedUnitGroup newUnitGroup = new SavedUnitGroup();

		newUnitGroup.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
		newUnitGroup.unitGroupName = group.name;
		newUnitGroup.attackspeed = group.attackspeed;
		newUnitGroup.movementspeed = group.movementspeed;
		
		newUnitGroup.damage = group.damage;
		newUnitGroup.health = group.health;
		
		newUnitGroup.attackrange = group.attackrange;
		newUnitGroup.visionRange = group.visionRange;

		newUnitGroup.numUnits = group.numUnitsInGroup;
		unitGroupsSaved.Add (newUnitGroup);
	}

	// spawns a waiting unit group
	public void spawnUnitgroup(SavedUnitGroup group) {
		GameObject newgroup = Instantiate(unitGroupPrefab, group.position, Quaternion.identity) as GameObject;
		UnitGroupBase unitGroupBase = newgroup.GetComponent<UnitGroupBase>();
		unitGroupBase.setAttributes(group.unitGroupName, group.attackspeed, group.damage, group.health, group.movementspeed, group.attackrange, group.visionRange);

		GameObject newunit;

		for(int i = 0; i < group.numUnits; i++) {
			newunit = Instantiate(unitPrefab, group.position, Quaternion.identity) as GameObject;
			UnitBase unitBase = newunit.GetComponent<UnitBase>();
			unitBase.setUnitGroup(unitGroupBase);
			//unitBase.setAttributesFromGroup();
		}

		deleteUnitGroup(group);
	}
}
