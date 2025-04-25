using Zenject;

namespace Assets.Scripts.Application
{
    public class ApplicationContainer : IInitializable
    {
        private readonly GlobalConst _globalConst;
        [Inject]
        public ApplicationContainer()
        {
            _globalConst = new GlobalConst();
        }
        public GlobalConst GlobalConst => _globalConst;

        public void Initialize()
        {

        }
    }
}
