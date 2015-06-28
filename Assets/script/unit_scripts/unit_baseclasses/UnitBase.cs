using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum UnitFaction
{
	PlayerFaction,
	EnemyFaction,
	NeutralFaction,
	NoFaction
}

public abstract class UnitBase : MonoBehaviour
{
	
	// ---------------------------
	// General unit objects
	// ---------------------------
	public UnitRangeScript unitRange;
	protected NavMeshAgent unitNavMeshAgent;
	public UnitGroupBase unitGroup;
	protected Animator unitAnimator;

	// Tracks whether or not unit is going to be destroyed
	protected bool isUnitDisabled;
	
	// ---------------------------
	// Unit parameters
	// - to be adjusted via script/editor
	// ---------------------------
	// general attributes
	public UnitFaction unitFaction;
	public string unitName;

	// combat attributes
	public float unitBaseAttackspeed;
	protected float unitCurrentAttackspeed;

	public float unitBaseDamage;
	protected float unitCurrentDamage;

	public float unitBaseHealth;
	public float unitCurrentHealth;

	public float unitBaseMovementspeed;
	protected float unitCurrentMovementspeed;

	public float unitBaseVisionRange;
	protected float unitCurrentVisionrange;

	public float unitBaseAttackRange;
	protected float unitCurrentAttackRange;

	// unit cost
	public float unitCostFood;
	public float unitCostMaterials;
	
	// ---------------------------
	// Logic variablescv 
	// - influenced by unit logic
	// ---------------------------
	
	// Tracks current cooldown
	public float unitCurrentCombatCooldown;

	// Panic = no commands acceptable
	public bool isInPanic = false;

	// Unit behaviour based on command
	public int unitCommand;

	// Follow target(in group)
	public UnitBase followTarget;

	// Movement target
	public Vector3 unitMovementTarget;
	
	// Target priority; lower priority = change target
	public int unitTargetPriority;

	// Tracks units in range for later analysis
	//public List<UnitBase> unitsInRange;
	public List<GameObject> enemiesInRange;

	// Tracks units targeting this one
	public List<UnitBase> unitsTargetingMe;

	// Current combat target
	public GameObject unitCombatTarget;

	// For enemy purposes
	public RouteScript currentRoute;
	public int currentWayPoint = 1;

	// Use this for initialization
	public void Awake () {
		isUnitDisabled = false;
		unitNavMeshAgent = GetComponent<NavMeshAgent>();
		unitAnimator = GetComponent<Animator>();

		unitCurrentAttackspeed = unitGroup.attackspeed;
		unitCurrentMovementspeed = unitGroup.movementspeed;
		unitNavMeshAgent.speed = unitCurrentMovementspeed;
		
		unitCurrentDamage = unitGroup.damage;
		unitCurrentHealth = unitGroup.health;
		
		unitCurrentAttackRange = unitGroup.attackrange;
		unitCurrentVisionrange = unitGroup.visionRange;

		if (!unitGroup.myUnitList.Contains(GetComponent<UnitBase>()))
		{
			unitGroup.myUnitList.Add(GetComponent<UnitBase>());
		}

		if(unitRange.myCollider != null) {
			unitRange.myCollider.radius = unitCurrentVisionrange;
		}


	}


	public void Update() {
		// update destination if changed
		if (unitNavMeshAgent.destination != unitMovementTarget && !isUnitDisabled)
		{
			unitNavMeshAgent.SetDestination(unitMovementTarget);
		}
	}

	virtual public void dealDamage() {
		if(unitCombatTarget != null) {	
			unitCombatTarget.SendMessage("receiveDamage", unitCurrentDamage, SendMessageOptions.DontRequireReceiver);
		}
	}

	virtual public void receiveDamage(int damage) {
		unitCurrentHealth -= damage;
		if(unitCurrentHealth <= 0 && isUnitDisabled == false) {
			foreach(UnitBase t in unitsTargetingMe) {
				t.unitCombatTarget = null;
			}
			Destroy(gameObject);
			isUnitDisabled = true;
		}
	}

	virtual public bool analyseSpecialObjects() {
		return false;
	}

