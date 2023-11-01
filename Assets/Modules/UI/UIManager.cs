using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager I { get; private set; }
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
