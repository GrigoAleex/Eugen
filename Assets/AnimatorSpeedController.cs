using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSpeedController : MonoBehaviour
{
    public float speed;

    private void Awake()
    {
        GetComponent<Animator>().speed = speed;
    }

}
