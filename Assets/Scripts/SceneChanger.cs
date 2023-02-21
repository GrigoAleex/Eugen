using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int sceneIndex = -1;

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (!collision.tag.Equals("Player")) return;
        
        if (sceneIndex < 0) sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneIndex);
    }
}
