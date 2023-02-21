using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MachineWriter : MonoBehaviour
{
    private TextMeshProUGUI _object;
    
    public void DisplayText(string text)
    {
        _object = GetComponent<TextMeshProUGUI>();
        _object.text = "";
        if (_object.text != text) StartCoroutine(Display(text));
    }

    private IEnumerator Display(string text)
    {
        foreach (char c in text.ToCharArray())
        {
            _object.text += c;
            yield return null;
        }
    }
}
