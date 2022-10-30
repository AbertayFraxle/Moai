using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    public Slider ambience, effects, sensitivity;
    public GameObject main;
    // Start is called before the first frame update
    void Start()
    {
        ambience.value = PlayerPrefs.GetFloat("ambience",0.5f);
        effects.value = PlayerPrefs.GetFloat("effects", 1f); 
        sensitivity.value = PlayerPrefs.GetFloat("sensitivity", 0.5f);


        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ambienceUpdate()
    {
        PlayerPrefs.SetFloat("ambience", ambience.value);
    }

    public void effectUpdate()
    {
        PlayerPrefs.SetFloat("effects", effects.value);
    }

    public void sensitivityUpdate()
    {
        PlayerPrefs.SetFloat("sensitivity", sensitivity.value);
    }

    public void backButton()
    {
        if (main != null)
        {
            main.SetActive(true);
        }

        this.gameObject.SetActive(false);
    }




}
