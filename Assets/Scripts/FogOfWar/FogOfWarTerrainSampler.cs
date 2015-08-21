using UnityEngine;

public class FogOfWarTerrainSampler : FogOfWarHeightSampler
{
    [SerializeField]
    Terrain terrain = null;

    public override float SampleHeight(Vector3 worldPosition)
    {
        if (terrain != null)
        {
			if(terrain.SampleHeight(worldPosition) > 1.0f) {
				return terrain.SampleHeight(worldPosition);
			} else {
				return terrain.SampleHeight(worldPosition) - 1.4f;
			}
			return terrain.SampleHeight(worldPosition) - 1.5f;
        }

        return 0f;
    }
}