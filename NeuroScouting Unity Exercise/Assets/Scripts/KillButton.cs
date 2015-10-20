using UnityEngine;
using System.Collections;

public class KillButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // when called, delete the object this is attached to
    public void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
}
