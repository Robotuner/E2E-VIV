using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Election.Models
{
    public class Command : ICommand
    {
        private Action<object> action;
        private Func<object, bool> canExecute;
        private Func<object, Task> func;

        public Command(Action<object> action, Func<object,bool> canExecute = null)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public Command(Func<object, Task> action, Func<object, bool> canExecute = null)
        {
            this.func = action;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Wires CanExecuteChanged event 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Forcess checking if execute is allowed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            if (action != null)
            {
                action(parameter);
            }
            if (func != null)
            {
                func(parameter);
            }
        }
    }
}
