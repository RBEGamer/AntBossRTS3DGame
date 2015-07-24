using UnityEngine;
using System.Collections;

public class FogOfWarRenderer : MonoBehaviour
{
    [SerializeField]
    static FogOfWar fogOfWar;
	Renderer[] thisRenderer;
	void Start() {
		thisRenderer = GetComponentsInChildren<Renderer>();
		if(!fogOfWar) {
			fogOfWar = GameObject.FindGameObjectWithTag(vars.fogofwar_tag).GetComponent<FogOfWar>();
		}
	}

    void Update()
    {
		if(fogOfWar != null) {
			if (fogOfWar != null && fogOfWar.enabled && fogOfWar.gameObject.active)
	        {
				for(int i = 0; i < thisRenderer.Length; i++) {
					thisRenderer[i].enabled = fogOfWar.IsRevealed(transform.position);

				}
			}
		}

    }
}