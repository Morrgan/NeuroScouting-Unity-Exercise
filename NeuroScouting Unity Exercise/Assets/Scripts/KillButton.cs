using UnityEngine;
using System.Collections;

public class KillButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
}
