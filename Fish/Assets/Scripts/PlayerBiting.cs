using UnityEngine;
using System.Collections;

public class PlayerBiting: Biting {

	private Score totalScore;

	public override void Start () {
		base.Start ();

		totalScore = Camera.main.GetComponentInChildren<Score> ();
	}
	
	protected override void Eat(GameObject food) {
		int score = (int)food.transform.localScale.y;
		if (score == 0) score = 75;
		else score *= 100;
		
		GameObject floatingScore = (GameObject) Instantiate(Resources.Load("FloatingScore", typeof (GameObject)), food.transform.position, Quaternion.identity);
		floatingScore.GetComponent<FloatingScore>().Init(score);

		totalScore.AddScore (score);

		base.Eat (food);
	}
}
