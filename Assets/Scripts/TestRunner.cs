using UnityEngine;
public class TestRunner : MonoBehaviour 
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Run();
        }
    }

    public Decision testDecision;

    void Run()
    {
    }
}
