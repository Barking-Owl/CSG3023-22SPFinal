/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: April 28, 2022
 * 
 * Description: Manages paddle. Main hurdle was to implement it moving without it just sticking in place. It should now work.
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10; //speed of paddle
    public float x; //Reference to x axis
    public Vector3 starterPos;

    private void Start()
    {
        starterPos = Vector3.zero;
        starterPos.y = -9;
        transform.position = starterPos; //Set starting position of the paddle to zero except for y
    }

    // Update is called once per frame
    void Update()
    {
        //Get the current position
        Vector3 curPos = transform.position;
        
        x = Input.GetAxis("Horizontal");

        //Move the paddle by getting new pos
        Vector3 newPos = new Vector3(curPos.x + x*speed*Time.deltaTime, -9, 0);
        //Now it works! Just needed to get the curPos. No movement besides x.
        transform.position = newPos;

    }//end Update()
}
