using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using MefCalculator.Gui.Nodes.IntegerOutput;
using MefCalculator.PluginFactory;
using NodeNetwork;
using NodeNetwork.Toolkit;
using NodeNetwork.Toolkit.NodeList;
using NodeNetwork.ViewModels;
using ReactiveUI;

namespace MefCalculator.Gui
{
    public class MainViewModel : ReactiveObject
    {
        static MainViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<MainViewModel>));
        }

        public NodeListViewModel ListViewModel { get; } = new NodeListViewModel();
        public NetworkViewModel NetworkViewModel { get; } = new NetworkViewModel();

        #region ValueLabel
        private string _valueLabel;
        public string ValueLabel
        {
            get => _valueLabel;
            set => this.RaiseAndSetIfChanged(ref _valueLabel, value);
        }
        #endregion

        public MainViewModel()
        {
            var loader = new GenericMefPluginLoader<NodePluginFactory>("Plugins\\Nodes");
            foreach (var plugin in loader.Plugins)
            {
                ListViewModel.AddNodeType(() => plugin.Value.CreatePlugin());
            }

            var integerOutput = new IntegerOutputNodeViewModel();
            NetworkViewModel.Nodes.Add(integerOutput);

            NetworkViewModel.Validator = network =>
            {
                var containsLoops = GraphAlgorithms.FindLoops(network).Any();
                if (containsLoops)
                {
                    return new NetworkValidationResult(false, false, new ErrorMessageViewModel("Network contains loops!"));
                }

                //bool containsDivisionByZero = GraphAlgorithms.GetConnectedNodesBubbling(integerOutput)
                //    .OfType<DivisionNodeViewModel>()
                //    .Any(n => n.Input2.Value == 0);
                //if (containsDivisionByZero)
                //{
                //    return new NetworkValidationResult(false, true, new ErrorMessageViewModel("Network contains division by zero!"));
                //}

                return new NetworkValidationResult(true, true, null);
            };

            integerOutput.ResultInput.ValueChanged
                .Select(v => (NetworkViewModel.LatestValidation?.IsValid ?? true) ? v.ToString() : "Error")
                .BindTo(this, vm => vm.ValueLabel);
        }
    }
}
