using UnityEngine;
using System.Collections;

public class TimedSpawner : MonoBehaviour {
    public GameObject spawnable;

    [Range(0, 60)]
    public float spawnTime = 10;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
	}
	
	IEnumerator Spawn()
    {
        while (true) {
            yield return new WaitForSeconds(spawnTime);

            var spawned = (GameObject)Instantiate(spawnable, transform.position, transform.rotation);
            spawned.SetActive(true);

            Destroy(spawned, spawnTime * 10);
        }
    }
}
