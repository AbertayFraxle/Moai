using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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

    private bool noise = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (noise)
        {
            Vector3 newpoint = cam.WorldToViewportPoint(this.transform.position);
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

        if (timer >= 5)
        {

            Vector2 random = (Random.insideUnitCircle * 10);
            randomtranslate.Set(random.x, 100, random.y);


            if (Physics.Raycast(target.position+randomtranslate, Vector3.down, out RaycastHit hit))
            {
                Vector3 point = cam.WorldToViewportPoint(hit.point);

                if (point.x < 1 && point.y < 1)
                {

                }
                else
                {

                    if ((hit.point - target.position).magnitude > 8)
                    {
                        this.transform.position = hit.point;
                        this.transform.LookAt(target.position);
                        noise = true;
                        timer = 0;
                    }
                }
            }



        }
    }
}
