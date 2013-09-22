using UnityEngine;
using System.Collections;

/*
 *  A bird has an intruder he's wary of, which the bird will fly away from if he gets too close.
 *  If so, the bird runs to a specifed location, or else randomly flies away and disappears.
 */

public class Bird : MonoBehaviour { // Bird Mk II

    public GameObject intruder; // The intruder the bird will be wary of.
    public float intruderDistanceUntilRun = 2.5f; // The bird will run when the intruder is within this distance.
    public float intruderDistance; // Current distance from the intruder.  Constantly updates as intruder moves.


    public Vector3 birdHome; // The target in space that a Bird will try to stick to.
    public Vector3 birdRadius = 5.0f * Vector3.one; // Max Vector3 distance a Bird will wander from its home.
    public Vector3 birdRunLocation; // A Vector3 target that a bird will scatter to when intruder is too close.


    bool flyAway = false; // Whether the bird is triggered to scatter.
    float flyVelocity = 5f; // Velocity to run away at.  Measured in units per frame.


    public bool flyToRunLocation = true; // If true, bird flys to run location if disturbed.  Otherwise, bird randomly scatters.
    public bool flyInArc = true; // Makes the birds fly in a COOL arc.  Disable if you want bird to fly in a straight path;


    Vector3 runDirection; // Used if the bird is triggered to randomly scatter.
    float secondsPassed; // Used if the bird is triggered to randomly scatter.
    public float secondsUntilDisappear = 1.0f; // Time until bird disappears after running; Used if the bird is triggered to randomly scatter.



    public void Start() {

    }



    public void Update() {

     
        intruderDistance = (intruder.transform.position - this.transform.position).magnitude;

        /*  -----------------------
         *  --- RUN AWAY LOGIC. ---
            -----------------------  */

        // If the bird is running away...
        if (flyAway) {

            // If bird isn't specified to go to his run location, he'll fly in a random direction for a bit then disappear.
            if (!flyToRunLocation) {

                // Generate a random direction to fly in;
                if (secondsPassed == 0) {
                    runDirection = new Vector3(Random.Range(-30, 30), Random.Range(5, 10), Random.Range(-30, 30));
                    secondsPassed += Time.deltaTime;
                }

                // If time to disappear...
                else if (secondsPassed >= secondsUntilDisappear) {
                    Destroy(this);
                }
                
                // Otherwise, keeps going in a pre-determined random direction;
                else {
                    this.transform.Translate(runDirection * flyVelocity * Time.deltaTime);
                    secondsPassed += Time.deltaTime;
                }
            }

            //  If the bird is super close to where he's running, just set him there.  
            //  Prevents him from being stuck moving back and forth to reach it. 
            else if ((birdRunLocation - this.transform.position).magnitude < ((birdRunLocation - this.transform.position) * flyVelocity * Time.deltaTime).magnitude)
            { 
                this.transform.position = birdRunLocation; 
            }

            // Else, check if you're where you're running. If not, face this location and fly towards it.
            else if (this.transform.position != birdRunLocation) { 

                // Bird will fly in a COOL arc if true; 
                if (flyInArc) {
                    this.transform.LookAt(birdRunLocation);
                }

                // Moves bird in the direction of his run location.
                this.transform.Translate((birdRunLocation - this.transform.position) * flyVelocity * Time.deltaTime);
            }
        }

        // If the intruder gets too close, fly away.
        else if (intruderDistance < intruderDistanceUntilRun) {
            flyAway = true;
        }

        /*  ------------------------------
         *  --- GROUND MOVEMENT LOGIC. ---
            ------------------------------  */

        else {
        }

    } // End function Update()



}

