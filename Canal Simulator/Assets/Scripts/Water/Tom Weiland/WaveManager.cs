using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public float amplitude = 1f;
    public Vector2 size;
    public float speed = 1f;
    public Vector2 offset;

    private float xSpeed;
    private float ySpeed;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        xSpeed = Random.Range(-1, 1);
        ySpeed = Random.Range(-1, 1);
        if(xSpeed == 0)
        {
            xSpeed = 1;
        }
        if (ySpeed == 0)
        {
            ySpeed = 1;
        }
    }

    private void Update()
    {
        offset.x += Time.deltaTime * speed * xSpeed;
        offset.y += Time.deltaTime * speed * ySpeed;
    }

    public float GetWaveHeight(float x, float y)
    {
        return Mathf.PerlinNoise((x / size.x) + offset.x, (y / size.y) + offset.y) * amplitude;
    }
}
