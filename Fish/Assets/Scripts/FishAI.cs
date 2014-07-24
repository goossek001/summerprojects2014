using UnityEngine;
using System.Collections;

public class FishAI : Swimming {

	public Collider2D sightArea;

	public float turnDelay = 0.5f;
	private float turnTimer;
	private int swimDirection;
	
	private State state;

	private bool adrialineActiveted;

	// Use this for initialization
	override public void Start () {
		base.Start ();

		Vector2 goal = new Vector2 (-transform.position.x, transform.position.y);
		state = new WanderState (goal);
		turnTimer = turnDelay;

		adrialineActiveted = false;
	}

	public void ActiveteAdrialine() {
		if (!adrialineActiveted) {
			adrialineActiveted = true;
			
			turnPower *= 2f;
			turnDelay *= 0.4f;

			rigidbody2D.velocity = Vector2.zero;
		}

	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		Vector2 goal = state.findNextGoal(this);
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
		FishLocated (fish);
	}

	public void FishLocated(GameObject fish) {
		if (fish.GetComponent<PlayerInput> () != null) state.FishLocated (this, fish);
	}
	
	public void FishLost(GameObject fish) {
		if (fish.GetComponent<PlayerInput> () != null) state.FishLost (this, fish);
	}
}

public interface State {
	Vector2 findNextGoal (FishAI handler);
	void FishLocated (FishAI handler, GameObject newFish);
	void FishLost (FishAI handler, GameObject lostFish);
}

public class WanderState: State {

	private Vector2 goal;

	public WanderState(Vector2 goal) {
		this.goal = goal;
	}
	
	Vector2 State.findNextGoal (FishAI handler) {
		return goal;
	}

	void State.FishLocated (FishAI handler, GameObject newFish) {
		if (newFish.transform.localScale.y < handler.transform.localScale.y) {
			handler.ChangeState(new ChaseState());
		} else if (newFish.transform.localScale.y > handler.transform.localScale.y) {
			handler.ChangeState(new FleeState(newFish, handler));
		}
	}

	//This method dont have to do anything, because the only fish in sight in wander state will be fish with the same size as himself (no thread and no pray)
	void State.FishLost (FishAI handler, GameObject lostFish) { }
}

public class ChaseState: State {

	private GameObject pray;
	private Vector2 lastKnownPosition;
	private float timePast;
	
	Vector2 State.findNextGoal (FishAI handler) {
		return (Vector2) pray.transform.position;
	}
	
	void State.FishLocated (FishAI handler, GameObject newFish) {
		if (newFish.transform.localScale.y < handler.transform.localScale.y) {
			handler.ChangeState(new ChaseState());
		} else if (newFish.transform.localScale.y > handler.transform.localScale.y) {
			handler.ChangeState(new FleeState(newFish, handler));
		}
	}

	void State.FishLost (FishAI handler, GameObject lostFish) {

	}
}

public class FleeState: State {

	private GameObject hunter;
	private bool isHunterInSight;
	private Vector2 lastKnownPosition;

	public FleeState (GameObject hunter, FishAI handler) {
		this.hunter = hunter;
		lastKnownPosition = hunter.transform.position;
		isHunterInSight = true;

		handler.ActiveteAdrialine ();
		handler.GetComponentInChildren <MouthOpening> ().Scare ();
	}

	Vector2 State.findNextGoal (FishAI handler) {
		if (isHunterInSight && hunter != null) {
			lastKnownPosition = hunter.transform.position;
		}

		return (Vector2) handler.transform.position * 2 - lastKnownPosition;
	}
	
	void State.FishLocated (FishAI handler, GameObject newFish) {
		if (newFish.transform.localScale.y > handler.transform.localScale.y) {
			hunter = newFish;
			lastKnownPosition = hunter.transform.position;
			isHunterInSight = true;
		}
	}

	void State.FishLost (FishAI handler, GameObject lostFish) { 
		if (lostFish == hunter) {
			isHunterInSight = false;
			lastKnownPosition = (Vector2) (handler.transform.position-handler.transform.right);
		}
	}
}
