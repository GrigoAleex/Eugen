using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroPlayableDirectorCallback : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject player;

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            player.GetComponent<SpriteRenderer>().color = Color.white;
        } 
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
