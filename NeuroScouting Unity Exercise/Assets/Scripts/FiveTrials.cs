using UnityEngine;
using System.Collections;

public class FiveTrials : MonoBehaviour {

    float dt = 0; // change in time between frames
    //prefabs
    public GameObject FootBallPrefab;
    public GameObject AltFootBallPrefab;
    public GameObject BatPrefab;
    public GameObject AltBatPrefab;
    public GameObject Frisbee;
    public GameObject AltFrisbee;
    // various control booleans
    bool delay = true;
    bool startDelay = true;
    bool spawnControl = true;
    bool justPressed = false;
    bool gameOver = false;
    GameObject displayed = null; // empty game object that gets reused
    public GameObject[] trial; // collection of game objects for quick access
    public GUIText scoreText; // text displayed on screen
    public int score; // the actual score value
    // various control ints
    int rand = -1;
    int trialControl = 0;
    int trialLocation; // location of the target object in the set of objects
    int[] secondaryLocation = new int[6];
    int secondaryControl = 0;
    float averageTime = 0f; // average time taken to press the space bar for correct targets
    int numPresses = 0;
    int numFails = 0;

    // Use this for initialization
    void Start () {
        // set the aforementioned collection of game objects
        trial = new GameObject[5];
        trial[0] = AltFootBallPrefab;
        trial[1] = BatPrefab;
        trial[2] = AltBatPrefab;
        trial[3] = Frisbee;
        trial[4] = AltFrisbee;
        // instantiate and display the target object at the beginning of the trial
        GameObject targ = (GameObject)GameObject.Instantiate(FootBallPrefab, new Vector3(0, 1, 2), Quaternion.identity);
        targ.transform.Rotate(new Vector3(-45, 0, 45));
        // randomly determine where in the set the target object will appear
        trialLocation = Random.Range(0, 5);
        // intialize array of which sets will have a target object in them
        for(int i = 0; i < secondaryLocation.Length; i++)
        {
            secondaryLocation[i] = 0;
        }
        // choose 3 random sets to have the target object
        for (int j = 0; j < 3; j++)
        {
            int r = Random.Range(0, 3);
            if(secondaryLocation[r] == 0)
            {
                secondaryLocation[r] = 1;
            }
            else if(r + 1 < secondaryLocation.Length && secondaryLocation[r + 1] == 0)
            {
                secondaryLocation[r + 1] = 1;
            }
            else if(r + 2 < secondaryLocation.Length && secondaryLocation[r + 2] == 0)
            {
                secondaryLocation[r + 2] = 1;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (secondaryControl > secondaryLocation.Length - 1)
        {
            DisplayFinal();
            if (Input.GetKeyDown("space"))
            {
                Application.LoadLevel("StartScreen");
            }
        }
        else
        {
            RunTrial(secondaryControl);
        }
	}

    void RunTrial(int loc)
    {
        // delay the game until the target object display is not on the screen, and don't continue if the game is over
        if (startDelay == false && gameOver == false)
        {
            // if not waiting and have spawned less than 6 objects (zero based index),
            if (delay == false && trialControl <= 5)
            {
                // make a random number
                rand = (int)Random.Range(0, 4);
                // if the time is exactly zero or the method is ready to spawn,
                if (dt == 0 || spawnControl == true)
                {
                    // if the current item in the trial is not the randomly determined location of the target object,
                    if (trialControl != trialLocation)
                    {
                        // spawn a random object from the collection
                        displayed = (GameObject)GameObject.Instantiate(trial[rand], new Vector3(0, 1, 2), Quaternion.identity);
                    }
                    // otherwise,
                    else
                    {
                        if (secondaryLocation[loc] != 0)
                        {
                            // spawn the target object
                            displayed = (GameObject)GameObject.Instantiate(FootBallPrefab, new Vector3(0, 1, 2), Quaternion.identity);
                        }
                        else
                        {
                            displayed = (GameObject)GameObject.Instantiate(trial[rand], new Vector3(0, 1, 2), Quaternion.identity);
                        }
                    }
                    // rotate the object so it looks better
                    displayed.transform.Rotate(new Vector3(-45, 0, 45));

                    // prevent any more objects from spawning until this one is gone
                    spawnControl = false;
                }
                // if it has been 2 seconds,
                else if (dt > 2)
                {
                    // reset time
                    dt = 0;
                    // delay spawning of next object
                    delay = true;
                    // advance the trial to the next object
                    trialControl++;
                }
            }
            // otherwise,
            else
            {
                // if it has been one second,
                if (dt > 1)
                {
                    // reset the timer
                    dt = 0;
                    // remove the delay
                    delay = false;
                    // ready the program to spawn another object
                    spawnControl = true;
                    // reset the spacebar if it has been pressed
                    justPressed = false;
                }
                // if 6 or more objects have been spawned
                if (trialControl > 5)
                {
                    // end the game
                    gameOver = true;
                }
            }
            // if the spacebar was pressed,
            if (Input.GetKeyDown("space"))
            {
                // check if the target object is on screen
                GameObject[] target = GameObject.FindGameObjectsWithTag("Target");
                // if not and the space bar hasn't been pressed already,
                if (target.Length == 0 && justPressed == false)
                {
                    // subtract 100 points per number of failed presses and note that the space bar was pressed
                    numFails++; // increase the number of failed presses
                    score -= 100 * numFails;
                    justPressed = true;
                }
                // otherwise if the space bar hasn't been pressed already,
                else if (justPressed == false)
                {
                    // add 2 divided by the amount of time it took to press the button times 100 to the score
                    score += (int)(2f / dt * 100);
                    // increase the number of presses and the average time it took to press the button
                    numPresses++;
                    averageTime += dt;
                    justPressed = true;
                }
            }
            // increase the time and update the game score
            dt += Time.deltaTime;

            UpdateScore();
        }
        // if the game is over
        else if (gameOver == true)
        {
            secondaryControl++;
            trialLocation = (int)Random.Range(0, 5);
            trialControl = 0;
            gameOver = false;
        }
        // otherwise
        else
        {
            // don't start the game until the first target object has been removed from the screen
            GameObject[] target = GameObject.FindGameObjectsWithTag("Target");
            if (target.Length == 0)
            {
                startDelay = false;
            }
        }
    }

    void UpdateScore()
    {
        // prevents the score from having a negative value
        if (score < 0)
        {
            score = 0;
        }
        // updates the score text
        scoreText.text = "Score: " + score;
    }

    void DisplayFinal()
    {
        // displays the final score, the average amount of time it took to press the space bar, the number of times the player pressed with the wrong object on screen,
        // and instructions on how to return to the start menu
        scoreText.text = "Score:" + score + "\nAverage Time to Press: " + averageTime / (float)numPresses + "\nNumber of False Positives: " + numFails + "\nPress Space to go back to the main menu.";
    }
}
