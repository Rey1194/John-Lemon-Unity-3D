using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// Variables
	private Vector3 m_Movement;
	private Animator m_Animator;
	private AudioSource m_AudioSource;
	private Rigidbody m_Rigidbody;
	private Quaternion m_Rotation = Quaternion.identity;
	public float turnSpeed = 20f;
	
  // Start is called before the first frame update
  void Start() {
	  m_Animator = this.GetComponent<Animator>();
	  m_Rigidbody = this.GetComponent<Rigidbody>();
	  m_AudioSource = this.GetComponent<AudioSource>();
  }
  // Update is called once per frame
	void FixedUpdate() {
		// check for the player inputs
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
		// Apply the inputs to the player
	  m_Movement.Set(horizontal, 0f, vertical);
	  m_Movement.Normalize();
		// Verify if the player is moving to change the animation
	  bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
	  bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
	  bool isWalking = hasHorizontalInput || hasVerticalInput;
		m_Animator.SetBool("IsWalking", isWalking);
		// If moving, play the steps  SFX
		if (isWalking) {
			// only play if not playing already
			if (m_AudioSource.isPlaying == false) {
				// reproduce the SFX
				m_AudioSource.Play();
			}
		}
		else {
			m_AudioSource.Stop();
		}
	  // Player Rotation
		Vector3 desiredForward = Vector3.RotateTowards(this.transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
		m_Rotation = Quaternion.LookRotation(desiredForward);
	}
	// Callback for processing animation movements for modifying root motion.
	protected void OnAnimatorMove()
	{
		m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
		m_Rigidbody.MoveRotation(m_Rotation);
	}
}
