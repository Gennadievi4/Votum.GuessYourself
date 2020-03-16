﻿using System;
using System.Windows.Input;

namespace Guess.Yourself
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public abstract bool CanExecute(object parametr);

        public abstract void Execute(object parametr);
    }
}