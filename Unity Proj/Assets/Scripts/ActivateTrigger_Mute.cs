using UnityEngine;

public class ActivateTrigger_Mute : MonoBehaviour {
	public enum Mode {
		Trigger   = 0, // Just broadcast the action on to the target
		Replace   = 1, // replace target with source
		Activate  = 2, // Activate the target GameObject
		Enable    = 3, // Enable a component
		Animate   = 4, // Start animation on target
		Reverse   = 5, // Start Reverse on Target
		Deactivate= 6, // Decativate target GameObject
		Mute	  = 7, // Mutes the Audio
		Unmute    = 8  // Unmutes the Audio
	}

	/// The action to accomplish
	public Mode action = Mode.Activate;

	/// The game object to affect. If none, the trigger work on this game object
	public Object target;
	public GameObject source;
	public int triggerCount = 1;///
	public bool repeatTrigger = false;
	
	void DoActivateTrigger () {
		triggerCount--;

		if (triggerCount == 0 || repeatTrigger) {
			Object currentTarget = target != null ? target : gameObject;
			Behaviour targetBehaviour = currentTarget as Behaviour;
			GameObject targetGameObject = currentTarget as GameObject;
			if (targetBehaviour != null)
				targetGameObject = targetBehaviour.gameObject;
		
			switch (action) {
				case Mode.Trigger:
					targetGameObject.BroadcastMessage ("DoActivateTrigger");
					break;
				case Mode.Replace:
					if (source != null) {
						Object.Instantiate (source, targetGameObject.transform.position, targetGameObject.transform.rotation);
						DestroyObject (targetGameObject);
					}
					break;
				case Mode.Activate:
					targetGameObject.SetActive(true);
					break;
				case Mode.Enable:
					if (targetBehaviour != null)
						targetBehaviour.enabled = true;
					break;
				case Mode.Animate:
					targetGameObject.animation.Play ("take 001");
					break;	
				case Mode.Reverse:
				targetGameObject.animation["take 001"].speed = -targetGameObject.animation["take 001"].speed;
				targetGameObject.animation["take 001"].time = targetGameObject.animation["take 001"].length;
				targetGameObject.animation.Play("take 001");
					break;	
				case Mode.Deactivate:
					targetGameObject.SetActive(false);
					break;
			case Mode.Mute:
				targetGameObject.audio.mute = true;
				break;
			case Mode.Unmute:
				targetGameObject.audio.mute = false;
				break;
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		DoActivateTrigger ();
	}
}