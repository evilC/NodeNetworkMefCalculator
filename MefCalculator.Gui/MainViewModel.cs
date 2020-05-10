using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using MefCalculator.Gui.Nodes.IntegerOutput;
using NodeNetwork;
using NodeNetwork.Toolkit;
using NodeNetwork.Toolkit.NodeList;
using NodeNetwork.ViewModels;
using ReactiveUI;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MefCalculator.Gui
{
    public class MainViewModel : ReactiveObject
    {
        [ImportMany(typeof(NodeViewModel))]
        private IEnumerable<Lazy<NodeViewModel>> _nodePlugins;

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
            var catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.SatisfyImportsOnce(this);
            container.ComposeParts(this);

            foreach (var plugin in _nodePlugins)
            {
                ListViewModel.AddNodeType(() => plugin.Value);
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
