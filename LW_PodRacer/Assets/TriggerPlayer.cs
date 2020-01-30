using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayer : MonoBehaviour
{
    public GameManager m_GameManager;
    public GameEndStatus enum_EndGameType = GameEndStatus.win;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_GameManager.endGame(enum_EndGameType);
        }
    }
}
