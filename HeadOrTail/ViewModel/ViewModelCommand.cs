using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HeadOrTail.ViewModel
{
    class ViewModelCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return Predicate(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Action(parameter);
        }

        public Func<object, bool> Predicate { get; set; }

        public Action<object> Action { get; set; }

        public ViewModelCommand(INotifyPropertyChanged property, string propName)
            : this(property, new List<string>() { propName })
        {
        }

        public ViewModelCommand()
        {
        }

        public ViewModelCommand(INotifyPropertyChanged property, List<string> propsName)
        {
            property.PropertyChanged += (o, e) =>
            {
                foreach (string propName in propsName)
                {
                    if (e.PropertyName.Equals(propName))
                    {
                        if (CanExecuteChanged != null)
                            CanExecuteChanged(this, e);
                    }
                }
            };
        }

    }
}
