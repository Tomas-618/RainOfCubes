using UnityEngine;
using AYellowpaper;

public class LifeTimersFabric : Fabric<LifeTimer>
{
    [SerializeField] private InterfaceReference<ICanOnlyPutOutInPosition, MonoBehaviour> _bombsSpawner;

    public override LifeTimer Create()
    {
        LifeTimer entity = base.Create();

        entity.Init(_bombsSpawner.Value);

        return entity;
    }
}
