using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
	// Reference in the editor
	[SerializeField] private GameObject player;
	[SerializeField] private CanvasGroup exitBackgroundImageCAnvasGroup;
	[SerializeField] private CanvasGroup caughtBackgroundImageCanvasGroup;
	[SerializeField] private AudioSource exitAudio;
	[SerializeField] private AudioSource caughtAudio;
	// variables
	[SerializeField] private float fadeDuration = 1f;
	[SerializeField] private float displayImageDuration = 1f;
	bool m_IsPlayerAtExit;
	bool m_IsPlayerCaught;
	bool m_HasAudioPlayed;
	float m_Timer;
	
	// OnTriggerEnter is called when the Collider other enters the trigger.
	void OnTriggerEnter(Collider other)	{
		//Si el player conlisiona con este objecto
		if (other.gameObject == player)	{
			m_IsPlayerAtExit = true;
		}
	}
	// Método para verificar si el jugador ha sido capturado
	public void CaughtPlayer () {
		m_IsPlayerCaught = true;
	}
	// Update is called every frame, if the MonoBehaviour is enabled.
	void Update() {
		if (m_IsPlayerAtExit) {
			EndLevel(exitBackgroundImageCAnvasGroup, false, exitAudio);
		}
		else if(m_IsPlayerCaught){
			EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
		}
	}
	// Método para reiniciar o terminar el juego.
	private void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource) {
		// Reproduce el sonido en caso de no haberlo hecho antes
		if (!m_HasAudioPlayed) {
			audioSource.Play();
			m_HasAudioPlayed = true;
		}
		// Se crea un temporizador
		m_Timer += Time.deltaTime;
		//el alpha de la imagen se reducriá a lo largo del tiempo
		imageCanvasGroup.alpha = m_Timer / fadeDuration;
		// si el temporizador es mayor a la duración de la imagen, se cierra el juego
		if (m_Timer > fadeDuration + displayImageDuration) {
			// verifica si el juego se reinicia
			if ( doRestart == true) {
				// Reinicia la escena
				SceneManager.LoadScene(0);
			}
			else if (doRestart == false) {
				// Cierra la aplicación
				Application.Quit();
			}
		}
	}
}
