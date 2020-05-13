using NodeNetwork.ViewModels;

namespace MefCalculator.PluginFactory
{
    public abstract class NodePluginFactory
    {
        public abstract NodeViewModel CreatePlugin();
    }
}
