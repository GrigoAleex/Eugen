using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class StartTimeline : MonoBehaviour
{
    public PlayableDirector director;
    public Vector3 playerPostion = new Vector3(13.75f, -2.6f, 0f);
    public GameObject player;

    private bool _shouldMovePlayer = false;

    private void Update()
    {
        if (!_shouldMovePlayer) return;

        player.transform.position += Vector3.right * 0.124f;
        
        if (player.transform.position.x >= 13.75)
        {
            Destroy(gameObject);
            director.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            player.GetComponent<Locomotion>().Disable();
            _shouldMovePlayer= true;
        }
    }
}
