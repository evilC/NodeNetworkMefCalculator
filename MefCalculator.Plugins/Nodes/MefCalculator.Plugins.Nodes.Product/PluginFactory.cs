using System.ComponentModel.Composition;
using MefCalculator.PluginFactory;
using NodeNetwork.ViewModels;

namespace MefCalculator.Plugins.Nodes.Product
{
    [Export(typeof(NodePluginFactory))]
    [ExportMetadata("NodeName", "Product")]
    public class PluginFactory : NodePluginFactory
    {
        public override NodeViewModel CreatePlugin()
        {
            return new ProductNodeViewModel();
        }
    }

}
