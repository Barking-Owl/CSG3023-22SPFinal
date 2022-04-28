/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: April 28, 2022
 * 
 * Description: Spawns bircks. For the most part already done, but there were errors to do with a missing declaration of brickGO.
 * There was also no reference in brickPrefab which has been fixed.
 * There was an error to instantiate.
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
   
    public GameObject brickPrefab; //Reference to brick prefab
    public GameObject brickGO; //Individual bricks that'll be generated
    public float paddingBetweenBricks = 0.25f; //space between bricks
    private Vector2 brickPadding = new Vector2(0,0);

    static public int brickCount;

    // Start is called before the first frame update
    void Start()
    {

       //brick padding is the width/height of the brick plus the padding between
       brickPadding.x = brickPrefab.transform.localScale.x + paddingBetweenBricks;
       brickPadding.y = brickPrefab.transform.localScale.y + paddingBetweenBricks;


        for (int y=0; y < 7; y++)
        {
            for(int x=0; x < 7; x++)
            {
                Vector3 pos = new Vector3(x * brickPadding.x , y * brickPadding.y, 0); 
              
                brickGO = Instantiate(brickPrefab); 
              
                brickGO.transform.parent = transform; 
                brickGO.transform.localPosition = pos;
                brickCount++;
            }//end for(int x=0; x < 9; x++)
        }//end for (int y=0; y < 9; y++)
    }

}
