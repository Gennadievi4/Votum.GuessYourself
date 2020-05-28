using System.Collections.Generic;

namespace GuessYouSelf.Core
{
    public partial class QuestionView
    {
        public QuestionView(List<string> questions)
        {
            InitializeComponent();
            lv.ItemsSource = questions;
        }
    }
}
