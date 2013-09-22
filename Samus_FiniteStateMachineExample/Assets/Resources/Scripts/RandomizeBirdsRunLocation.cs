using UnityEngine;
using System.Collections;

/* 
 *  Takes an array of GameObjects implementing a Bird script and randomizes their run locations in-place.  
 *  Useful if you want to give a bunch of birds a random scatter pattern every game run.
 */

public class RandomizeBirdsRunLocation : MonoBehaviour {


    public GameObject [] birds; // Array of GameObjects that use a Bird script.


    void Start() {

        // Implementation of the Fisher-Yates Shuffle Algorithm O(n). Shuffles in-place.
        for (int i = birds.Length; i > 1; i--)
        {
            // Pick random bird to swap its run location.
            int j = Random.Range(0, i); // 0 <= j <= i-1
 
            Vector3 temp = birds[j].GetComponent<Bird>().birdRunLocation; // Saves the birdRunLocation from the random bird.

            // Swaps run locations of the random bird and birds[i - 1]
            birds[j].GetComponent<Bird>().birdRunLocation = birds[i - 1].GetComponent<Bird>().birdRunLocation; 
            birds[i - 1].GetComponent<Bird>().birdRunLocation = temp;
        }

    }


} // End class RandomizeBirdsRunLocation


