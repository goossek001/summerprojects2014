using UnityEngine;
using System.Collections;

public class FishAI : Swimming {

	public Collider2D sightArea;

	public float turnDelay = 0.5f;
	private float turnTimer;
	private int swimDirection;
	
	private State state;

	// Use this for initialization
	override public void Start () {
		base.Start ();

		Vector2 goal = new Vector2 (-transform.position.x, transform.position.y);
		state = new WanderState (goal);
		turnTimer = turnDelay;
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		Vector2 goal = state.findNextGoal(this, null);
		Vector2 directionVector = goal - (Vector2)transform.position;
		directionVector = directionVector.normalized;
		int direction = (int) Mathf.Sign(Vector2.Dot(directionVector, -transform.up));
		if (direction != swimDirection) {
			turnTimer -= Time.fixedDeltaTime;
			if (turnTimer <= 0) {
				swimDirection = direction;
				turnTimer = turnDelay;
			}
		}
		Swim(swimDirection);
	}

	public void ChangeState (State newState) {
		state = newState;
	}
	
	public void Poke(GameObject fish) {
		//AddFish(fish);
		
		state.FishLocated (this, null);

		//Remove(fish);
	}

	public void FishLocated(GameObject fish) {
		//AddFish(fish);

		state.FishLocated (this, null);
	}
	
	public void FishLost(GameObject fish) {
		//Remove(fish); 
		
		state.FishLost (this, null);
	}
}

public interface State {
	Vector2 findNextGoal (FishAI handler, GameObject[] objectsInSight);
	void FishLocated (FishAI handler, GameObject[] objectsInSight);
	void FishLost (FishAI handler, GameObject[] objectsInSight);
}

public class WanderState: State {

	private Vector2 goal;

	public WanderState(Vector2 goal) {
		this.goal = goal;
	}
	
	Vector2 State.findNextGoal (FishAI handler, GameObject[] objectsInSight) {
		return goal;
	}

	void State.FishLocated (FishAI handler, GameObject[] objectsInSight) {

	}

	void State.FishLost (FishAI handler, GameObject[] objectsInSight) {

	}
}
/*
public class ChaseState: State {

	private GameObject pray;
	private Vector2 lastKnownPosition;
	private float timePast;

}

public class FleeState: State {

	private GameObject[] hunters;
	private Vector2 lastKnownPosition;

}
*/
