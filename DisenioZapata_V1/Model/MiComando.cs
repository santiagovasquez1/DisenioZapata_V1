using System;
using System.Windows.Input;

namespace DisenioZapata_V1.Model
{
    [Serializable]
    public class MiComando : ICommand
    {
        private Action Action;
        private Func<bool> canExecute;

        public MiComando(Action action) : this(action, () => true)
        {
        }

        public MiComando(Action action, Func<bool> canExecute)
        {
            this.Action = action;
            this.canExecute = canExecute;
        }

        [field: NonSerialized]
        public event EventHandler CanExecuteChanged;

        public void ReevaluateCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public void Execute(object parameter)
        {
            Action();
        }
    }
}