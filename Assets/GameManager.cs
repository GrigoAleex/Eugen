using Eugen;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IObserver
{
    public List<Subject> subjects= new List<Subject>();
    public GameObject player;
    public Image dialogBox;

    private void Awake()
    {
        subjects.ForEach(subject => subject.AddObserver(this));
        dialogBox.color = new Color(0, 0, 0, 0);
    }
    
    public void OnNotify(string subject, string action)
    {
        if (action.Equals("Dialog finished"))
        {
            player.GetComponent<PlayableDirector>().Play();
            player.GetComponent<Locomotion>().enabled = true;
            dialogBox.color = new Color(0, 0, 0, 0);
        } else if (action.Equals("Dialog started")) {
            player.GetComponent<Locomotion>().enabled = false;
            dialogBox.color = Color.white;
        }
    }
}
