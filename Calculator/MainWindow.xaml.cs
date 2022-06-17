using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SelectedOperator selectedOperator;
        double lastNumber, result;

        public MainWindow()
        {
            InitializeComponent();
            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            EqualButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch(selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Substraction:
                        result = SimpleMath.Subtract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                }

                resultLabel.Content = result.ToString();
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tmp;
            if (double.TryParse(resultLabel.Content.ToString(), out tmp))
            {
                tmp = tmp / 100;
                if (lastNumber != 0)
                    tmp *= lastNumber;
                resultLabel.Content = tmp.ToString();
            }
        }

        //50 + 5% (2.5)
        //80 + 10% (8) = (88)

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            result = 0;
            lastNumber = 0;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }
            if (sender == MultiplicationButton)
                selectedOperator = SelectedOperator.Multiplication;
            if (sender == DivisionButton)
                selectedOperator = SelectedOperator.Division;
            if (sender == PlusButton)
                selectedOperator = SelectedOperator.Addition;
            if (sender == MinusButton)
                selectedOperator = SelectedOperator.Substraction;
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (resultLabel.Content.ToString().Contains("."))
            {
                return;
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}.";

            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
            int selectedValue =int.Parse((sender as Button).Content.ToString());
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Substraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Add(double n1, double n2)  { return n1 + n2; }
        public static double Subtract(double n1, double n2) { return n1 - n2; }
        public static double Multiply(double n1, double n2) { return n1 * n2; }
        public static double Divide(double n1, double n2) 
        {
            if (n2 == 0)
            {
                MessageBox.Show("Division by 0 is not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return n1 / n2;
        }




    }
}
