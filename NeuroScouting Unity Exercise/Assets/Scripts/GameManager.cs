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
    bool justPressed = false;
    GameObject displayed = null;
    public GameObject[] trial;
    public GUIText scoreText;
    public int score;
    int rand = -1;

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
        RunTrial();
    }

    void RunTrial()
    {
        if (delay == false)
        {
            rand = (int)Random.Range(0, 5);
            if (dt == 0 || spawnControl == true)
            {
                displayed = (GameObject)GameObject.Instantiate(trial[rand], new Vector3(0, 1, 2), Quaternion.identity);
                displayed.transform.Rotate(new Vector3(-45, 0, 45));
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
                justPressed = false;
            }
        }
        if(Input.GetKeyDown("space"))
        {
            GameObject[] target = GameObject.FindGameObjectsWithTag("Target");
            if(target.Length == 0 && justPressed == false)
            {
                score -= 100;
                justPressed = true;
            }
            else if(justPressed == false)
            {
                score += (int)(2f / dt * 100);
                justPressed = true;
            }
        }
        dt += Time.deltaTime;
        UpdateScore();
    }

    void UpdateScore()
    {
        if (score < 0)
        {
            score = 0;
        }
        scoreText.text = "Score: " + score;
    }
}
