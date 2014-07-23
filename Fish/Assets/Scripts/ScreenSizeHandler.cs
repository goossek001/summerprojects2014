using UnityEngine;
using System.Collections;

public class ScreenSizeHandler : MonoBehaviour {

	public Transform fishSpawnTrigger;

	public Transform playerLeftBoundrie;
	public Transform playerRightBoundrie;
	public Transform playerUpBoundrie;
	public Transform playerDownBoundrie;

	// Use this for initialization
	public void Start () {
		float screenSize_y = Camera.main.orthographicSize * 2;
		float screenSize_x = screenSize_y * Screen.width / Screen.height;

		fishSpawnTrigger.localScale = new Vector2 (screenSize_x + 10, screenSize_y + 10);

		float extraSize = 200; //To prevent a fish being partial outside the area when growing

		playerLeftBoundrie.localScale = new Vector2 (extraSize, screenSize_y+extraSize);
		playerLeftBoundrie.position = new Vector2 (-0.5f*(screenSize_x+playerLeftBoundrie.localScale.x), 0);
		
		playerRightBoundrie.localScale = new Vector2 (extraSize, screenSize_y+extraSize);
		playerRightBoundrie.position = new Vector2 (0.5f*(screenSize_x+playerRightBoundrie.localScale.x), 0);
		
		playerUpBoundrie.localScale = new Vector2 (screenSize_x, extraSize);
		playerUpBoundrie.position = new Vector2 (0, 0.5f*(screenSize_y+playerUpBoundrie.localScale.y));
		
		playerDownBoundrie.localScale = new Vector2 (screenSize_x, extraSize);
		playerDownBoundrie.position = new Vector2 (0, -0.5f*(screenSize_y+playerDownBoundrie.localScale.y));
	}
}
