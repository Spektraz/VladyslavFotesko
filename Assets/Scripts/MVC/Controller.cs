using System;
using Zenject;
public abstract class Controller<TModel> : IController<TModel>, IInitializable, IDisposable
{
    protected readonly TModel m_viewModel;

    [Inject]
    public Controller(TModel viewModel)
    {
        m_viewModel = viewModel;
    }

    public void Initialize()
    {
        OnInitialize();
        BindEvents();
    }

    public void Dispose()
    {
        UnbindEvents();
        OnDispose();
    }

    protected virtual void OnInitialize() { }
    protected virtual void OnDispose() { }
    protected virtual void BindEvents() { }
    protected virtual void UnbindEvents() { }
}