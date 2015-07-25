using UnityEngine;
public class YouDiedScreen : MonoBehaviour 
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Application.LoadLevel("1");
        }
    }
}
