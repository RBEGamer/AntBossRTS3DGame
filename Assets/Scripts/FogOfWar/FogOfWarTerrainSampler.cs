using UnityEngine;

public class FogOfWarTerrainSampler : FogOfWarHeightSampler
{
    [SerializeField]
    Terrain terrain = null;

    public override float SampleHeight(Vector3 worldPosition)
    {
        if (terrain != null)
        {
			if(terrain.SampleHeight(worldPosition) > 1) {
				return 5;
			} else {
				return -1.5f;
			}
            return terrain.SampleHeight(worldPosition);
        }

        return 0f;
    }
}