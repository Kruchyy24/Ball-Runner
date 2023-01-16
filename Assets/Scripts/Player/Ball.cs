using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            Destroy(other.gameObject, 10f);
        }
        
        if (other.gameObject.CompareTag("Diamond"))
        {
            GameManager.Instance.UpdateDiamond();
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("Spike"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
