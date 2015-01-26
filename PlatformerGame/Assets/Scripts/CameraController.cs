using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public GUIStyle deathBoxGuiStyle;
    public GUIStyle starsBoxGuiStyle;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(new Vector3(target.position.x, target.position.y + 0.75f, target.position.z));
            Vector3 delta = new Vector3(target.position.x, target.position.y + 0.75f, target.position.z) - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;


            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }

    void OnGUI()
    {
        GUI.Box(new Rect(15f, 5f, 70, 20), "Stars: " + HeroController.starsCollected.ToString(), starsBoxGuiStyle);
        GUI.Box(new Rect(15f, 30f, 70, 20), "Deaths: " + HeroController.deaths.ToString(), deathBoxGuiStyle);
    }

}
