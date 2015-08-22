using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour {
	public List<GameObject> listUpgrades;
	public List<Skill> listUpgradeSkills;
	public List<string> listUpgradesNames;

	public int numSkills = 10;
	// Use this for initialization
	void Start () {
		Skill[] skillObjects = new Skill[numSkills];
		skillObjects = GetComponentsInChildren<Skill>(true);
		foreach (Skill skill in skillObjects)
		{
			listUpgrades.Add (skill.gameObject);
			listUpgradesNames.Add(skill.gameObject.name);
			listUpgradeSkills.Add (skill);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject getUpgrade(string upgradeName) {
		for(int i = 0; i < listUpgradesNames.Count; i++) {
			if(upgradeName == listUpgradesNames[i]) {
				return listUpgrades[i];
			}
		}
		return null;
	}


	public List<Skill> getUpgradeListOfTier(int tier) {
		List<Skill> resultList = new List<Skill>();
		for(int i = 0; i < listUpgradeSkills.Count; i++) {
			if(listUpgradeSkills[i].skillTier == tier) {
				resultList.Add (listUpgradeSkills[i]);
			}
		}

		return resultList;
	}
}
