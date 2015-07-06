using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class objectives_manager : MonoBehaviour {



	public GameObject objective_holder_0;
	public GameObject objective_holder_1;
	public GameObject objective_holder_2;

	public List<objective_desc> objectives = new List<objective_desc>();

	//public SortedList<objective_desc> sorted_objectives;
	// Use this for initialization
	void Start () {
		this.name = vars.objective_manager_name;

		refresh_obj_list();

		}

		//LISTE MIT ALLEN OBJECTIVE MIT DIESEM LEVEL ANLEGEN AUF REIHEN FLOGE SORTIEREN


	public void add_objective(objective_desc desc){
		if(!objectives.Contains(desc)){
		objectives.Add(desc);
	}
	}


	public void refresh_obj_list(){

		objective_desc desc;
		foreach (GameObject godesc in GameObject.FindGameObjectsWithTag(vars.objective_tag_name)) {
			desc = godesc.GetComponent<objective_desc>();
			if(!desc.finished && !objectives.Contains(desc)){
				objectives.Add(desc);
			}
			
		}






	


	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		//ALLE FERTGEN OBJ LÖSCHEN und das nächste auf active setzten
		//das aktuelle überwachen
		//die nächsten 3  in die liste eintragen


		for (int i = 0; i < objectives.Count; i++) {








		}






	}
}