	virtual public void analyseUnitsInRange() {
		cleanUp();
		unitGroup.cleanUp();


		if(unitCurrentCombatCooldown <= 0) {
			if(analyseSpecialObjects()) {
				return;
			}

			List<GameObject> enemyUnitsInRange = new List<GameObject>();
			foreach(GameObject enemy in enemiesInRange) {
				if(enemy.tag != gameObject.tag && enemy.tag != "NeutralFaction") {
					enemyUnitsInRange.Add(enemy);
				}
			}

			if(enemyUnitsInRange.Count > 0) {
				GameObject closest = enemyUnitsInRange[0];
				foreach(GameObject enemy in enemyUnitsInRange) {
					UnitBase enemyUnitScript = enemy.GetComponent<UnitBase>();
					if(enemyUnitScript) {
						if(enemyUnitScript.unitCombatTarget == this) {
							if(unitTargetPriority < 80) {
								setTarget(closest, 80);
								return;
							}
						}
					}
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
				if(unitTargetPriority < 60) {
					setTarget(closest, 60);
					return;
				}
			}
			
			List<GameObject> enemyUnitsInGroupRange = new List<GameObject>();
			foreach(GameObject enemy in unitGroup.enemiesInGroupRange) {
				if(enemy.tag != gameObject.tag && enemy.tag != "NeutralFaction") {
					enemyUnitsInRange.Add(enemy);
				}
			}
			if(enemyUnitsInGroupRange.Count > 0) {
				GameObject closestInGroup = enemyUnitsInGroupRange[0];
				foreach(GameObject enemy in enemyUnitsInGroupRange) {
					if(enemy != null) {
						UnitBase enemyUnitScript = enemy.GetComponent<UnitBase>();
						if(enemyUnitScript) {
							if(enemyUnitScript.unitCombatTarget== this) {
								setTarget(enemy, 60);
								return;
							}
						}
						
							if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
						   Vector3.Distance(this.transform.position, closestInGroup.transform.position)) {
							closestInGroup = enemy;
						}
					}
				}
				if(unitTargetPriority < 40) {
					setTarget(closestInGroup, 40);
					return;
				}
				
			}
		}
	}

	virtual public void setTarget(GameObject target, int priority) {
		if(target != null && unitCombatTarget != null) {
			unitCombatTarget.SendMessage("removeUnitTargetingMe", this, SendMessageOptions.DontRequireReceiver);
			CancelInvoke("attackTarget");
		}
		unitTargetPriority = priority;
		unitCombatTarget = target;
		unitCommand = 2;
		if(target != null) {
			unitCombatTarget.SendMessage("addUnitTargetingMe", this, SendMessageOptions.DontRequireReceiver);
		}
		
	}

	virtual public void onDisable() {
		isUnitDisabled = true;
	}

	virtual public void addEnemyInRange(GameObject enemy) {
		enemiesInRange.Add(enemy.gameObject);
		if(unitGroup != null) {
			Debug.Log(gameObject.tag + " adds " + enemy.gameObject.tag + " to group " + unitGroup.tag);

			unitGroup.addEnemyToRange(enemy.gameObject);
		}
	}
	
	virtual public void removeEnemyInRange(GameObject enemy) {
		enemiesInRange.Remove(enemy);
		if(unitGroup != null) {
			Debug.Log(gameObject.tag + " removes " + enemy.gameObject.tag);
			if(enemiesInRange.Contains(enemy)) {

				unitGroup.removeEnemyFromRange(enemy);
			}
		}
	}

	virtual public void addUnitTargetingMe(UnitBase unit) {
		if(!unitsTargetingMe.Contains(unit)) {
			unitsTargetingMe.Add(unit);
		}
	}
	
	virtual public void removeUnitTargetingMe(UnitBase unit) {
		if(unitsTargetingMe.Contains(unit)) {
			unitsTargetingMe.Remove(unit);
		}
	}

	virtual public void cleanUp() {
		for(int i = enemiesInRange.Count - 1; i >= 0; i--) {
			if (enemiesInRange[i] == null)
			{
				enemiesInRange.RemoveAt(i);
			}
		}
	}
}
