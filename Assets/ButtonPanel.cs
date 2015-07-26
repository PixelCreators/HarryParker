using UnityEngine;
using UnityEngine.UI;

public class ButtonPanel : MonoBehaviour
{
    public Text zero, zero_1, zero_2, zero_3;
    public int[] votes;
    

    void Update()
    {
        zero.text = votes[0].ToString();
        zero_1.text = votes[1].ToString();
        zero_2.text = votes[2].ToString();
        zero_3.text = votes[3].ToString();
    }
}
