using Eugen;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : Subject
{
    [SerializeField] private float _textSpeed = 0.3f;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string[] _lines;
    [SerializeField] private int _index;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private void Awake()
    {
        _text.text = "";
        StartDialog();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (HasFinisedToWriteLine()) GoToNextLine();
            else
            {
                StopAllCoroutines();
                _text.text = _lines[_index];
            }
        }
    }

    private bool HasFinisedToWriteLine()
    {
        return _text.text == _lines[_index];
    }

    public void StartDialog()
    {
        _index = 0;
        Notify("Dialog started");
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach(char c in _lines[_index].ToCharArray()) {
            _text.text += c;

            _audioSource.clip = _audioClip;
            _audioSource.Play();
            
            yield return new WaitForSeconds(_textSpeed);
        }
    }

    private void GoToNextLine()
    {
        if (_index == _lines.Length - 1) {
            gameObject.SetActive(false);
            Notify("Dialog finished");
            return;
        }

        _index++;
        _text.text = "";
        StartCoroutine(TypeLine());
    }
}

namespace Eugen
{
    public interface IObserver
    {
        public void OnNotify(string subject, string action);
    }
}

public class Subject : MonoBehaviour
{
    [SerializeField]
    protected List<IObserver> _observers = new List<IObserver> ();

    public void AddObserver(IObserver observer) { _observers.Add(observer); }
    public void RemoveObserver(IObserver observer) { _observers.Remove(observer); }

    protected void Notify(string action)
    {
        _observers.ForEach(observer => observer.OnNotify(this.GetType().Name, action));
    }
}
