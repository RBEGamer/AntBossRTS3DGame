using UnityEngine;
using System.Collections;

public enum UnitCommand {
	RetreatToBase = 2,
	Move = 3, 
	Attack = 4,
	AttackMove = 5,
	Idle = 6
}


public class UnitScript : MonoBehaviour {
	// General object scripts
	public Animator animator;
	public NavMeshAgent navMeshAgent;
	public MovementScript movementScript;
	public AttributeScript attributeScript;
	public HealthScript healthScript;
	public UnitVision unitVision;
	public UnitCommandHandler unitCommandHandler;
	public UnitTargetScript unitTargetScript;
	public FlagScript flagScript;
	public WeaponScript weaponScript;

	public GameObject healthBar;


	// General unit variables
	public UnitGroupScript unitGroupScript;
	public UnitCommand currentCommand;
	public GameObject currentTarget;

	public float spreadDistance;
	
	// rare idle animation
	public float idleRareChance = 20;
	public float idleRareCooldown = 5.0f;

	public float currentIdleRareCooldown = 5.0f;
	public float currentIdleTime = 0.0f;
	public float upperIdleTime = 10.0f;
	public float lowerIdleTime = 6.0f;
	

	void Start() {
		unitGroupScript.addUnit(this);
	}

	void FixedUpdate() {
		unitCommandHandler.HandleCommands();
		if(flagScript.Faction == UnitFaction.PlayerFaction) {
			setHealthVisual(healthScript.CurrentHealth / healthScript.BaseHealth);
		}
		currentTarget = unitCommandHandler.attackTarget;

		healthScript.regenerate(attributeScript.CurrentHealthRegeneration, attributeScript.CurrentShieldRegeneration);
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


	public void OnSelected() { unitGroupScript.SendMessage("OnSelected", SendMessageOptions.DontRequireReceiver); }
	
	public void OnUnselected() { }
	
	public bool isSelected() { return false; }
	
	public string getName() { return "";}
}
