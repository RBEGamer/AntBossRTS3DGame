using UnityEngine;
using System.Collections;

public class base_manager : MonoBehaviour {

	public float res_a_storage;
	public float res_b_storage;
	public float res_c_storage;


	// Use this for initialization
	void Start () {
		this.name = vars.base_name;
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void add_to_storage(vars.ressource_type _type, float _amount){
		switch (_type) {
			case vars.ressource_type.A:
			res_a_storage += _amount;
			break;

		case vars.ressource_type.B:
			res_b_storage += _amount;
			break;

		case vars.ressource_type.C:
			res_c_storage += _amount;
			break;
		default:
			break;
		}
	}
}
