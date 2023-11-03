using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISummon {

    IEnumerator OnTurnEnd();
    IEnumerator Connect();
}
