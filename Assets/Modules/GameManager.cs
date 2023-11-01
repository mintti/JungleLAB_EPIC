using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager I { get; private set; }
    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
