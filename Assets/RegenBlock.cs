using UnityEngine;
using System.Collections;

public class RegenBlock : MonoBehaviour {
    public GameObject regenBlock;
    public GameObject regenBlockPrefab;

    [Range(0, 30f)]
    public float waitTime = 5f;
	
    void Start()
    {
        StartCoroutine(Regen());
    }

	// Update is called once per frame
	IEnumerator Regen () {
        const float WAIT = 0.1f;
        while (true)
        {
            if(regenBlock != null)
                yield return new WaitForSeconds(WAIT);
            else
            {
                yield return new WaitForSeconds(waitTime);
                regenBlock = (GameObject)Instantiate(regenBlockPrefab, transform.position, transform.rotation);
                regenBlock.SetActive(true);
            }
        }
	}
}
