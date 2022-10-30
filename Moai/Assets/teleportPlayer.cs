using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class teleportPlayer : MonoBehaviour
{

    public float timer, chaseTimer,killTimer;

    [SerializeField]
    public Transform target;
    Vector3 randomtranslate;
    Vector3 zeroTranslate;
    [SerializeField]
    public LayerMask layer;
    public Camera cam;
    public float rand;
    public bool seen, disappeared;
    public float disChance;
    public float moveSpeed;
    public float disRand;


    private bool noise = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        seen = false;
        rand = Random.Range(10, 30);
        disChance = 2;
        disRand = Mathf.Ceil(Random.value * disChance);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newpoint = cam.WorldToViewportPoint(this.transform.position);
        float dist = (this.transform.position - target.position).magnitude;
        timer += Time.deltaTime;
        if (noise)
        {
            if (!target.GetComponent<safeChecker>().isSafe())
            {
                if (newpoint.x < 1 && newpoint.x > 0 && newpoint.y < 1 && newpoint.y > 0)
                {
                    float angle = Vector3.Angle(cam.transform.forward, this.transform.position - cam.transform.position);
                    if (Mathf.Abs(angle) < 90)
                    {
                        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("effects", 1);
                        this.GetComponent<AudioSource>().Play();
                        noise = false;
                        seen = true;
                    }
                }
            }
            
        }

        //if the moai is seen, give the player 5 seconds before it starts pursuing, also regress the death timer by half speed
        if (seen)
        {
            chaseTimer += Time.deltaTime;
            killTimer -= Time.deltaTime / 2;
            if (chaseTimer > 5)
            {
                this.transform.LookAt(target.position);
                this.transform.position += transform.forward * moveSpeed * Time.deltaTime; 
            }

        }else
        {
            if (!disappeared)
            {
                if (dist < 30 && !target.GetComponent<safeChecker>().isSafe())
                {
                    //if the moai has teleported and has not been seen by the player, increment the timer until their death
                    killTimer += Time.deltaTime;
                }
            }
        }

        if (killTimer < 0)
        {
            killTimer = 0;
        }
        else
        {
            if (killTimer >= 60)
            {
                SceneManager.LoadScene(1, LoadSceneMode.Single);
            }
        }

        if (timer >= rand)
        {
            if ((newpoint.x > 1 || newpoint.x < 0) && (newpoint.y > 1 || newpoint.y < 0) || (dist > 200))
            {
                if (disRand < disChance)
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
                                disRand = Mathf.Ceil(Random.value * disChance);
                                this.transform.position = hit.point;
                                this.transform.LookAt(target.position);
                                noise = true;
                                timer = 0;
                                rand = Random.Range(10, 30);
                                chaseTimer = 0;
                                seen = false;
                                disappeared = false;
                            }
                        }
                    }
                }
                else
                {
                    disRand = Mathf.Ceil(Random.value * disChance);
                    disappeared = true;
                    seen = false;
                    rand = Random.Range(10, 30);
                    this.transform.position = new Vector3(0, -100, 0);
                    timer = 0;
                    disChance += 1;
                }
            }
        }
    }
}
