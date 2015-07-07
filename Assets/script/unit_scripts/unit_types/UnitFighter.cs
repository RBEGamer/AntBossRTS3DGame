using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class UnitFighter : UnitBase, ISelectableBase {

	public float spreadDistanceInGroup = 5.0f;

	// rare idle animation
	private float idleRareChance = 20;
	private float currentIdleRareCooldown = 5.0f;
	private float idleRareCooldown = 5.0f;

	public float currentIdleTime = 0.0f;
	public float upperIdleTime = 10.0f;
	public float lowerIdleTime = 6.0f;

	private bool isNearDefensePoint = false;


	void Start() {
		setAttributesFromGroup();
	}
		
	// Update is called once per frame
	void Update () {
		/*
		if(uiManager.is_in_menu) {
			unitNavMeshAgent.velocity = Vector3.zero;
			unitNavMeshAgent.ResetPath();
			unitAnimator.speed = 0;
			return;
		}*/

		checkInRange();

		if(!unitCombatTarget) {
			unitTargetPriority = 0;
		}
		if(unitCurrentHealth < unitBaseHealth) {
			unitCurrentHealth += Time.deltaTime * unitCurrentRegeneration;
		}

		if(isInPanic) {
			// update destination if changed
			if (unitNavMeshAgent.destination != unitMovementTarget && !isUnitDisabled)
			{
				unitNavMeshAgent.SetDestination(unitMovementTarget);
			}

			if (!unitNavMeshAgent.pathPending)
			{
				if (unitNavMeshAgent.remainingDistance <= unitNavMeshAgent.stoppingDistance)
				{
					if (!unitNavMeshAgent.hasPath || unitNavMeshAgent.velocity.sqrMagnitude == 0f)
					{
						// Done 
						
						foreach (Transform child in transform)
						{
							GameObject.Destroy(child.gameObject);
						}

						unitGroup.removeUnit(this);
						Destroy(gameObject);
						isUnitDisabled = true;
					}
				}
			}
			return;
		}	
		if(unitCurrentCombatCooldown > 0) {
			unitCurrentCombatCooldown -= Time.deltaTime;
			return;
		}
		// -1: attack move to defense point
		if (unitCommand == -1)
		{
			unitAnimator.SetBool("isrunning", true);
			analyseUnitsInRange();
			if (followTarget != null)
			{
				unitMovementTarget = followTarget.transform.position;
			}
		}
		
		// move to defense point, ignore enemies
		if (unitCommand == 1)
		{
			unitAnimator.SetBool("isrunning", true);
			if (followTarget != null)
			{	
				unitMovementTarget = followTarget.transform.position;
			}
		}
		
		// attack target directly
		if (unitCommand == 2) {
			analyseUnitsInRange();
			unitAnimator.SetBool("isrunning", true);
			if(unitCombatTarget != null) {
				unitMovementTarget = unitCombatTarget.transform.position;
				
				if (!unitNavMeshAgent.pathPending)
				{
					if (unitNavMeshAgent.remainingDistance <= unitNavMeshAgent.stoppingDistance)
					{
						if (!unitNavMeshAgent.hasPath || unitNavMeshAgent.velocity.sqrMagnitude == 0f)
						{
							if(Vector3.Distance(this.transform.position, unitCombatTarget.transform.position) <= unitCurrentAttackRange) {
								unitMovementTarget = this.transform.position;
								if(unitCurrentCombatCooldown <= 0) {
									unitAnimator.speed = 1/unitCurrentAttackspeed;
									unitAnimator.SetTrigger("doattack");
									unitCurrentCombatCooldown = unitCurrentAttackspeed;
								}
							}
						}
					}
				}
				
			}
			else {	
				unitCommand = 0;
				unitTargetPriority = 0;
				getNewIdlePosition(unitGroup.transform.position, spreadDistanceInGroup);
			}
		}
		
		// idle
		if (unitCommand == 0)
		{
			unitAnimator.speed = 1;
			unitAnimator.SetBool("isrunning", true);
			analyseUnitsInRange();
			if (!unitNavMeshAgent.pathPending)
			{
				if (unitNavMeshAgent.remainingDistance <= unitNavMeshAgent.stoppingDistance)
				{
					if (!unitNavMeshAgent.hasPath || unitNavMeshAgent.velocity.sqrMagnitude == 0f)
					{
						// Done 
						
						if(currentIdleTime <= 0.0f) 
						{
							unitAnimator.SetBool("isrunning", false);
							currentIdleRareCooldown = idleRareCooldown;
							getNewIdlePosition(unitGroup.transform.position, spreadDistanceInGroup);
							currentIdleTime = Random.Range (lowerIdleTime, upperIdleTime);
						} 
						else 
						{
							currentIdleRareCooldown -= Time.deltaTime;
							if(currentIdleRareCooldown <= 0) {
								if(Random.Range (0, 100) <= idleRareChance) {
									unitAnimator.SetTrigger("idlerare");
									currentIdleRareCooldown = idleRareCooldown;
								}
							}
							unitAnimator.SetBool("isrunning", false);
							currentIdleTime -= Time.deltaTime;
						}
					}
				}
			}
		}

		// update destination if changed
		if (unitNavMeshAgent.destination != unitMovementTarget && !isUnitDisabled)
		{
			unitNavMeshAgent.SetDestination(unitMovementTarget);
		}
	}


	// helper functions
	public void getNewIdlePosition(Vector3 pivot, float distance)
	{
		unitMovementTarget = new Vector3(pivot.x + Random.Range(-distance, distance) + 1.0f,
		                             transform.position.y,
		                             pivot.z + Random.Range(-distance, distance) + 1);
	}

	bool checkInRange() {
		if(Vector3.Distance(this.transform.position, unitGroup.transform.position) < spreadDistanceInGroup && isNearDefensePoint == false) {
			isNearDefensePoint = true;
			if(unitCommand != 2) {
				followTarget = null;
				getNewIdlePosition(unitGroup.transform.position, spreadDistanceInGroup);
				unitCommand = 0;
			}
		} else if(isNearDefensePoint == true && Vector3.Distance(this.transform.position, unitGroup.transform.position) > spreadDistanceInGroup){
			isNearDefensePoint = false;
			currentIdleTime = 0.0f;
		}
		
		return isNearDefensePoint;
	}

	public void attackTarget() {
		dealDamage();
	}
	
	public void OnSelected() { unitGroup.SendMessage("OnSelected", SendMessageOptions.DontRequireReceiver); }
	
	public void OnUnselected() { }
	
	public bool isSelected() { return false; }
	
	public string getName() { return "";}
}
