using UnityEngine;
using System.Collections;

public class CastUnitInRange : MonoBehaviour {
	public Skill skill;

	public UnitFaction targetFaction;
	public string unitFlag;
	public bool includeOwnUnits = false;


	public bool castOnce = false;
	public bool casted = false;
	public int currentUnitsInRange = 0;

	// Use this for initialization
	void Start () {
		skill = GetComponent<Skill>();
	}
	
	// Update is called once per frame
	void Update () {
		int unitsFoundInRange = skill.unitScript.unitGroupScript.getNumUnitsInRangeByFlag(unitFlag, targetFaction, includeOwnUnits);
		Debug.Log (unitsFoundInRange + " YOLO");
		//skill.unitScript.unitGroupScript.
		if(!castOnce) {
			if(unitsFoundInRange > currentUnitsInRange) {
				for(int i = currentUnitsInRange; i < unitsFoundInRange; i++) {
					SendMessage("OnFire", SendMessageOptions.DontRequireReceiver);
				}
				currentUnitsInRange = unitsFoundInRange;
			} else if(unitsFoundInRange < currentUnitsInRange){
				for(int i = currentUnitsInRange; i > unitsFoundInRange; i--) {
					SendMessage("OnUnFire", SendMessageOptions.DontRequireReceiver);
				}
				currentUnitsInRange = unitsFoundInRange;
			}
		}
	 	else {
			currentUnitsInRange = unitsFoundInRange;
			if(currentUnitsInRange > 0 && !casted) {
				SendMessage("OnFire", SendMessageOptions.DontRequireReceiver);
				casted = true;
			} else if(currentUnitsInRange == 0 && casted) {
				SendMessage("OnUnFire", SendMessageOptions.DontRequireReceiver);
				casted = false;
			}
		}
	}
}
