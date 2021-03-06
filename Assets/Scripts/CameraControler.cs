﻿using System.Collections;
using UnityEngine;
public class CameraControler : MonoBehaviour 
{
    private static CameraControler _instance;
    public float MoveSpeed;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public static void MoveTo(Vector3 position)
    {
        _instance.StartCoroutine(_instance.MoveToCoroutine(position));
    }

    private IEnumerator MoveToCoroutine(Vector3 position)
    {
        while ((transform.position - position).magnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, MoveSpeed*Time.deltaTime);
            yield return null;
        }
    }
}
