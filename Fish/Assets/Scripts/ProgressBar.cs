using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Biting))]
public class ProgressBar : MonoBehaviour {

	public Texture2D content;
	public Texture2D contour;
	
	public Vector2 position;
	public Vector2 size;
	public Vector2 contentOffset;

	private float progress;
	
	private Vector2 contentSize;

	public void Start () {
		progress = 0;

		position.x *= Screen.width;
		position.y *= Screen.height;
		size.x *= Screen.width;
		size.y *= Screen.height;

		contentOffset.x *= size.x;
		contentOffset.y *= size.y;

		contentSize = new Vector2(size.x * content.width / contour.width * 0.9f, size.y * content.height / contour.height * 1.5f);
	}

	// Use this for initialization
	public void OnGUI () {  
		GUI.BeginGroup (new Rect (position.x, position.y, size.x, size.y));
			GUI.BeginGroup (new Rect (contentOffset.x, contentOffset.y + (1 - progress) * contentSize.y, contentSize.x, progress * contentSize.y));
				GUI.DrawTexture (new Rect (0,-(1 - progress) * contentSize.y, contentSize.x, contentSize.y), content);
			GUI.EndGroup ();

			GUI.DrawTexture (new Rect (0, 0, size.x, size.y), contour);
		GUI.EndGroup ();

	}
	
	// Update is called once per frame
	public void SetSize (float progress) {
		this.progress = progress;
	}
}
