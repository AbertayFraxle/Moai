using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class teleportPlayer : MonoBehaviour
{

    private float timer;

    [SerializeField]
    public Transform target;
    Vector3 randomtranslate;
    Vector3 zeroTranslate;
    public LayerMask layer;
    public Camera cam;
    public float rand;

    private bool noise = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        rand = Random.Range(10, 30);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newpoint = cam.WorldToViewportPoint(this.transform.position);

        timer += Time.deltaTime;
        if (noise)
        {
            
            if (newpoint.x < 1 && newpoint.x > 0 && newpoint.y < 1 && newpoint.y > 0) 
            {
                float angle = Vector3.Angle(cam.transform.forward, this.transform.position - cam.transform.position);
                if (Mathf.Abs(angle) < 90)
                {

                    this.GetComponent<AudioSource>().Play();
                    noise = false;
                }
            }
        }

        if (timer >= rand)
        {
            float dist = (this.transform.position - target.position).magnitude;

            if ((newpoint.x > 1 || newpoint.x < 0) && (newpoint.y > 1 || newpoint.y < 0) || (dist > 100))
            {

                Vector2 random = (Random.insideUnitCircle * 10);
                randomtranslate.Set(random.x, 100, random.y);


                if (Physics.Raycast(target.position + randomtranslate, Vector3.down, out RaycastHit hit))
                {
                    Vector3 point = cam.WorldToViewportPoint(hit.point);

                    if (point.x < 1 && point.y < 1)
                    {

                    }
                    else
                    {

                        if ((target.position - hit.point).magnitude > 8)
                        {
                            this.transform.position = hit.point;
                            this.transform.LookAt(target.position);
                            noise = true;
                            timer = 0;
                            rand = Random.Range(10, 30);
                        }
                    }
                }


            }
        }
    }
}
