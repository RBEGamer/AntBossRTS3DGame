using UnityEngine;
using System.Collections;

public class cursor_replacer : MonoBehaviour {

	public Texture2D cursorTexture;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
		Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
