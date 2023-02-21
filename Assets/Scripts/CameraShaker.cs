using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Camera _camera;
    public float duration;
    public float magnitude;

    private void Awake()
    {
        StartCoroutine(Shake(duration, magnitude));    
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = _camera.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            _camera.transform.position += new Vector3(x, y, -10f) * 0.05f;
            elapsed += Time.deltaTime;

            yield return null;
        }

        _camera.transform.position = orignalPosition;
    }
}
