using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Vector3 offset;
    bool Shaker1 = true;
    bool Shaker2 = false;
    

    // Update is called once per frame
    void Update()
    {
       
        if (Shaker1 == true)
        
        {
            transform.position = player1.position + offset;

            if (Input.GetKeyDown("f"))
            {
                SwitchP1();
            }

        }

        if (Shaker2 == true)
        
        {
            transform.position = player2.position + offset;

            if (Input.GetKeyDown("k"))
            {
                SwitchP2();
            }
        }
    }

    void SwitchP1()
    {
        
        Debug.Log("f_pressed & shaker to p2");
        Shaker2 = true;
        Shaker1 = false;
    }

    void SwitchP2()
    {
        
        Debug.Log("k_pressed & shaker to p1");
        Shaker1 = true;
        Shaker2 = false;
    }

}
