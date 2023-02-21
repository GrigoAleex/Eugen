using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGround : MonoBehaviour
{
    public GameObject[] pieces;

    private void Awake()
    {
        for(int i = 0; i < pieces.Length; i++)
        {
            pieces[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            pieces[i].transform.Rotate(new Vector3(0, 0, Random.Range(-5f, 5f)), Space.Self);
        }
    }
}
