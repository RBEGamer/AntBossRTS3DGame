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

	public static ui_manager uiManager;

	public UnitRangeScript unitRange;
	protected NavMeshAgent unitNavMeshAgent;
	public UnitGroupBase unitGroup;
	protected Animator unitAnimator;
	protected Transform unitTransform;

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

	public float unitBaseRegeneration;
	protected float unitCurrentRegeneration;

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
	// Logic variables
	// - influenced by unit logic
	// ---------------------------


	// Tracks current cooldown
	public float unitCurrentCombatCooldown;

	// Panic = no commands acceptable
	public bool isInPanic = false;
	public bool retreatToBase = false;

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
	
	// combat purposes
	public float unitRadius;
	protected float enemyUnitRadius;

	// For enemy purposes
	public RouteScript currentRoute;
	public int currentWayPoint = 1;


	// Upgrade logic(friendly units only)
	public upgrade_description upgrade_slot_0;
	public upgrade_description upgrade_slot_1;
	public upgrade_description upgrade_slot_2;



	// Use this for initialization
	public void Awake () {
		uiManager = GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>();
		unitNavMeshAgent = GetComponent<NavMeshAgent>();
		unitAnimator = GetComponent<Animator>();
		if(unitGroup != null) {
			unitCurrentAttackspeed = unitGroup.attackspeed;
			unitCurrentMovementspeed = unitGroup.movementspeed;
			unitCurrentRegeneration = unitGroup.regeneration;
			unitNavMeshAgent.speed = unitCurrentMovementspeed;
			
			unitCurrentDamage = unitGroup.damage;
			unitCurrentHealth = unitGroup.health;
			
			unitCurrentAttackRange = unitGroup.attackrange;
			unitCurrentVisionrange = unitGroup.visionRange;
			unitGroup.addUnit(this);
		}

		isUnitDisabled = false;

		unitCombatTarget = null;
		if(unitRange.myCollider != null) {
			unitRange.myCollider.radius = unitCurrentVisionrange;
		}

		unitTransform = this.transform;
		unitRadius = unitNavMeshAgent.radius + 1;
	}

	public void setUnitGroup(UnitGroupBase newUnitGroup) {
		unitGroup = newUnitGroup;
	}

	public void setAttributesFromGroup() {
		// combat attributes
		unitCurrentAttackspeed = unitGroup.attackspeed;
		unitCurrentMovementspeed = unitGroup.movementspeed;
		unitNavMeshAgent.speed = unitCurrentMovementspeed;
		
		unitCurrentDamage = unitGroup.damage;
		unitCurrentHealth = unitGroup.health;
		unitCurrentRegeneration = unitGroup.regeneration;

		unitCurrentAttackRange = unitGroup.attackrange;
		unitCurrentVisionrange = unitGroup.visionRange;

		unitBaseAttackspeed = unitGroup.attackspeed;
		unitBaseMovementspeed = unitGroup.movementspeed;

		unitBaseDamage = unitGroup.damage;
		unitBaseHealth = unitGroup.health;
		
		unitBaseAttackRange = unitGroup.attackrange;
		unitBaseVisionRange = unitGroup.visionRange;

		unitBaseRegeneration = unitGroup.regeneration;
		if(unitRange.myCollider != null) {
			unitRange.myCollider.radius = unitCurrentVisionrange;
		}

		if(unitGroup != null) {
			unitGroup.addUnit(this);
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
				t.unitTargetPriority = 0;
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
				//if(enemy.tag != gameObject.tag && enemy.tag != "NeutralFaction") {
					enemyUnitsInRange.Add(enemy);
				//}
			}

			if(enemyUnitsInRange.Count > 0) {
				GameObject closest = enemyUnitsInRange[0];
				foreach(GameObject enemy in enemyUnitsInRange) {
					UnitBase enemyUnitScript = enemy.GetComponent<UnitBase>();
					if(enemyUnitScript) {
						if(enemyUnitScript.unitCombatTarget == this) {
								setTarget(closest, 100);
								return;
						}
					}
					if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
					   Vector3.Distance(this.transform.position, closest.transform.position)) {
						closest = enemy;
					}
				}
					setTarget(closest, 90);
			}
			
			List<GameObject> enemyUnitsInGroupRange = new List<GameObject>();
			foreach(GameObject enemy in unitGroup.enemiesInGroupRange) {
				//if(enemy.tag != gameObject.tag) {
					enemyUnitsInRange.Add(enemy);
				//}
			}
			if(enemyUnitsInGroupRange.Count > 0) {
				GameObject closestInGroup = enemyUnitsInGroupRange[0];
				foreach(GameObject enemy in enemyUnitsInGroupRange) {
					if(enemy != null) {
						UnitBase enemyUnitScript = enemy.GetComponent<UnitBase>();
						if(enemyUnitScript) {
							if(enemyUnitScript.unitCombatTarget== this) {
								setTarget(enemy, 80);
								return;
							}
						}
						
							if(Vector3.Distance(this.transform.position, enemy.transform.position) <= 
						   Vector3.Distance(this.transform.position, closestInGroup.transform.position)) {
							closestInGroup = enemy;
						}
					}
				}
				setTarget(closestInGroup, 70);
				return;
			}
		}
	}

	virtual public void setTarget(GameObject target, int priority) {
		if(unitCombatTarget != null) {
			unitCombatTarget.SendMessage("removeUnitTargetingMe", this, SendMessageOptions.DontRequireReceiver);
		}
		CancelInvoke("attackTarget");

		if(target == null) {
			unitTargetPriority = priority;
			unitCombatTarget = null;
		}
		else if(priority > unitTargetPriority) {
			unitTargetPriority = priority;
			unitCombatTarget = target;
			unitCommand = 2;
			//Debug.Log(gameObject.name + " changes target to " + target.name + " priority " + priority);
			unitCombatTarget.SendMessage("addUnitTargetingMe", this, SendMessageOptions.DontRequireReceiver);
			if(unitCombatTarget.tag.Contains(vars.unit_tag)) {
				enemyUnitRadius = unitCombatTarget.GetComponent<NavMeshAgent>().radius;
			}
		}
	}




	virtual public void onDisable() {
		isUnitDisabled = true;
	}

	virtual public void addEnemyInRange(GameObject enemy) {
		enemiesInRange.Add(enemy.gameObject);
		if(unitGroup != null) {
			//Debug.Log(gameObject.tag + " adds " + enemy.gameObject.tag + " to group " + unitGroup.tag);

			unitGroup.addEnemyToRange(enemy.gameObject);
		}
	}
	
	virtual public void removeEnemyInRange(GameObject enemy) {
		enemiesInRange.Remove(enemy);
		if(unitGroup != null) {
			//Debug.Log(gameObject.tag + " removes " + enemy.gameObject.tag);
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

		for(int i = unitsTargetingMe.Count - 1; i >= 0; i--) {
			if (unitsTargetingMe[i] == null)
			{
				unitsTargetingMe.RemoveAt(i);
			}
		}
	}
}
