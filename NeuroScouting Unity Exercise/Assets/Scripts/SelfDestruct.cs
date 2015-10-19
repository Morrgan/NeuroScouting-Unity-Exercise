using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    float deltaTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(deltaTime > 2)
        {
            Destroy(this.gameObject);
        }
        deltaTime += Time.deltaTime;
	}
}
