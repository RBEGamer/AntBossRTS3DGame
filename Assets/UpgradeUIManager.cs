using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UpgradeUIManager : MonoBehaviour {
	public static int numResearchedTiers = 1;
	public static int numUpgradesOnCurrentTier = 0;

	public GameObject selectedSlot;
	public int selectedTier;
	public GameObject selectedUpgrade;
	public GameObject upgradeTarget;

	public List<GameObject> tierUIs;
	public UpgradeManager upgradeManager;

	public Text upgradeHeadlineText;
	public Text upgradeDescription;
	public Text researchButtonText;
	public Text equipButtonText;


	private static base_manager baseManager;
	private static ui_manager uiManager;

	public void Start() {
		selectTier(1);
		if(!baseManager) {
			baseManager = GameObject.Find (vars.base_name).GetComponent<base_manager>();
		}

		if(!uiManager) {
			uiManager = GameObject.Find (vars.ui_manager_name).GetComponent<ui_manager>();
		}
	}

	public void selectTier(int selectTier) {
		if(upgradeManager.getUpgradeListOfTier(selectTier).Count == 0) {
			upgradeDescription.text = "No upgrades on tier " + selectTier + " available.";
		} else {
			upgradeDescription.text = "Choose the upgrade you wish to research or equip.";
		}
		selectedUpgrade = null;
		upgradeHeadlineText.text = "UPGRADE TIER " + selectTier;
		researchButtonText.text = "Select an upgrade first!";
		equipButtonText.text = "Select an upgrade first!";;
		for(int i = 0; i < tierUIs.Count; i++) {
			if(selectTier-1 == i) {
				tierUIs[i].SetActive(true);

			} else {
				tierUIs[i].SetActive(false);

			}
		}
	}

	public void selectUpgrade(string upgradeToSelect) {
		selectedUpgrade = upgradeManager.getUpgrade(upgradeToSelect);
		upgradeHeadlineText.text = selectedUpgrade.name;

		Skill upradeSkill = selectedUpgrade.GetComponent<Skill>();
		upgradeDescription.text = upradeSkill.desc;
		researchButtonText.text = "Research: "+ upradeSkill.costA_research+ " / " + upradeSkill.costB_research;
		equipButtonText.text = "Equip: "+ upradeSkill.costA_equip+ " / " + upradeSkill.costB_equip;
	}
	

	public void researchUpgrade()  {
		if(selectedUpgrade) {
			if(baseManager.res_a_storage >= selectedUpgrade.GetComponent<Skill>().costA_research &&
			   baseManager.res_b_storage >= selectedUpgrade.GetComponent<Skill>().costB_research) {

				Skill currentSkill = selectedUpgrade.GetComponent<Skill>();
				if(numResearchedTiers >= currentSkill.skillTier && currentSkill.researched == false) {
					baseManager.res_a_storage -= currentSkill.costA_research;
					baseManager.res_b_storage -= currentSkill.costB_research;

					currentSkill.researched = true;
					numUpgradesOnCurrentTier++;
					if(numUpgradesOnCurrentTier == 2) {
						numResearchedTiers++;
						numUpgradesOnCurrentTier = 0;
					}
				}
			}
		}
	}

	public void equipUpgrade() {
		if(selectedUpgrade) {
			if(baseManager.res_a_storage >= selectedUpgrade.GetComponent<Skill>().costA_equip &&
			   baseManager.res_b_storage >= selectedUpgrade.GetComponent<Skill>().costB_equip && 
			   selectedUpgrade.GetComponent<Skill>().researched) {
				switch(uiManager.ui_view_slot_0) {
				case ui_manager.selected_ui_in_slot_0.base_ui:
					addUpgradeToBase();
					break;
				case ui_manager.selected_ui_in_slot_0.ressource_ui:
					addUpgradeToRessource();
					break;
				case ui_manager.selected_ui_in_slot_0.unit_ui:
					addUpgradeToUnit();
					break;
				case ui_manager.selected_ui_in_slot_0.waypoint_ui:
					addUpgradeToWaypoint();
					break;
				}
			}
		}
	}


	public void addUpgradeToBase() {
		GameObject newUpgrade;
		GameObject parentObject;
		Skill newSkill;
		parentObject = baseManager.gameObject;

		newUpgrade = (GameObject)Instantiate(selectedUpgrade, parentObject.transform.position, Quaternion.identity);
		newSkill = newUpgrade.GetComponent<Skill>();
		newSkill.skillTarget = parentObject;
		newUpgrade.transform.parent = parentObject.transform;
		newUpgrade.SetActive(true);

		Skill currentSkill = selectedUpgrade.GetComponent<Skill>();
		baseManager.res_a_storage -= currentSkill.costA_equip;
		baseManager.res_b_storage -= currentSkill.costB_equip;
	}

	public void addUpgradeToRessource() {
		GameObject newUpgrade;
		GameObject parentObject;
		Skill newSkill;
		parentObject = uiManager.wpManager.getResObjectById(uiManager.connected_wp_id).gameObject;
		
		newUpgrade = (GameObject)Instantiate(selectedUpgrade, parentObject.transform.position, Quaternion.identity);
		newSkill = newUpgrade.GetComponent<Skill>();
		newSkill.skillTarget = parentObject;
		newUpgrade.transform.parent = parentObject.transform;
		newUpgrade.SetActive(true);

		Skill currentSkill = selectedUpgrade.GetComponent<Skill>();
		baseManager.res_a_storage -= currentSkill.costA_equip;
		baseManager.res_b_storage -= currentSkill.costB_equip;
	}

	public void addUpgradeToWaypoint() {
		GameObject newUpgrade;
		GameObject parentObject;
		Skill newSkill;
		parentObject = uiManager.wpManager.getNodeObjectById(uiManager.connected_wp_id).gameObject;
		
		newUpgrade = (GameObject)Instantiate(selectedUpgrade, parentObject.transform.position, Quaternion.identity);
		newSkill = newUpgrade.GetComponent<Skill>();
		newSkill.skillTarget = parentObject;
		newUpgrade.transform.parent = parentObject.transform;
		newUpgrade.SetActive(true);

		Skill currentSkill = selectedUpgrade.GetComponent<Skill>();
		baseManager.res_a_storage -= currentSkill.costA_equip;
		baseManager.res_b_storage -= currentSkill.costB_equip;
	}

	public void addUpgradeToUnit() {
		GameObject newUpgrade;
		GameObject parentObject;
		Skill newSkill;

		newUpgrade = (GameObject)Instantiate(selectedUpgrade, Vector3.zero, Quaternion.identity);
		newSkill = newUpgrade.GetComponent<Skill>();
		newUpgrade.name = newUpgrade.name.Replace("(Clone)", ""); 
		if(newSkill.specialNormal && uiManager.sug.specialUpgrade == "") {
			uiManager.sug.specialUpgrade = newUpgrade.name;
		} else if(!newSkill.specialNormal && uiManager.sug.normalUpgrades.Count < 4 && !uiManager.sug.normalUpgrades.Contains(selectedUpgrade.name)) {
			uiManager.sug.normalUpgrades.Add(newUpgrade.name);
		}

		Skill currentSkill = selectedUpgrade.GetComponent<Skill>();
		baseManager.res_a_storage -= currentSkill.costA_equip;
		baseManager.res_b_storage -= currentSkill.costB_equip;
		Destroy(newUpgrade);

	}

}
