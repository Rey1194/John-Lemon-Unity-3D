using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
	// Editor reference
	[SerializeField] private Transform playerTransform;
	// Variables
	bool m_IsPlayerInRange;
	public GameEnding gameEnding;
	
	// OnTriggerEnter is called when the Collider other enters the trigger.
	void OnTriggerEnter(Collider other) {
		if (other.transform == playerTransform) {
			m_IsPlayerInRange = true;
		}
	}
	// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	void OnTriggerExit(Collider other) {
		if (other.transform == playerTransform) {
			m_IsPlayerInRange = false;
		}
	}
	// Update is called every frame, if the MonoBehaviour is enabled.
	void Update() {
		if (m_IsPlayerInRange) {
			// ¿está el jugador dentro del radio de alcance?
			Vector3 direction = playerTransform.position - this.transform.position + Vector3.up;
			// crear un rayo
			Ray ray = new Ray(this.transform.position, direction);
			// Esta línea define la variable RaycastHit
			RaycastHit raycastHit;
			// Solo se ejecutará si el Raycast ha chocado con algo.
			if (Physics.Raycast(ray, out raycastHit)) {
				// Identificar si el personaje del jugador está en el radio de alcance
				if (raycastHit.collider.transform == playerTransform) {
					// terminar el juego
					gameEnding.CaughtPlayer();
				}
			}
		}
	}
}
