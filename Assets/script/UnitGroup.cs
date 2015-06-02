// UnitGroup
// 
// Represents a unit group
// Controls units
//
//
// last change:  2015/05/31
//               first iteration - Kevin 
// 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitGroup : MonoBehaviour, ObjectBase {

    // General
    public List<Unit> myUnitList;
    public List<GameObject> baseList;
    public Vector3 nearestBasePosition;
    public CapsuleCollider myCollider;
    public static UnitGroupUIManager myUnitGroupManager;

    // Unit control
    public Vector3 currentFormationPivot = -Vector3.zero;
    private bool isGroupSelected = false;
    private bool inPanic = false;

	// Use this for initialization
	void Start () {
        myCollider = GetComponent<CapsuleCollider>();
        myUnitGroupManager = GameObject.FindGameObjectWithTag("UnitGroupManager").GetComponent<UnitGroupUIManager>();
        myUnitGroupManager.unitGroups.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R) && isSelected())
        {
            setPanic();
        }

        cleanUp();
	}

    public void OnSelected()
    {
        isGroupSelected = true;
        myUnitGroupManager.selectedUnitGroup = this;
    }

    public void OnUnselected()
    {
        if (myUnitGroupManager.isAmove != true)
        {
            isGroupSelected = false;
            myUnitGroupManager.selectedUnitGroup = null;
        }
    }

    // issues retreat command
    public void setPanic()
    {
        inPanic = true;
        foreach (Unit t in myUnitList)
        {
            t.isInPanic = true;
            findNearestBasePosition();
        }
        transform.position = nearestBasePosition;
    }

    // rightclick(when selected)
    public void OnRightclick(Vector3 destination) {
        if (!inPanic)
        {
            if (myCollider.bounds.Contains(destination))
            {
                moveToDefensePoint(this.transform.position);
            }
            else
            {
                placeNewDefensePoint(destination);
            }
        }
    }
    
    // a-move / standard right click
    public void placeNewDefensePoint(Vector3 destination)
    {
        transform.position = destination;
        Unit nearestUnit = findNearestUnit(destination);

        foreach (Unit t in myUnitList)
        {
            t.followTarget = null;
            if (t == nearestUnit)
            {
                t.targetPosition = destination;
            }
            else
            {
                t.followTarget = nearestUnit;
            }
            t.currentCommand = -1;
        }
    }

    // retreat to defense point / double right click
    public void moveToDefensePoint(Vector3 destination)
    {
        Unit nearestUnit = findNearestUnit(destination);

        foreach (Unit t in myUnitList)
        {
            t.followTarget = null;
            if (t == nearestUnit)
            {
                t.targetPosition = destination;
            }
            else
            {
                t.followTarget = nearestUnit;
            }
            t.currentCommand = 1;
        }
    }

    // helper function
    public Unit findNearestUnit(Vector3 destination) {
        Unit nearestUnit = myUnitList[0];
        foreach (Unit t in myUnitList)
        {
            if (Vector3.Distance(nearestUnit.transform.position, destination) > Vector3.Distance(t.transform.position, destination))
            {
                nearestUnit = t;
            }
        }

        return nearestUnit;
    }


    // helper function
    public void findNearestBasePosition()
    {
        nearestBasePosition = baseList[0].transform.position;
        foreach (GameObject t in baseList)
        {
            if (Vector3.Distance(transform.position, nearestBasePosition) > Vector3.Distance(t.transform.position, nearestBasePosition))
            {
                nearestBasePosition = t.transform.position;
            }
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
        if (myUnitList.Count == 0)
        {
            myUnitGroupManager.unitGroups.Remove(this);
            Destroy(gameObject);
        }
    }
    public bool isSelected() { return isGroupSelected; }

    public string getName() { return ""; }

    public void moveToTarget(GameObject target) { }

    public void moveToVector3(Vector3 destination) { }
}
