using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public struct SavedUnitGroup {
	public int numUnits;
	public string unitGroupName;

	// Upgrade logic(friendly units only)
	public List<string> normalUpgrades;
	public string specialUpgrade;
}

public class UnitGroupCache : MonoBehaviour {
	public UpgradeManager upgradeManager;

	public GameObject unitGroupPrefab;
	public GameObject unitPrefab;
	public GameObject testPrefab;
	
	public List<SavedUnitGroup> unitGroupsSaved;
	public static int unitGroupCounter = 1;

	// Use this for initialization
	void Start () {
		unitGroupsSaved = new List<SavedUnitGroup>();
	}

	// creates new(empty) group
	public SavedUnitGroup createNewGroup() {
		SavedUnitGroup newUnitGroup = new SavedUnitGroup();
		newUnitGroup.unitGroupName = "Standard Ants " + unitGroupCounter.ToString();
		unitGroupCounter++;
		newUnitGroup.normalUpgrades = new List<string>();
		//newUnitGroup.normalUpgrades.Add("Heal");
		newUnitGroup.normalUpgrades.Add("Moral");
		unitGroupsSaved.Add (newUnitGroup);
		
		return newUnitGroup;
	}
	
	// deletes existing group
	// returns num units
	public int deleteUnitGroup(SavedUnitGroup group) {
		for(int i = unitGroupsSaved.Count - 1; i >= 0; i--) {
			if (unitGroupsSaved[i].unitGroupName  == group.unitGroupName)
			{
				unitGroupsSaved.RemoveAt(i);
			}
		}
		return group.numUnits;
	}
	
	// deletes a unit from a group
	public void deleteUnitFromGroup(SavedUnitGroup group) {
		if(group.numUnits > 0) {
			group.numUnits--;
		}
	}
	
	// adds a unit group(on retreat)
	public void addUnitGroup(UnitGroupScript group) {
		SavedUnitGroup newUnitGroup = new SavedUnitGroup();
		
		newUnitGroup.numUnits = group.unitsInBaseScripts.Count;
		unitGroupsSaved.Add (newUnitGroup);
	}
	
	// spawns a waiting unit group
	public void spawnUnitgroup(SavedUnitGroup group) {
		Vector3 position = new Vector3(transform.position.x - 5.0f, transform.position.y, transform.position.z);
		GameObject newgroup = Instantiate(unitGroupPrefab, position, Quaternion.identity) as GameObject;
		UnitGroupScript newUnitGroupScript = newgroup.GetComponent<UnitGroupFriendlyScript>();

		GameObject newUnit;
		List <UnitScript> listNewUnits = new List<UnitScript>();
		for(int i = 0; i < group.numUnits; i++) {
			newUnit = Instantiate(unitPrefab, position, Quaternion.identity) as GameObject;
			UnitScript unitScript = newUnit.GetComponent<UnitScript>();
			unitScript.unitGroupScript = newUnitGroupScript;
			position = new Vector3(0,0,0);
			listNewUnits.Add(unitScript);
		}
		GameObject newUpgrade;
		Skill newUpgradeSkill;

		foreach(string upgrade in group.normalUpgrades) {
			newUpgrade = Instantiate(upgradeManager.getUpgrade(upgrade), position, Quaternion.identity) as GameObject;
			newUpgradeSkill = newUpgrade.GetComponent<Skill>();
			if(newUpgradeSkill.skillType == 1) {
				newUpgrade.SetActive(true);
				newUpgradeSkill.unitGroupScript = newUnitGroupScript;
				newUpgrade.transform.parent = newgroup.transform;
			}
			if(newUpgradeSkill.skillType == 0 && listNewUnits.Count > 0) {
				newUpgradeSkill.unitScript = listNewUnits[0];
				newUpgrade.transform.parent = listNewUnits[0].transform;
				newUpgrade.SetActive(true);
				for(int i = 1; i < listNewUnits.Count; i++) {
					newUpgrade = Instantiate(upgradeManager.getUpgrade(upgrade), position, Quaternion.identity) as GameObject;
					newUpgrade.SetActive(true);
					newUpgradeSkill = newUpgrade.GetComponent<Skill>();
					newUpgradeSkill.unitScript = listNewUnits[i];
					newUpgrade.transform.parent = listNewUnits[i].transform;
				}
			}
		}


		deleteUnitGroup(group);

	}

	public void addUpgrade(string upgradeName, SavedUnitGroup group, bool type) {
		if(type) {
			if(group.normalUpgrades.Count < 4) {
				group.normalUpgrades.Add(upgradeName);
			}
		} else if(!type) {
			if(group.specialUpgrade == null) {
				group.specialUpgrade = upgradeName;
			}
		}
	}
}