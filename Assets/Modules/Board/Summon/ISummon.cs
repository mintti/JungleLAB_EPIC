using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISummon {
    int Index { get; }
    IEnumerator OnTurnEnd();
    IEnumerator Connect();
    void OnEvent();
    void OnPass();
}
