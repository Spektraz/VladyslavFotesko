using UnityEngine;
using Zenject;

public abstract class View<TModel, TController> : MonoBehaviour
    where TController : IController<TModel>
{
    [SerializeField] protected TModel m_viewModel;

    [Inject] protected TController m_controller;
    protected virtual void OnDestroy()
    {
        m_controller.Dispose();
    }
}