/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: April 28, 2022
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Ball : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Set in inspector - save score and score text")]
    public GameObject paddle;
    public float speed;
    [Tooltip("Set dynamically")]
    public int score;
    public Text scoreTxt;
    public Text brickTxt;

    [Header("Ball Settings")]
    public AudioSource audioSource;
    public AudioClip clip;
    public Text ballTxt;
    public bool isInPlay;
    public Rigidbody rb;
    [Tooltip("This is our lives")]
    public int numberOfBalls; //The lives
    [Tooltip("How can we move the ball? Only y needs a value in inspector.")]
    public Vector3 initialForce;




    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }//end Awake()


        // Start is called before the first frame update
        void Start()
    {
        SetStartingPos(); //set the starting position

    }//end Start()


    // Update is called once per frame
    void Update()
    {
        Vector3 curPos = transform.position;
        //Update the text. Ball text is the lives
        if (ballTxt)
        {
            ballTxt.text = "Ball(s): " + numberOfBalls;
        } //end if
        //Score is how many bricks got caught
        if (scoreTxt)
        {
            scoreTxt.text = "Score: " + score;
        } //end if


        if (!isInPlay) {
            //Move the ball to correspond with the paddle's x position
            Vector3 newPos = new Vector3(paddle.transform.position.x, curPos.y, curPos.z);
            transform.position = newPos;
        } //end if
        //Move spacebar
        if (Input.GetKey("space") && !isInPlay)
        {
            isInPlay = true;
            Move();
        } //end if

        if (brickTxt)
        {
            brickTxt.text = "Bricks Left: " + BrickSpawner.brickCount;
        }
        if (BrickSpawner.brickCount <= 0)
        {
            GameOver();
        }
    }//end Update()


    //Updates after update
    private void LateUpdate()
    {
        if (isInPlay)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }

    }//end LateUpdate()


    void SetStartingPos()
    {
        isInPlay = false;//ball is not in play
        rb.velocity = Vector3.zero;//set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x; //x position of paddel
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y; //Y position of paddle plus it's height

        transform.position = pos;//set starting position of the ball 
    }//end SetStartingPos()

    public void Move()
    {
        //Since it is in play
        rb.AddForce(initialForce);
    }

    public void OnCollisionEnter(Collision col)
    {
        //First play the audiosource
        audioSource.PlayOneShot(clip);
        //Get col's tag, if it is Brick destroy it and add score
        if (col.gameObject.tag == "Brick")
        {
            score += 100;
            Destroy(col.gameObject);
            BrickSpawner.brickCount--;
        } //end if
    } //end OnCollisionEnter

    public void GameOver()
    {
        SceneManager.LoadScene("SampleScene");
    } //end GameOver()

    //For the ball hitting bounds
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "OutBounds")
        {
            numberOfBalls--;
            Invoke("SetStartingPos", 2f);
        } 
        if (numberOfBalls < 0)
        {
            //Should be a gameover sign. We can also reload the scene
            GameOver();
        }
    } //end OnTriggerEnter


}
