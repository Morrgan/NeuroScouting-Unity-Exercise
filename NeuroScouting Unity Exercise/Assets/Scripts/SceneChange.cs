using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // change to the 1 trial scene
    public void ChangeScene()
    { 
        Application.LoadLevel("MainScene");
    }

    // change to the 5 trial scene
    public void ChangeSceneFive()
    {
        Application.LoadLevel("FiveScene");
    }

    // change to the 10 trial scene
    public void ChangeSceneTen()
    {
        Application.LoadLevel("TenScene");
    }
}
