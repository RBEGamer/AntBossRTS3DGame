using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour {
	// General object variables
	public MovementScript movementScript;
	public Animator animator;
	public AttributeScript attributeScript;
	public HealthScript healthScript;
	public GameObject healthBar;

	// General unit variables
	public UnitFaction currentFaction;
	public UnitCommand currentCommand;
	public float spreadDistance;

	// Behaviour variables
	public GameObject attackTarget;
	public GameObject groupFollowTarget;



	// rare idle animation
	public float idleRareChance = 20;
	public float idleRareCooldown = 5.0f;


	protected float currentIdleRareCooldown = 5.0f;
	private float currentIdleTime = 0.0f;
	private float upperIdleTime = 10.0f;
	private float lowerIdleTime = 6.0f;


	void Awake() {
		attributeScript = GetComponent<AttributeScript>();
		healthScript = GetComponent<HealthScript>();
		movementScript = GetComponent<MovementScript>();
		animator = GetComponent<Animator>();

		movementScript.UpdateDestination(Vector3.zero);
	}


	void Update() {
		handleCommands();
		setHealthVisual(healthScript.CurrentHealth / healthScript.BaseHealth);
	}

	public void handleCommands() {
		switch(currentCommand) {
			case UnitCommand.AttackMove: {
				AttackMove();
				break;
			}
			case UnitCommand.Move: {
				Move();
				break;
			}
			case UnitCommand.Attack: {
				Attack();
				break;
			}
			case UnitCommand.Idle: {
				Idle();
				break;
			}
		}
	}

	public void AttackMove() {

	}

	public virtual void Move() {

	}

	public void Attack() {

	}

	public void Idle() {
		if(movementScript.reachedDestination) {
			if(currentIdleTime <= 0.0f) 
			{
				animator.SetBool("isrunning", false);
				currentIdleRareCooldown = idleRareCooldown;
				movementScript.UpdateDestination(newIdlePosition(transform.position, spreadDistance));
				currentIdleTime = Random.Range (lowerIdleTime, upperIdleTime);
			} 
			else 
			{
				currentIdleRareCooldown -= Time.deltaTime;
				if(currentIdleRareCooldown <= 0) {
					if(Random.Range (0, 100) <= idleRareChance) {
						animator.SetTrigger("idlerare");
						currentIdleRareCooldown = idleRareCooldown;
					}
				}
				animator.SetBool("isrunning", false);
				currentIdleTime -= Time.deltaTime;
			}
		} else {
			animator.speed = 1;
			animator.SetBool("isrunning", true);
		}
	}
	
	public void setHealthVisual(float healthNormalized){
		healthBar.transform.localScale = new Vector3( healthNormalized,
		                                             healthBar.transform.localScale.y,
		                                             healthBar.transform.localScale.z);
	}

	// helper functions
	public Vector3 newIdlePosition(Vector3 pivot, float distance)
	{
		RaycastHit hit;
		int safetycounter = 0;
		Vector3 newTargetPosition = new Vector3(pivot.x + Random.Range(-distance, distance) + 1.0f,
		                                 transform.position.y,
		                                 pivot.z + Random.Range(-distance, distance) + 1);

		while(true) {
			Ray testRay = new Ray(pivot, (newTargetPosition - pivot));
			if(Physics.Raycast(testRay, out hit, spreadDistance, LayerMask.GetMask("Obstacle"))) {
				if(hit.collider != null) {
					Debug.DrawRay(pivot, (newTargetPosition - pivot), Color.magenta, 10.0f);
					newTargetPosition = new Vector3(pivot.x + Random.Range(-distance, distance) + 1.0f,
					                                 transform.position.y,
				 	                                 pivot.z + Random.Range(-distance, distance) + 1);
					Debug.Log("hit wall! getting new idle point");
				}
				else {
					Debug.DrawRay(pivot, (newTargetPosition - pivot), Color.green, 10.0f);
					break;
				}
			}
			else {
				Debug.DrawRay(pivot, (newTargetPosition - pivot), Color.green, 10.0f);
				break;
			}
		}

		return newTargetPosition;
	}
}
