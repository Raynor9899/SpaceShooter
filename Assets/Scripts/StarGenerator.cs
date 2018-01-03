using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour {
    public GameObject StarGo;
    public int MaxStars;

    Color[] starColor = {
        new Color(0.5f, 0.5f, 1f), //blue
        new Color(0, 1f, 1f), //green
        new Color(1f, 1f, 0), //yellow
        new Color(1f, 0, 0), //red
    };
	// Use this for initialization
	void Start () {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));//This is bottom-left point (corner) of the screen.
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));//This is Top-right point (corner) of the screen.

        for (int i = 0; i < MaxStars; ++i) {
            GameObject star = (GameObject)Instantiate(StarGo);
            star.GetComponent<SpriteRenderer>().color = starColor[i % starColor.Length];
            star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

            star.transform.parent = transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
