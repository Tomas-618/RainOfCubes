using UnityEngine;
using AYellowpaper;

public class LifeTimersFabric : Fabric<LifeTimer>
{
    [SerializeField] private InterfaceReference<ICanOnlyPutOutInPosition, MonoBehaviour> _bombsPool;

    public override LifeTimer Create()
    {
        LifeTimer entity = base.Create();

        entity.Init(_bombsPool.Value);

        return entity;
    }
}
