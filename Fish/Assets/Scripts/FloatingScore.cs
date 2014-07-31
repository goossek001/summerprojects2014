using UnityEngine;
using System.Collections;

public class FloatingScore: MonoBehaviour{

	public float lifeTime = 5;
	public Sprite[] numberSprites;

	private float timer;
	private GameObject[] numbers;


	public void Start() {
		timer = 0;
	}

	public void Init (int score) {
		numbers = new GameObject[(int) Mathf.Log10(score)+1];
		for (int i = 0; i < numbers.Length; i++) {
			numbers[i] = new GameObject("number");
			
			numbers[i].transform.parent = transform;

			numbers[i].transform.localPosition = (Vector3) new Vector2((0.5f * (numbers.Length - 1) - i)*numberSprites[0].bounds.size.x*5/4, 0);

			SpriteRenderer renderer = numbers[i].AddComponent<SpriteRenderer>();
			renderer.sprite = numberSprites[(int)((score / Mathf.Pow(10, i)) % 10)];
			renderer.sortingOrder = 1;
		}
	}
	
	// Update is called once per frame
	public void Update () {
		for (int i = 0; i < numbers.Length; i++) {
			Color color = numbers[i].renderer.material.color;
			color.a =  (lifeTime - timer) / lifeTime;
			numbers[i].renderer.material.color = color;
		}

		timer += Time.deltaTime;
		if (timer >= lifeTime) {
			Destroy(gameObject);
		}
	}
}
