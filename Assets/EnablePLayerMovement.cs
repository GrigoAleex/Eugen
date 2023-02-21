using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePLayerMovement : MonoBehaviour
{
    public GameObject Player;
    public bool willEnable;

    private void Awake()
    {
        Player.GetComponent<Player>().enabled = willEnable;
    }
}
