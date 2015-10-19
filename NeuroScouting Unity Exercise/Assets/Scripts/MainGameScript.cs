using UnityEngine;
using System.Collections;

public class MainGameScript : MonoBehaviour {

    //prefabs
    public GameObject FootBallPrefab;
    public GameObject AltFootBallPrefab;
    public GameObject BatPrefab;
    public GameObject AltBatPrefab;
    public GameObject Frisbee;
    public GameObject AltFrisbee;

    GameObject displayed = null;
    public GameObject[] trial;
    float deltaTime = 0.00f;
    bool delay = false;

    // Use this for initialization
    void Start () {
        // reference to remind myself how to make a game object...
        //GameObject b = (GameObject)GameObject.Instantiate(FootBallPrefab, new Vector3(0, 0, 10), Quaternion.identity);
        trial = new GameObject[6];
        trial[0] = FootBallPrefab;
        trial[1] = AltFootBallPrefab;
        trial[2] = BatPrefab;
        trial[3] = AltBatPrefab;
        trial[4] = Frisbee;
        trial[5] = AltFrisbee;
    }
	
	// Update is called once per frame
	void Update () {
        if (delay == false)
        {
            if (deltaTime == 0.00f)
            {
                displayed = (GameObject)GameObject.Instantiate(trial[(int)Random.Range(0, 5)], new Vector3(0, 0, 10), Quaternion.identity);
            }
            else if (deltaTime > 2)
            {
                //Destroy(displayed);
                deltaTime = 0.00f;
                delay = true;
            }
            else
            {
                float dt = Time.deltaTime;
                deltaTime += Time.deltaTime;
            }
        }
        else
        {
            if(deltaTime > 1)
            {
                deltaTime = 0.00f;
                delay = false;
            }
            else
            {
                float dt = Time.deltaTime;
                deltaTime += Time.deltaTime;
            }
        }
	}
}
