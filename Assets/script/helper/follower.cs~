using UnityEngine;
using System.Collections;

public class follower : MonoBehaviour {



	public Vector3 offeset;
	public GameObject move_to;

	public bool en_move;
	public bool en_lerp;
	public float lerp_speed;


	public bool exclude_y;
	public float height;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	

		if(exclude_y){

		if(move_to){
			if(en_lerp){
					this.transform.position = Vector3.Lerp(this.transform.position ,move_to.transform.position, lerp_speed*Time.deltaTime)+offeset;
			}else{
					this.transform.position = move_to.transform.position+offeset;
			}
		}

		}else{

			if(move_to){
				Vector2 tmp ;
				if(en_lerp){
					 tmp = Vector2.Lerp(new Vector2(this.transform.position.x,this.transform.position.z) , new Vector2(move_to.transform.position.x,move_to.transform.position.z), lerp_speed*Time.deltaTime);
					this.transform.position =  new Vector3(tmp.x, height, tmp.y)+offeset;
				}else{


					this.transform.position =  new Vector3(move_to.transform.position.x,height, move_to.transform.position.z)+offeset;
				}
			}

		}
	}
}
