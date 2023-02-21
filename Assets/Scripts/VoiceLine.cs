using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VoiceLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textContainer;
    [SerializeField] private string _text;
    [SerializeField] private float _textDuration = 1.0f;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        { 
            _audioSource.clip = _audioClip;
            _audioSource.Play();

            StartCoroutine(displayVoiceLineText());
        }
    }

    IEnumerator displayVoiceLineText()
    {
        _textContainer.text = _text;

        yield return new WaitForSeconds(_textDuration);

        _textContainer.text = "";

        Destroy(gameObject);
    }
}
