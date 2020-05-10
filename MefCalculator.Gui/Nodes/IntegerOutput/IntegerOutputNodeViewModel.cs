using DynamicData;
using MefCalculator.Editors.Integer;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;

namespace MefCalculator.Gui.Nodes.IntegerOutput
{
    public class IntegerOutputNodeViewModel : NodeViewModel
    {
        static IntegerOutputNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<IntegerOutputNodeViewModel>));
        }

        public ValueNodeInputViewModel<int?> ResultInput { get; }

        public IntegerOutputNodeViewModel()
        {
            Name = "Output";

            this.CanBeRemovedByUser = false;

            ResultInput = new ValueNodeInputViewModel<int?>
            {
                Name = "Value",
                Editor = new IntegerValueEditorViewModel()
            };
            this.Inputs.Add(ResultInput);
        }
    }
}
