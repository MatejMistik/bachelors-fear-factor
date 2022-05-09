using UnityEngine;
using SensorToolkit;

public class AreAliveAlliesNearby : Node
{
    private Sensor sensor;
    private int alliesDeadCounter;

    public AreAliveAlliesNearby(Sensor sensor)
    {
        this.sensor = sensor;
    }

    public override NodeState Evaluate()
    {
        foreach(GameObject detectedObject in sensor.DetectedObjects)
        {
            if (detectedObject.name == "Enemy" && detectedObject.CompareTag("DeadEnemy"))
            {
                alliesDeadCounter++;
            }
        }
        if(alliesDeadCounter > 0)
        { 
            alliesDeadCounter = 0;
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
