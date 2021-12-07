using UnityEngine;
using System.Collections;

public class SpikeDropper : MonoBehaviour {
    public GameObject spike;
    public GameObject spikeImage;

    Transform dropPoint;

    const float lowTime = 1f;
    const float highTime = 5f;

    // Use this for initialization
    void Start () {
        dropPoint = spikeImage.transform;
        StartCoroutine(drop());
	}
	
	IEnumerator drop()
    {
        spikeImage.SetActive(false);
        float time = Random.Range(lowTime, highTime);
        while (true)
        {
            yield return new WaitForSeconds(time);
            spikeImage.SetActive(true);
            yield return new WaitForSeconds(2f);
            spikeImage.SetActive(false);

            Quaternion rotation = Quaternion.Euler(0, 0, 180);
            Destroy((GameObject)Instantiate(spike, dropPoint.position, rotation), 5);
            time = Random.Range(lowTime, highTime);
        }
    }
}
