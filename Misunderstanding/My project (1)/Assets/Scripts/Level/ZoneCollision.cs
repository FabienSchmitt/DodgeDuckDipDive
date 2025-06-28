using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneCollision : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] string UpOrDown = "Up";


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger : " + "Level" + UpOrDown);
;        if (collision.gameObject.tag == "Player")
        {
            var scene = SceneManager.GetSceneByName("Level" + UpOrDown);
            SceneManager.LoadScene("Level" + UpOrDown, LoadSceneMode.Single);
        }
    }
}
