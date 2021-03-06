using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public Transform bar;
    public float progress = 0.0f;

	// Use this for initialization
	void Start () {
        bar.localScale = new Vector2(progress, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
        bar.localScale = new Vector2(progress, 1.0f);
	}

    public void addProgress(float newProgress)
    {
        progress = Mathf.Clamp(progress + newProgress, 0.0f, 1.0f);
    }

    public void setProgress(float newProgress)
    {
        progress = Mathf.Clamp(newProgress, 0.0f, 1.0f);
    }

    public void resetProgress()
    {
        progress = 0;
    }

    public bool isFull()
    {
        return progress == 1.0f;
    }
}
