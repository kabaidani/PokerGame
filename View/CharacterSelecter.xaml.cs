using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PokerGame.view
{
    /// <summary>
    /// Interaction logic for CharcterSelecter.xaml
    /// </summary>
    public partial class CharcterSelecter : Window
    {
        public CharcterSelecter()
        {
            InitializeComponent();
        }

        private void TextBlock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        public static bool IsValid(string str)
        {
            return !_regex.IsMatch(str);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == Key.Return)
                {
                    if (e.Key == Key.Enter)
                    {
                        textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    }
                }
            }
        }
    }
}
