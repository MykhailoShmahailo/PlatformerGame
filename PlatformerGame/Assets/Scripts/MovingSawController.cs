using UnityEngine;
using System.Collections;

public class MovingSawController : MonoBehaviour 
{

    private Vector3[] positions;
    private int _currentPos;

    public float unitsMove = 5f;
    public float maxSpeed = 5f;
    bool moveRight = true;
    public int currentPoint;

	// Use this for initialization
	void Start () 
    {
        positions = new Vector3[]
        {
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            new Vector3(transform.position.x + unitsMove, transform.position.y, transform.position.z)
        };
        

        transform.position = positions[0];
        currentPoint = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0f, 0f, 1f));
        StartMoving();
	}

    private void StartMoving()
    {
        if (transform.position == positions[currentPoint])
            currentPoint++;
        if (currentPoint >= positions.Length)
            currentPoint = 0;


        transform.position = Vector3.MoveTowards(transform.position, positions[currentPoint], maxSpeed * Time.deltaTime);

    }
}
