using UnityEngine;
using System.Collections;

public class FishAI : Swimming {

	public Collider2D sightArea;

	public float turnDelay = 0.2f;
	private float turnTimer;
	
	private State state;

	// Use this for initialization
	override public void Start () {
		base.Start ();

		Vector2 goal = new Vector2 (-transform.position.x, transform.position.y);
		state = new WanderState (goal);
	}
	
	// Update is called once per frame
	public void Update () {
		Vector2 goal = state.findNextGoal(this, null/*sightArea.collidingObjects*/);
		Vector2 direction = (goal - (Vector2)transform.position).normalized;
		int relativeDirection = (int) Mathf.Sign(Vector2.Dot(direction, -transform.up));
		Swim(relativeDirection);
	}

	public void ChangeState (State newState) {
		state = newState;
	}
}

public interface State {
	Vector2 findNextGoal (FishAI handler, GameObject[] objectsInSight);
}

public class WanderState: State {

	private Vector2 goal;

	public WanderState(Vector2 goal) {
		this.goal = goal;
	}
	
	Vector2 State.findNextGoal (FishAI handler, GameObject[] objectsInSight) {
		return goal;
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
