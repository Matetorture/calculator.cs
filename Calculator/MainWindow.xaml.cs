using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private double currentValue = 0;
        private double lastValue = 0;
        private string operation = "";
        private bool isNewEntry = true;
        private string history = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (isNewEntry || display.Content.ToString() == "0")
                {
                    display.Content = button.Content.ToString();
                    isNewEntry = false;
                }
                else
                {
                    display.Content += button.Content.ToString();
                }
            }
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (!isNewEntry)
                {
                    Button_Click_Equals(null, null);
                }
                lastValue = double.Parse(display.Content.ToString(), CultureInfo.InvariantCulture);
                operation = button.Content.ToString();

                history += display.Content.ToString() + " " + operation + " ";
                historyDisplay.Content = history;

                isNewEntry = true;
            }
        }

        private void Button_Click_Equals(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(operation) && !isNewEntry)
            {
                currentValue = double.Parse(display.Content.ToString(), CultureInfo.InvariantCulture);
                switch (operation)
                {
                    case "+":
                        currentValue = lastValue + currentValue;
                        break;
                    case "-":
                        currentValue = lastValue - currentValue;
                        break;
                    case "*":
                        currentValue = lastValue * currentValue;
                        break;
                    case "/":
                        currentValue = (currentValue != 0) ? lastValue / currentValue : double.NaN;
                        break;
                    case "%":
                        currentValue = lastValue % currentValue;
                        break;
                    case "x^y":
                        currentValue = Math.Pow(lastValue, currentValue);
                        break;
                }
                
                history += display.Content.ToString() + " = ";

                display.Content = currentValue.ToString(CultureInfo.InvariantCulture);

                historyDisplay.Content = history;

                lastValue = currentValue;
                operation = "";
                isNewEntry = true;
            }
        }

        private void Button_Click_C(object sender, RoutedEventArgs e)
        {
            display.Content = "0";

            historyDisplay.Content = "";
            history = "";

            currentValue = 0;
            lastValue = 0;
            operation = "";
            isNewEntry = true;
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            string text = display.Content.ToString();
            display.Content = text.Length > 1 ? text.Substring(0, text.Length - 1) : "0";
        }

        private void Button_Dot(object sender, RoutedEventArgs e)
        {
            if (!display.Content.ToString().Contains("."))
            {
                display.Content += ".";
                isNewEntry = false;
            }
        }
    }
}