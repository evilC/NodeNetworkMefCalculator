using System.ComponentModel.Composition;
using MefCalculator.PluginFactory;
using NodeNetwork.ViewModels;

namespace MefCalculator.Plugins.Nodes.Sum
{
    [Export(typeof(NodePluginFactory))]
    [ExportMetadata("NodeName", "Sum")]
    public class PluginFactory : NodePluginFactory
    {
        public override NodeViewModel CreatePlugin()
        {
            return new SumNodeViewModel();
        }
    }
}
