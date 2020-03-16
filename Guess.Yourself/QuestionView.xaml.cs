using System.Collections.Generic;
using System.Windows;

namespace Guess.Yourself
{
    public partial class QuestionView : Window
    {
        public QuestionView(List<string> questions)
        {
            InitializeComponent();
            lv.ItemsSource = questions;
        }
    }
}
