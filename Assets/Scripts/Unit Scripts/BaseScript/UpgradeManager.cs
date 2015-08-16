using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour {
	public List<GameObject> listUpgrades;
	public List<string> listUpgradesNames;

	// Use this for initialization
	void Start () {
		Skill[] skillObjects = new Skill[10];
		skillObjects = GetComponentsInChildren<Skill>(true);
		foreach (Skill skill in skillObjects)
		{
			listUpgrades.Add (skill.gameObject);
			listUpgradesNames.Add(skill.gameObject.name);
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
}
