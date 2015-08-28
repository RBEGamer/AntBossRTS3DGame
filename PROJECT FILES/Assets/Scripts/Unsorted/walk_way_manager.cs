using UnityEngine;
using System.Collections;

public class walk_way_manager : MonoBehaviour {


	private Texture2D btest;
	public Texture2D way_ground_texture;

	public int overlay_texture_w = 1024,overlay_texture_h = 1024;

	public float min_trans = 0.0f;
	public float max_trans = 0.6f;

	

	public Texture2D brush_texture;


	// Use this for initialization
	void Awake () {
		this.name = vars.walk_way_manager_name;


		btest = new Texture2D(1024, 1024);

		//DAS IN DEN LADESCREEN PACKEN

		for (int i = 0; i < btest.width; i++) {
			for (int j = 0; j < btest.height; j++) {
				btest.SetPixel(i,j, new Color(way_ground_texture.GetPixel(i,j).r, way_ground_texture.GetPixel(i,j).g, way_ground_texture.GetPixel(i,j).b,min_trans));
				
			}
		}
		btest.Apply();



	}



	public Texture2D get_init_text(){
		return btest;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
