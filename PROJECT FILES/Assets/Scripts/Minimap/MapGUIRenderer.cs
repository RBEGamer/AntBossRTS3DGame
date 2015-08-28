using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Map))]
public class MapGUIRenderer : MonoBehaviour
{
    Map map;
    GUIStyle iconStyle;

    [SerializeField]
    int iconSize = 24;

	[SerializeField]
	Texture toggleIcon;
	[SerializeField]
	Texture alphaPlusIcon;
	[SerializeField]
	Texture alphaMinusIcon;

    [SerializeField]
    bool displayCoordinates;

    [SerializeField]
    string label = "";

	[HideInInspector]
	float currentAlphaLevel = 1.0f;
    void Start()
    {
        map = GetComponent<Map>();
    }

    void DrawMapItems(ref bool labelDrawn)
    {
		if (map.isVisible) {
			foreach (MapItem item in map.MapItems.OrderBy(x => x.ZOrder)) {
				labelDrawn = item.Draw (labelDrawn) || labelDrawn;
			}
		}
    }

	void DrawButtons()
	{
		float left = map.ScreenLeft + map.ScreenSize;
		bool rightAligned = Screen.width - (left + iconSize) < iconSize;
		
		left = rightAligned ? map.ScreenLeft - iconSize : left;
		
		float offset = rightAligned ? -4f : 4f;
		GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if (GUI.Button(new Rect(left + offset, map.ScreenTop - 2, iconSize, iconSize), toggleIcon, iconStyle))
		{
			map.toggleMap();
		}

		if (map.isVisible) {
			if (GUI.Button (new Rect (left + offset, map.ScreenTop + iconSize, iconSize, iconSize), alphaPlusIcon, iconStyle)) {
				alphaUp ();
			}

			if (GUI.Button (new Rect (left + offset, map.ScreenTop + iconSize * 2, iconSize, iconSize), alphaMinusIcon, iconStyle)) {
				alphaDown ();
			}
		}
	}

	void alphaUp() {
		currentAlphaLevel = Mathf.Clamp (currentAlphaLevel += 0.125f, 0.2f, 1.0f);
	}

	void alphaDown () {
		currentAlphaLevel = Mathf.Clamp (currentAlphaLevel -= 0.125f, 0.2f, 1.0f);
	}

    void DrawLabel()
    {
		if (map.isVisible) {
			float top = 0f;

			switch (map.Orientation) {
			case MapOrientation.TopLeft:
			case MapOrientation.TopRight:
			case MapOrientation.TopCenter:
				top = map.ScreenTop + map.ScreenSize + 4;
				break;

			default:
				top = map.ScreenTop - 24f;
				break;
			}

			GUI.Box (new Rect (map.ScreenLeft - 2, top, map.ScreenSize + 4, 20f), "");
			GUI.Label (new Rect (map.ScreenLeft, top, map.ScreenSize + 4, 20f), label);

			if (displayCoordinates) {
				float wTop;
				float wLeft;

				Vector3 mpos = map.NormalizedMousePosition;
				Vector3 world = map.TargetPosition;

				if (map.ScreenToMapCoords (mpos.x, mpos.y, out wLeft, out wTop)) {
					world = map.MapToWorldCoords (wLeft, wTop);
				}

				GUIStyle style = new GUIStyle ("Label");
				style.alignment = TextAnchor.MiddleRight;

				float x = (float)Math.Round (world.x, 2);
				float z = (float)Math.Round (world.z, 2);

				GUI.Label (new Rect (map.ScreenLeft, top, map.ScreenSize - 4, 20f), string.Format ("Z: {0}", z), style);
				GUI.Label (new Rect (map.ScreenLeft, top, map.ScreenSize - 60f, 20f), string.Format ("X: {0}", x), style);
			}
		}
    }

    void DrawPingIcons(ref bool labelDrawn)
    {
        if (map.PingIcon != null)
        {
            float size = map.IconSizeScaled;
            Vector3 mpos = map.NormalizedMousePosition;

            foreach (MapActivePing ping in map.Pings)
            {
                float top;
                float left;

                map.WorldToMapCoordsStandard(ping.Position, out left, out top);

                top += map.ScreenTop - (size / 2f);
                left += map.ScreenLeft - (size / 2f);

                GUI.DrawTexture(new Rect(left, top, size, size), map.PingIcon, ScaleMode.ScaleAndCrop, map.PingIconAlphaBlend);

                if (!labelDrawn && !string.IsNullOrEmpty(ping.Label))
                {
                    if (mpos.x >= left && mpos.x <= left + size && mpos.y >= top && mpos.y <= top + size)
                    {
                        GUI.Box(new Rect(left + size, top, 150, 20), ping.Label);
                        labelDrawn = true;
                    }
                }
            }
        }
    }

    void OnGUI()
    {
        if (iconStyle == null)
        {
            iconStyle = new GUIStyle("button");
            iconStyle.padding = new RectOffset(0, 0, -2, 0);
        }

		if (map.isVisible) {
			GUI.color = new Color(1.0f, 1.0f, 1.0f, currentAlphaLevel);
			GUI.Box (new Rect (map.ScreenLeft - 2, map.ScreenTop - 2, map.ScreenSize + 4, map.ScreenSize + 4), "");
			GUI.BeginGroup (new Rect (map.ScreenLeft, map.ScreenTop, map.ScreenSize, map.ScreenSize));
			GUI.DrawTexture (new Rect (map.TextureLeft, map.TextureTop, map.TextureSize, map.TextureSize), map.Texture, ScaleMode.StretchToFill, true);
			GUI.EndGroup ();
		}

        bool labelDrawn = false;


        //DrawLabel();
        DrawMapItems(ref labelDrawn);
        DrawPingIcons(ref labelDrawn);
		//DrawButtons();
    }
}
