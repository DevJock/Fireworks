using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordPainter : MonoBehaviour
{

    public float TimeDelay;
    float timer;
    public Material defaultMaterial;
    Renderer[] renderers;


	void Start ()
    {
        TimeDelay = Random.Range(1.0f, 3.0f);
        timer = TimeDelay;
        renderers = new Renderer[transform.childCount];
        for(int i=0;i<transform.childCount;i++)
        {
            renderers[i] = transform.GetChild(i).GetComponent<Renderer>();
            renderers[i].material = defaultMaterial;
        }
		
	}


    private void Update()
    {
        if(timer <=0.0f)
        {
            Color color = new Color(Random.Range(51.0f, 255.0f) / 255.0f, Random.Range(51.0f, 255.0f) / 255.0f, Random.Range(51.0f, 255.0f) / 255.0f);
            for(int i=0;i<renderers.Length;i++)
            {
                renderers[i].material.color = color;
            }
            timer = TimeDelay;
        }
        timer -= Time.deltaTime;
    }

}
