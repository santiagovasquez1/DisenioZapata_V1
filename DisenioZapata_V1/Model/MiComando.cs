using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DisenioZapata_V1.Model
{
    public class MiComando : ICommand
    {
        Action Action;
        public MiComando(Action action)
        {
            this.Action = action;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
           return true;
        }

        public void Execute(object parameter)
        {
            Action();
        }
    }
}
