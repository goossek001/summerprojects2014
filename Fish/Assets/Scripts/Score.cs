using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public Vector2 position;
	public Sprite[] numberSprites;
	
	private GameObject[] numbers;

	private int score;
	
	public void Start() {
		float screenSize_y = Camera.main.orthographicSize * 2;
		float screenSize_x = screenSize_y * Screen.width / Screen.height;

		position = (Vector3) new Vector2 (screenSize_x * position.x, screenSize_y * position.y);

		SetScore (0);
	}
	
	private void SetScore(int score) {
		if (numbers != null) {
			for (int i = 0; i < numbers.Length; i++) {
				Destroy(numbers[i]);
			}
		}

		this.score = score;

		numbers = new GameObject[(int) (score != 0? Mathf.Log10(score)+1: 1)];
		for (int i = 0; i < numbers.Length; i++) {
			numbers[i] = new GameObject("number");
			
			numbers[i].transform.parent = transform;
			
			numbers[i].transform.localPosition = (Vector3) new Vector2(position.x+(0.5f*(numbers.Length - 1) - i )*numberSprites[0].bounds.size.x*5/4, position.y + 0.5f);
			
			SpriteRenderer renderer = numbers[i].AddComponent<SpriteRenderer>();
			renderer.sprite = numberSprites[(int)((score / Mathf.Pow(10, i)) % 10)];
			renderer.sortingOrder = 1;
		}
	}
	
	// Update is called once per frame
	public void AddScore (int deltaScore) {
		SetScore (score + deltaScore);
	}
}
