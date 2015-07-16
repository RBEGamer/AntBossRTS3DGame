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

public enum UnitCommand {
	AttackMove,
	Move,
	AttackDirectly,
	Idle
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
	public Renderer[] unitRenderer;
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

	// spread idle distance
	public float spreadDistanceInGroup = 5.0f;
	
	// rare idle animation
	protected float idleRareChance = 20;
	protected float currentIdleRareCooldown = 5.0f;
	protected float idleRareCooldown = 5.0f;
	
	public float currentIdleTime = 0.0f;
	public float upperIdleTime = 10.0f;
	public float lowerIdleTime = 6.0f;
	public GameObject healthBar;
	
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
	public UnitCommand unitCommand;

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
		unitRenderer = GetComponentsInChildren<Renderer>();
		if(gameObject.tag.Contains(vars.enemy_tag)) {
			//setRenderer(false);
		}

		uiManager = GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>();
		unitNavMeshAgent = GetComponent<NavMeshAgent>();
		unitAnimator = GetComponent<Animator>();
		if(unitGroup != null) {
			setAttributesFromGroup();
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

	public void setRenderer(bool status) {
		if(unitRenderer[0].enabled != status) {
			foreach(Renderer r in unitRenderer) {
				r.enabled = status;
			}
		}
	}

	public bool getRenderer() {
		return unitRenderer[0].enabled;
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
		unitCurrentRegeneration = unitBaseRegeneration;
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
				enemyUnitsInGroupRange.Add(enemy);
				//Debug.Log ("ADDING UNIT");
				//}
			}
			if(enemyUnitsInGroupRange.Count > 0) {
				//Debug.Log ("CHECKING UNITS IN GROUP RANGE)");
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
			unitCommand = UnitCommand.AttackDirectly;
			//Debug.Log(gameObject.name + " changes target to " + target.name + " priority " + priority);

			unitCombatTarget.SendMessage("addUnitTargetingMe", this, SendMessageOptions.DontRequireReceiver);

			//if(unitCombatTarget.tag.Contains(vars.unit_tag)) {
				enemyUnitRadius = unitCombatTarget.GetComponent<NavMeshAgent>().radius;
			//}
		}
	}

	// helper functions
	virtual public void getNewIdlePosition(Vector3 pivot, float distance)
	{
		RaycastHit hit;
		int safetycounter = 0;
		unitMovementTarget = new Vector3(pivot.x + Random.Range(-distance, distance) + 1.0f,
		                                 transform.position.y,
		                                 pivot.z + Random.Range(-distance, distance) + 1);
		while(true) {
			Ray testRay = new Ray(pivot, (unitMovementTarget - pivot));
			if(Physics.Raycast(testRay, out hit, spreadDistanceInGroup, LayerMask.GetMask("Obstacle"))) {
				if(hit.collider != null) {
					Debug.DrawRay(pivot, (unitMovementTarget - pivot), Color.magenta, 10.0f);
					unitMovementTarget = new Vector3(pivot.x + Random.Range(-distance, distance) + 1.0f,
					                                 transform.position.y,
					                                 pivot.z + Random.Range(-distance, distance) + 1);
					Debug.Log("hit wall! getting new idle point");
				}
				else {
					Debug.DrawRay(pivot, (unitMovementTarget - pivot), Color.green, 10.0f);
					break;
				}
			}
			else {
				Debug.DrawRay(pivot, (unitMovementTarget - pivot), Color.green, 10.0f);
				break;
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
		if(!unitCombatTarget) {
			unitTargetPriority = 0;
		}

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
