using Scriptable;
using UnityEngine;
public class Brick : ColorObj
{
    public Stage stage;

    public void OnDesSpawn()
    {
        stage.RemoveBrick(this);
    }
}
