using UnityEngine;
using UnityEngine.SceneManagement;

public class Step : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("UpRes");
    }
}
