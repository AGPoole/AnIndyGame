using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IdolCollectable : Collectable {
    public int winPageIndex;

    protected override void CollectedAction()
    {
        SceneManager.LoadScene(winPageIndex);
        base.CollectedAction();
    }
}
