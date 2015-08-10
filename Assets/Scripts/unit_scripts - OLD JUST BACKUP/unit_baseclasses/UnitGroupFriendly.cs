﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitGroupFriendly : UnitGroupBase {
	public static UnitGroupUIManager myUnitGroupManager;
	
	public List<GameObject> baseList;
	public GameObject nearestBase;
	public Vector3 nearestBasePosition;
	
	private bool isGroupSelected = false;
	private bool inPanic = false;
	private bool retreatToBase = false;

	
	// Use this for initialization
	void Start () {
		myCollider = GetComponent<CapsuleCollider>();
		myRenderer = gameObject.GetComponent<Renderer>();
		
		myUnitGroupManager = GameObject.FindGameObjectWithTag(vars.unitgroup_tag+""+vars.unitgroup_manager_tag).GetComponent<UnitGroupUIManager>();
		myUnitGroupManager.unitGroupList.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.rotation = new Quaternion (270.0f, 0.0f, 0.0f, 1.0f);
		if (Input.GetKeyDown(vars.key_panic) && isSelected())
		{
			setPanic();
		}
		
		if(unitGroupTarget != null) {
			transform.position = new Vector3(unitGroupTarget.transform.position.x, unitGroupTarget.transform.position.y + 5.0f, unitGroupTarget.transform.position.z);
		}
		else {
			transform.position = new Vector3(transform.position.x, 4.0f, transform.position.z);
		}
		
		cleanUp();
	}
	
	public void setAttributes(string name, float atkspeed, float damage, float health, float movementspeed, float attackrange, float visionrange) {
		unitGroupName = name;
		attackspeed = atkspeed;
		
		damage = damage;
		health = health;
		movementspeed = movementspeed;
		
		attackrange = attackrange;
		visionRange = visionrange;
		
	}
	
	// issues retreat command
	public void setPanic()
	{
		inPanic = true;
		findNearestBasePosition();
		foreach (UnitBase t in myUnitList)
		{
			t.isInPanic = true;
			t.unitMovementTarget = nearestBasePosition;
		}
		transform.position = nearestBasePosition + Vector3.up *5;
	}
	
	
	// rightclick(when selected)
	public void OnRightclick(Vector3 destination) {
		unitGroupTarget = null;
		if (!inPanic && !startedLeaving)
		{
			if (myCollider.bounds.Contains(destination))
			{
				moveToDefensePoint(this.transform.position);
				return;
			}
			else
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if(hit.collider.gameObject.tag.Contains(vars.blockage_tag)) {
						return;
					}
					if (hit.collider.gameObject.tag.Contains(vars.enemy_tag) && hit.collider.gameObject.tag.Contains(vars.attackable_tag))
					{
						Debug.Log("hit: " + hit.collider.name);
						setTargetEnemy(hit.collider.gameObject);
						return;
					}

					if(hit.collider.gameObject.tag.Contains (vars.base_tag)) {
						findNearestBasePosition();
						moveToBase(baseList[0]);
						return;
					}
				} 
				placeNewDefensePoint(destination);
			}
		}
	}
	
	// a-move / standard right click
	public void placeNewDefensePoint(Vector3 destination)
	{
		transform.position = destination;
		UnitBase nearestUnit = findNearestUnit(destination);
		
		foreach (UnitBase t in myUnitList)
		{
			t.followTarget = null;
			t.setTarget(null, 0);
			if (t == nearestUnit)
			{
				t.unitMovementTarget = destination;
			}
			else
			{
				t.followTarget = nearestUnit;
			}

			t.unitCommand = UnitCommand.AttackMove;
			t.retreatToBase = false;
			retreatToBase = false;
			//t.isNearDefensePoint = false;
		}
		myRenderer.material.color = Color.white;
	}
	
	// retreat to base / double right click
	public void moveToBase(GameObject targetbase)
	{
		if(targetbase == unitGroupTarget) {
			foreach (UnitBase t in myUnitList)
			{
				t.unitCommand = UnitCommand.Move;
				t.retreatToBase = true;
				retreatToBase = true;
			}
		} else {
			UnitBase nearestUnit = findNearestUnit(targetbase.transform.position);
			myRenderer.material.color = Color.white;
			transform.position = targetbase.transform.position;
			unitGroupTarget = targetbase;
			foreach (UnitBase t in myUnitList)
			{
				t.setTarget(null, 0);
				t.followTarget = null;
				if (t == nearestUnit)
				{
					t.unitMovementTarget = targetbase.transform.position;
				}
				else
				{
					t.followTarget = nearestUnit;
				}
				t.unitCommand = UnitCommand.AttackMove;
				t.retreatToBase = true;
				retreatToBase = true;
			}
		}
	}

	// retreat to defense point / double right click
	public void moveToDefensePoint(Vector3 destination)
	{
		UnitBase nearestUnit = findNearestUnit(destination);
		myRenderer.material.color = Color.blue;
		foreach (UnitBase t in myUnitList)
		{
			t.setTarget(null, 0);
			t.followTarget = null;
			if (t == nearestUnit)
			{
				t.unitMovementTarget = destination;
			}
			else
			{
				t.followTarget = nearestUnit;
			}
			t.unitCommand = UnitCommand.Move;
			t.retreatToBase = false;
			retreatToBase = false;
		}

	}
	
	// helper function
	public void findNearestBasePosition()
	{
		baseList = myUnitGroupManager.baseList;
		nearestBasePosition = baseList[0].transform.position;
		foreach (GameObject t in baseList)
		{
			if (Vector3.Distance(transform.position, nearestBasePosition) > Vector3.Distance(t.transform.position, nearestBasePosition))
			{
				nearestBasePosition = t.transform.position;
				nearestBase = t;
			}
		}
	}
	
	
	
	public void cleanUp() {
		
		for(int i = myUnitList.Count - 1; i >= 0; i--) {
			if (myUnitList[i] == null)
			{
				myUnitList.RemoveAt(i);
				numUnitsInGroup--;
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
			if(inPanic || retreatToBase) {
				nearestBase.GetComponent<UnitGroupCache>().addUnitGroup(this);
			}
			myUnitGroupManager.unitGroupList.Remove(this);
			Destroy(gameObject);
		}
	}
	
	public void OnSelected()
	{
		isGroupSelected = true;
		myUnitGroupManager.selectedUnitGroupBase = this;
		GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().slot_0_set_unit();
	}
	
	public void OnUnselected()
	{
		if (myUnitGroupManager.isAmove != true)
		{
			isGroupSelected = false;
			GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().slot_0_set_empty();
			myUnitGroupManager.selectedUnitGroupBase = null;
			
		}
		
	}
	
	public bool isSelected() { return isGroupSelected;}
	
	public string getName() { return unitGroupName; }
}
