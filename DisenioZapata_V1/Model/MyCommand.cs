using System;
using System.Windows.Input;

namespace DisenioZapata_V1.Model
{
    public class MyCommand : ICommand
    {
        private Action action;
        private Func<bool> canExecute;

        public MyCommand(Action action) : this(action , () => true)
        {
        }

        public MyCommand(Action action , Func<bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public void Execute(object parameter)
        {
            action();
        }

        public void ReevaluateCanExecute()
        {
            CanExecuteChanged?.Invoke(this , EventArgs.Empty);
        }
    }
}