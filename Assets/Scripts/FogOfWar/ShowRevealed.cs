using UnityEngine;
using System.Collections;

public class ShowRevealed : MonoBehaviour
{
    [SerializeField]
    public static FogOfWar fogOfWar;


	public Renderer[] thisRenderer;
	public bool isVisible;

	void Awake() {
		thisRenderer = GetComponentsInChildren<Renderer>();
		if(!fogOfWar) {
			fogOfWar = GameObject.FindGameObjectWithTag(vars.fogofwar_tag).GetComponent<FogOfWar>();
		}

		for(int i = 0; i < thisRenderer.Length; i++) {
			thisRenderer[i].enabled = false;
		}
	}

    void Update()
    {
		if(fogOfWar != null) {
			if (fogOfWar != null && fogOfWar.enabled && fogOfWar.gameObject.active)
	        {
				for(int i = 0; i < thisRenderer.Length; i++) {
					thisRenderer[i].enabled = fogOfWar.IsRevealed(transform.position);
					isVisible = fogOfWar.IsRevealed(transform.position);
				}
			}
		}

    }
}