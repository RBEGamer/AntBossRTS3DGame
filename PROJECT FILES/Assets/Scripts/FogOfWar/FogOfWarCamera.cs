using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FogOfWarCamera : MonoBehaviour
{
    Transform parent;

    public static RenderTexture Texture { get; private set; }
    public static FogOfWarCamera Instance { get; private set; }

    void Start()
    {
        Instance = this;
        GetComponent<Camera>().enabled = true;
        GetComponent<Camera>().targetTexture = Texture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);

        parent = transform.parent;
    }

    void Update()
    {
        transform.position = parent.position;
        transform.rotation = parent.rotation;
    }
}