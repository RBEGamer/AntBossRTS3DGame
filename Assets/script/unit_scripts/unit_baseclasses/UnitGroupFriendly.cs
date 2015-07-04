using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitGroupFriendly : UnitGroupBase {
	public static UnitGroupUIManager myUnitGroupManager;

	public List<GameObject> baseList;
	public GameObject nearestBase;
	public Vector3 nearestBasePosition;

	private bool isGroupSelected = false;
	private bool inPanic = false;



	// Use this for initialization
	void Start () {
		myCollider = GetComponent<CapsuleCollider>();
		myUnitGroupManager = GameObject.FindGameObjectWithTag("UnGMan").GetComponent<UnitGroupUIManager>();
		myUnitGroupManager.unitGroupList.Add(this);
		myRenderer = gameObject.GetComponent<Renderer>();


		myUnitGroupManager = GameObject.FindGameObjectWithTag("UnGMan").GetComponent<UnitGroupUIManager>();
		myUnitGroupManager.unitGroupList.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R) && isSelected())
		{
			setPanic();
		}

		if(unitGroupTarget != null) {
			transform.position = unitGroupTarget.gameObject.transform.position;
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
		if (!inPanic)
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
					if (hit.transform.gameObject.tag == "Enemy")
					{
						Debug.Log("Setting target to " + hit.transform.gameObject.name);
						setTargetEnemy(hit.transform.gameObject);
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
			t.unitCommand = -1;
		}
		myRenderer.material.color = Color.white;
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
			t.unitCommand = 1;
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
			if(inPanic) {
				Debug.Log("HEY!");
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
	}
	
	public void OnUnselected()
	{
		if (myUnitGroupManager.isAmove != true)
		{
			isGroupSelected = false;
			myUnitGroupManager.selectedUnitGroupBase = null;
		}
	}
	
	public bool isSelected() { return isGroupSelected;}
	
	public string getName() { return unitGroupName; }
}
