using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class BoulderSpawner : MonoBehaviour {
    Collider2D col;
    public GameObject boulder;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            boulder.SetActive(!boulder.activeSelf);
            col.enabled = false;
        }
    }
}
