using DynamicData;
using MefCalculator.Editors.Integer;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;

namespace MefCalculator.Gui.Nodes.IntegerConstant
{
    public class IntegerConstantNodeViewModel : NodeViewModel
    {
        static IntegerConstantNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<IntegerConstantNodeViewModel>));
        }

        public IntegerValueEditorViewModel ValueEditor { get; } = new IntegerValueEditorViewModel();

        public ValueNodeOutputViewModel<int?> Output { get; }

        public IntegerConstantNodeViewModel()
        {
            this.Name = "Constant";

            Output = new ValueNodeOutputViewModel<int?>
            {
                Name = "Value",
                Editor = ValueEditor,
                Value = this.WhenAnyValue(vm => vm.ValueEditor.Value)
            };
            this.Outputs.Add(Output);
        }
    }
}
