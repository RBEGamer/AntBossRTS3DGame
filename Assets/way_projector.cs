using UnityEngine;
using System.Collections;

public class way_projector : MonoBehaviour {


	//neues material erstellen
	//neue alpha text erstellen
	//neue final text erstellen
	//final_text dem mat zuweisen

	//auf änderungen prüfen
	//neue alpha erstellen -> von vS nach vE mit brush
	//alpha+way_text verrrechnen 

	public Texture2D way_ground_texture;
	public Texture2D brush_texture;

	private Projector text_proj;
	private Material  projector_material;

	private Texture2D final_text;

	public float min_transparenz = 0.0f;
	public float max_transparenz = 0.5f;
	void Awake(){
		text_proj = this.gameObject.GetComponent<Projector>();
	}
	// Use this for initialization
	void Start () {

		final_text = new Texture2D(way_ground_texture.width, way_ground_texture.height);


		//setup mat
		projector_material = new Material(Shader.Find("Transparent/Diffuse")); //we nee an akpha channel so we must use a transparent shader
		projector_material.mainTexture = final_text; //assign the texture to the main_texture of the material
	//	projector_material.mainTextureScale = new Vector2(20.0f, 20.0f); // set the tiling of the new created material to the thiling of the main texture
		//assin mat to projecotr
		text_proj.material = projector_material; // assign the new material to the projector

		clear_brushed_path(); // clear the texture...

	

		//-> steps von s nach z ausrechnen


	





	}


	//clear()
	public void clear_brushed_path(){

		//DAS IN DEN LADESCREEN PACKEN
		
		for (int i = 0; i < final_text.width; i++) {
			for (int j = 0; j < final_text.height; j++) {
				final_text.SetPixel(i,j, new Color(way_ground_texture.GetPixel(i,j).r, way_ground_texture.GetPixel(i,j).g, way_ground_texture.GetPixel(i,j).b,min_transparenz));
				
			}
		}
		final_text.Apply();

	}

	public void draw_path(Vector3 pos){

		int x = 0;
		int y = 0;

		RaycastHit hit;
		if (Physics.Raycast(pos, -Vector3.up, out hit, 100.0F)){
			//Debug.Log(hit.collider.name);
		
			x = (int)(hit.textureCoord.x * final_text.width);
			y = (int)(hit.textureCoord.y * final_text.height);



			//je nach laufrichtung noch anpassen
			x -=10; // die correction damit die ameise genua auf dm brush läuft

		for (int k = 0; k < brush_texture.width; k++) {
			for (int l = 0; l < brush_texture.height; l++) {
				float t = brush_texture.GetPixel(k,l).r*max_transparenz;

				
			//	if((l+y) <= 0 || (k+x) <= 0 || (l+y) >= final_text.width || (l+y) >= final_text.height){

					//schauen ob da doch was ist und das nicht mit dem brus überschreiben

					if(t < final_text.GetPixel(k+x,l+y).a){
						t = final_text.GetPixel(k+x,l+y).a;
					}

					final_text.SetPixel(k+x,l+y, new Color(way_ground_texture.GetPixel(k+x,l+y).r, way_ground_texture.GetPixel(k+x,l+y).g, way_ground_texture.GetPixel(k,l).b,t));
			//	}else{
			//	}


				//brushdraufrechnen
				
			}
		}	
		
		final_text.Apply();
		}//ende raycast
	}
	// Update is called once per frame
	void Update () {
	
	}
}
