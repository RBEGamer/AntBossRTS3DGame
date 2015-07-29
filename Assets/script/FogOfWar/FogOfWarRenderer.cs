using UnityEngine;
using System.Collections;

public class FogOfWarRenderer : MonoBehaviour
{
    [SerializeField]
    static FogOfWar fogOfWar;
	public Renderer[] thisRenderer;
	public bool isVisible;
	void Start() {
		thisRenderer = GetComponentsInChildren<Renderer>();
		if(!fogOfWar) {
			fogOfWar = GameObject.FindGameObjectWithTag(vars.fogofwar_tag).GetComponent<FogOfWar>();
		}
	}

    void FixedUpdate()
    {

		if(fogOfWar != null) {
			if (fogOfWar != null && fogOfWar.enabled && fogOfWar.gameObject.active)
	        {
				Debug.Log (isVisible);
				for(int i = 0; i < thisRenderer.Length; i++) {
					thisRenderer[i].enabled = fogOfWar.IsRevealed(transform.position);
					isVisible = thisRenderer[i].enabled;

				}
			}
		}

    }
}