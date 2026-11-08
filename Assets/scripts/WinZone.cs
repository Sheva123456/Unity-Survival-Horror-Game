using UnityEngine;

public class WinZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { 
            GameManager.Instance.SetState(GameState.Win);
            Debug.Log("you waun");
        }
    }
}