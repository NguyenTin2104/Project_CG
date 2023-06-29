using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xMargin = 1f;
    public float xSmooth = 8f;
    public float ySmooth = 8f;
    public Vector2 maxXAndY;
    public Vector2 minXAndY;

    private Transform m_Player;

    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private bool CheckMargin()
    {
        return (transform.position.x - m_Player.position.x) < xMargin;
    }

    private void Update()
    {
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (CheckMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, m_Player.position.x, xSmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        /*targetX = Mathf.Clamp(targetY, maxXAndY.y, minXAndY.y);*/

        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }

}
