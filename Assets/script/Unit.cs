// Unit
// 
// Genral unit logic
// 
//
//
// last change:  2015/05/31
//               first iteration - Kevin 
// 
using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, ObjectBase {


    // General
    public UnitGroup myUnitGroup = null;
    public GameObject myUnitGroupGameObject = null;
    private NavMeshAgent myNavMeshAgent;

    // Unit logic
    // -1: attack move to defense point
    // 0 : idle
    // 1: move to defense point (retreat to point)
    public int currentCommand = -2;
    public Vector3 targetPosition = -Vector3.zero;
    public bool isNearDefensePoint = false;
    public Unit followTarget = null;
    public float spreadDistance = 10.0f;

    // panic = retreat to base
    public bool isInPanic = false;
    // TEMPORARY
    public bool isDisabled = false;

	// Use this for initialization
	void Start () {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myUnitGroupGameObject = myUnitGroup.gameObject;
        if (!myUnitGroup.myUnitList.Contains(GetComponent<Unit>()))
        {
            myUnitGroup.myUnitList.Add(GetComponent<Unit>());
        }
        

        currentCommand = -1;
        getNewIdlePosition(myUnitGroup.transform.position, spreadDistance);
	}
	
	// Update is called once per frame
	void Update () {

        if (isInPanic == false)
        {
            // -1: attack move to defense point
            if (currentCommand == -1)
            {
                // checkforEnemies
                // TODO
                if (followTarget != null)
                {
                    targetPosition = followTarget.transform.position;
                }
            }


            // move to defense point
            if (currentCommand == 1)
            {
                if (followTarget != null)
                {
                    targetPosition = followTarget.transform.position;
                }
            }

            // idle
            if (currentCommand == 0)
            {
                if (!myNavMeshAgent.pathPending)
                {
                    if (myNavMeshAgent.remainingDistance <= myNavMeshAgent.stoppingDistance)
                    {
                        if (!myNavMeshAgent.hasPath || myNavMeshAgent.velocity.sqrMagnitude == 0f)
                        {
                            // Done 
                            getNewIdlePosition(myUnitGroup.transform.position, spreadDistance);
                        }
                    }
                }
            }
        }
        else // is in panic, retreat to closest base
        {
            targetPosition = myUnitGroup.nearestBasePosition;
            myNavMeshAgent.stoppingDistance = 5.0f;
            if (!myNavMeshAgent.pathPending)
            {
                if (myNavMeshAgent.remainingDistance <= myNavMeshAgent.stoppingDistance)
                {
                    if (!myNavMeshAgent.hasPath || myNavMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        // Done 
                        
                        foreach (Transform child in transform)
                        {
                            GameObject.Destroy(child.gameObject);
                        }
                        Destroy(gameObject);
                        isDisabled = true;
                    }
                }
            }

        }

        // update destination if changed
        if (myNavMeshAgent.destination != targetPosition && !isDisabled)
        {
                myNavMeshAgent.SetDestination(targetPosition);
        }
	}

    // TEMPORARY FIX
    public void onDisable()
    {
        isDisabled = true;
    }
    
    // helper function
    public UnitGroup getUnitGroup()
    {
        return myUnitGroup;
    }

    // detect if outside of defense point
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == myUnitGroupGameObject)
        {
            isNearDefensePoint = false;
        }
    }

    // detect if inside of defense point
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == myUnitGroupGameObject)
        {
            //if (followTarget != null)
            //{
                followTarget = null;
                getNewIdlePosition(myUnitGroup.transform.position, spreadDistance);
                currentCommand = 0;
            //}
            if (isNearDefensePoint == false)
            {
                isNearDefensePoint = true;
            }
        }
    }

    // helper function
    public void getNewIdlePosition(Vector3 pivot, float distance)
    {
        targetPosition = new Vector3(pivot.x + Random.Range(-distance, distance) + 1.0f,
                            transform.position.y,
                            pivot.z + Random.Range(-distance, distance) + 1);
    }

    // OBJECT BASE
    public void OnSelected() { }

    public void OnUnselected() { }

    public void OnRightclick(Vector3 destination) { }

    public bool isSelected() { return false; }

    public string getName() { return ""; }

    public void moveToTarget(GameObject target) { }

    public void moveToVector3(Vector3 destination) { }
}
