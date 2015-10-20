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
    bool delay = true;
    bool startDelay = true;
    bool spawnControl = true;
    bool justPressed = false;
    GameObject displayed = null;
    public GameObject[] trial;
    public GUIText scoreText;
    public int score;
    int rand = -1;
    int trialControl = 0;
    int trialLocation;

    // Use this for initialization
    void Start () {
        // reference to remind myself how to make a game object...
        //GameObject b = (GameObject)GameObject.Instantiate(FootBallPrefab, new Vector3(0, 0, 10), Quaternion.identity);
        trial = new GameObject[5];
        trial[0] = AltFootBallPrefab;
        trial[1] = BatPrefab;
        trial[2] = AltBatPrefab;
        trial[3] = Frisbee;
        trial[4] = AltFrisbee;
        GameObject targ = (GameObject)GameObject.Instantiate(FootBallPrefab, new Vector3(0, 1, 2), Quaternion.identity);
        targ.transform.Rotate(new Vector3(-45, 0, 45));
        trialLocation = Random.Range(0, 5);
    }
	
	// Update is called once per frame
	void Update () {
        RunTrial();
    }

    void RunTrial()
    {
        if (startDelay == false)
        {
            if (delay == false && trialControl < 5)
            {
                rand = (int)Random.Range(0, 4);
                if (dt == 0 || spawnControl == true)
                {
                    if (trialControl != trialLocation)
                    {
                        displayed = (GameObject)GameObject.Instantiate(trial[rand], new Vector3(0, 1, 2), Quaternion.identity);
                    }
                    else
                    {
                        displayed = (GameObject)GameObject.Instantiate(FootBallPrefab, new Vector3(0, 1, 2), Quaternion.identity);
                    }
                    displayed.transform.Rotate(new Vector3(-45, 0, 45));
                    
                    spawnControl = false;
                }
                else if (dt > 2)
                {
                    dt = 0;
                    delay = true;
                    trialControl++;
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
            if (Input.GetKeyDown("space"))
            {
                GameObject[] target = GameObject.FindGameObjectsWithTag("Target");
                if (target.Length == 0 && justPressed == false)
                {
                    score -= 100;
                    justPressed = true;
                }
                else if (justPressed == false)
                {
                    score += (int)(2f / dt * 100);
                    justPressed = true;
                }
            }
            dt += Time.deltaTime;
            UpdateScore();
        }
        else
        {
            GameObject[] target = GameObject.FindGameObjectsWithTag("Target");
            if(target.Length == 0)
            {
                startDelay = false;
            }
        }
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
