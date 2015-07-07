﻿using UnityEngine;
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
	
	public void initialize(float _attackspeed, float _movementspeed, float _damage, float _health, float _attackrange, float _visionrange) {
		position = Vector3.zero;
		
		attackspeed = _attackspeed;
		movementspeed = _movementspeed;
		
		damage = _damage;
		health = _health;
		
		attackrange = _attackrange;
		visionRange = _visionrange;
		
		numUnits = 0;
	}
}


public class UnitGroupCache : MonoBehaviour {
	
	public GameObject unitGroupPrefab;
	public GameObject unitPrefab;
	
	public List<SavedUnitGroup> unitGroupsSaved;
	public static int unitGroupCounter = 1;

	public float standardAttackspeed;
	
	public float standardDamage;
	public float standardHealth;
	public float standardMovementspeed;
	
	public float standardAttackrange;
	public float standardVisionRange;

	// Use this for initialization
	void Start () {
		unitGroupsSaved = new List<SavedUnitGroup>();

		UnitGroupBase t = unitGroupPrefab.GetComponent<UnitGroupBase>();
		standardAttackspeed = t.attackspeed;
		
		standardDamage = t.damage;
		standardHealth = t.health;
		standardMovementspeed = t.movementspeed;
		
		standardAttackrange = t.attackrange;
		standardVisionRange = t.visionRange;
	}
	
	
	void Update() {

		
		// TEST
		if(Input.GetKeyDown(KeyCode.H)) {
			spawnUnitgroup(unitGroupsSaved[unitGroupsSaved.Count-1]);
		}
		//
	}
	
	// creates new(empty) group
	public SavedUnitGroup createNewGroup() {
		SavedUnitGroup newUnitGroup = new SavedUnitGroup();
		newUnitGroup.initialize(standardAttackspeed, standardMovementspeed, standardDamage, standardHealth, standardAttackrange, standardVisionRange);
		newUnitGroup.unitGroupName = "Standard Ants " + unitGroupCounter.ToString();
		newUnitGroup.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
		unitGroupCounter++;
		unitGroupsSaved.Add (newUnitGroup);
		
		return newUnitGroup;
	}
	
	// deletes existing group
	// returns num units
	public int deleteUnitGroup(SavedUnitGroup group) {
		for(int i = unitGroupsSaved.Count - 1; i >= 0; i--) {
			if (unitGroupsSaved[i].unitGroupName  == group.unitGroupName)
			{
				unitGroupsSaved.RemoveAt(i);
			}
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
