using System;

[Serializable]
public class Stop : ExplorationToolKit
{
    /**
    * Stop Control Methods
    **/
    public void BeenClicked()
    {
        //Scale Components
        if (ExTK.scaleData.scaleEnable)
        {
            ExTK.model.GetComponent<Scale>().StopScale();
        }
        //Rotate Components
        if (ExTK.rotateData.rotateEnable)
        {
            ExTK.model.GetComponent<Rotate>().StopRotate();
        }
        //Moving Components
        if (ExTK.moveData.moveEnable)
        {
            ExTK.model.GetComponent<Move>().StopMove();
        }
        //Animation Components
        if (ExTK.animationsEnable)
        {
            ExTK.model.GetComponent<Animations>().PauseAnim();
        }
    }
}

