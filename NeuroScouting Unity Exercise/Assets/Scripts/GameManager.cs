using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    float dt = 0;
    //prefabs
    public GameObject FootBallPrefab;
    public GameObject AltFootBallPrefab;
    public GameObject BatPrefab;
    public GameObject AltBatPrefab;
    public GameObject Frisbee;
    public GameObject AltFrisbee;
    bool delay = false;
    bool spawnControl = true;
    GameObject displayed = null;
    public GameObject[] trial;


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
            if (dt == 0 || spawnControl == true)
            {
                displayed = (GameObject)GameObject.Instantiate(trial[(int)Random.Range(0, 5)], new Vector3(0, 0, 5), Quaternion.identity);
                spawnControl = false;
            }
            else if (dt > 2)
            {
                //Destroy(displayed);
                dt = 0;
                delay = true;
            }
        }
        else
        {
            if (dt > 1)
            {
                dt = 0;
                delay = false;
                spawnControl = true;
            }
        }
        dt += Time.deltaTime;
    }
}
