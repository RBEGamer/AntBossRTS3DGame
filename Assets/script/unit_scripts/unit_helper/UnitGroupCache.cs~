using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct SavedUnitGroup {
	public Vector3 position;
	public string unitGroupName;
	
	public int numUnits;
	public int[] unitHealths;
	
	public float attackspeed;
	
	public float damage;
	public float health;
	public float movementspeed;
	
	public float attackrange;
	public float visionRange;

	public float regeneration;

	// Upgrade logic(friendly units only)
	public upgrade_description upgrade_slot_0;
	public upgrade_description upgrade_slot_1;
	public upgrade_description upgrade_slot_2;

	public void initialize(float _attackspeed, float _movementspeed, float _damage, float _health, float _attackrange, float _visionrange) {
		position = Vector3.zero;
		
		attackspeed = _attackspeed;
		movementspeed = _movementspeed;
		
		damage = _damage;
		health = _health;
		
		attackrange = _attackrange;
		visionRange = _visionrange;
		
		numUnits = 0;
	}
}


public class UnitGroupCache : MonoBehaviour {
	
	public GameObject unitGroupPrefab;
	public GameObject unitPrefab;
	
	public List<SavedUnitGroup> unitGroupsSaved;
	public static int unitGroupCounter = 1;

	public float standardAttackspeed;
	
	public float standardDamage;
	public float standardHealth;
	public float standardMovementspeed;
	
	public float standardAttackrange;
	public float standardVisionRange;

	public float standardRegeneration;

	// Use this for initialization
	void Start () {
		unitGroupsSaved = new List<SavedUnitGroup>();

		UnitGroupBase t = unitGroupPrefab.GetComponent<UnitGroupBase>();
		standardAttackspeed = t.attackspeed;
		
		standardDamage = t.damage;
		standardHealth = t.health;
		standardMovementspeed = t.movementspeed;
		
		standardAttackrange = t.attackrange;
		standardVisionRange = t.visionRange;
	}
	
	
	void Update() {

		
		// TEST
		if(Input.GetKeyDown(KeyCode.H)) {
			spawnUnitgroup(unitGroupsSaved[unitGroupsSaved.Count-1]);
		}
		//







	}
	
	// creates new(empty) group
	public SavedUnitGroup createNewGroup() {
		SavedUnitGroup newUnitGroup = new SavedUnitGroup();
		newUnitGroup.initialize(standardAttackspeed, standardMovementspeed, standardDamage, standardHealth, standardAttackrange, standardVisionRange);
		newUnitGroup.unitGroupName = "Standard Ants " + unitGroupCounter.ToString();
		newUnitGroup.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
		unitGroupCounter++;
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
	public void addUnitGroup(UnitGroupBase group) {
		SavedUnitGroup newUnitGroup = new SavedUnitGroup();
		
		newUnitGroup.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
		newUnitGroup.unitGroupName = group.name;
		newUnitGroup.attackspeed = group.attackspeed;
		newUnitGroup.movementspeed = group.movementspeed;
		
		newUnitGroup.damage = group.damage;
		newUnitGroup.health = group.health;
		newUnitGroup.regeneration = group.regeneration;

		newUnitGroup.attackrange = group.attackrange;
		newUnitGroup.visionRange = group.visionRange;
		
		newUnitGroup.numUnits = group.numUnitsInGroup;
		unitGroupsSaved.Add (newUnitGroup);
	}
	
	// spawns a waiting unit group
	public void spawnUnitgroup(SavedUnitGroup group) {
		GameObject newgroup = Instantiate(unitGroupPrefab, group.position, Quaternion.identity) as GameObject;
		UnitGroupBase unitGroupBase = newgroup.GetComponent<UnitGroupBase>();
		unitGroupBase.setAttributes(group.unitGroupName, group.attackspeed, group.damage, group.health, group.movementspeed, group.attackrange, group.visionRange, group.regeneration);
		
		GameObject newunit;
		
		for(int i = 0; i < group.numUnits; i++) {
			newunit = Instantiate(unitPrefab, group.position, Quaternion.identity) as GameObject;
			UnitBase unitBase = newunit.GetComponent<UnitBase>();
			unitBase.setUnitGroup(unitGroupBase);
		}
		
		
		deleteUnitGroup(group);
	}


	public bool add_upgrade(ref upgrade_description upgrade, SavedUnitGroup group){
		Debug.Log("unit upgrade add");
		if(upgrade != null && upgrade.upgrade_type == vars.upgrade_type.units){
			Debug.Log("upgrade check");
			base_manager thisBase = GameObject.FindGameObjectWithTag(vars.base_tag+""+vars.friendly_tag+""+vars.attackable_tag).GetComponent<base_manager>();
			if(thisBase.res_a_storage >= upgrade.costs_res_a && thisBase.res_b_storage >= upgrade.costs_res_b && thisBase.res_c_storage >= upgrade.costs_res_c){
				Debug.Log("costs check");
				// remove resources from base
				thisBase.res_a_storage -= upgrade.costs_res_a;
				thisBase.res_b_storage -= upgrade.costs_res_b;
				thisBase.res_c_storage -= upgrade.costs_res_c;
				
				if(group.upgrade_slot_0 == null || group.upgrade_slot_1 == null || group.upgrade_slot_2 == null){
					switch (upgrade.upgrade_add_to_value) {
					case vars.upgrade_values.ant_life:
						group.health += upgrade.increase_value;
						break;

					case vars.upgrade_values.ant_damage:
						group.damage += upgrade.increase_value;
						break;

					case vars.upgrade_values.ant_speed:
						group.movementspeed += upgrade.increase_value;
						break;
					case vars.upgrade_values.ant_regeneration:
						group.regeneration += upgrade.increase_value;
						break;

					default:
						break;
					}

					if(group.upgrade_slot_0 == null){
						group.upgrade_slot_0 = upgrade;
						Debug.Log("set upgrade to slot 0");
						//GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().upgrade_unit_button_0.GetComponent<Image>().sprite = group.upgrade_slot_0.upgrade_icon;
					}else 	if(group.upgrade_slot_1 == null){
						group.upgrade_slot_1 = upgrade;
						Debug.Log("set upgrade to slot 1");
						//GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().upgrade_base_button_1.GetComponent<Image>().sprite = group.upgrade_slot_1.upgrade_icon;
					}else 	if(group.upgrade_slot_2 == null){
						group.upgrade_slot_2 = upgrade;
						Debug.Log("set upgrade to slot 2");
						//GameObject.Find(vars.ui_manager_name).GetComponent<ui_manager>().upgrade_base_button_2.GetComponent<Image>().sprite = group.upgrade_slot_2.upgrade_icon;
					}

					return true;
				}
			}else{
				Debug.Log("err 2");
			}
			
		}else{
			Debug.Log("err 1");
		}
		return false;
		
	}
}
