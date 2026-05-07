using UnityEngine;
public class EchoManager : MonoBehaviour
{
    public static EchoManager Instance;
    public int echoesAlive = 3;

    void Awake() { Instance = this; }

    public void EchoKilled()
    {
        echoesAlive--;
        echoesAlive = Mathf.Max(0, echoesAlive);
    }
}