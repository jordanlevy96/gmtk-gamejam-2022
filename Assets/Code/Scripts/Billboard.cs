using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    private Camera gameCamera;
    // Start is called before the first frame update
    void Start()
    {
        gameCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(gameCamera.transform);

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
