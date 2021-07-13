using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WPFKeyboardTrainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        int difficultyLevel = 1;
        bool capslockIsPressed = false;
        string allLetters;
        double numOfChars = 0;
        DateTime startTime;
        int speed=0;
        int fails=0;
        
        public MainWindow()
        {
            InitializeComponent();
            stopButton.IsEnabled = false;
            textBox1.Text = "Hi! Click start..";
        }
        bool isEnd()
        {
            return textBox1.Text.Length <= textBox2.Text.Length;
        }
        void doCalculation()
        {
            int len = textBox1.Text.Length > textBox2.Text.Length ? textBox2.Text.Length : textBox1.Text.Length;
            for (int i = 0; i < len; i++)
            {
                if (textBox1.Text[i] != textBox2.Text[i])
                {
                    fails++;
                }
            }
            speed =(int) (numOfChars/((DateTime.Now - startTime).TotalSeconds/60));


        }
        void EndSession()
        {
            
            startButton.IsEnabled = true;
            stopButton.IsEnabled = false;
            doCalculation();
            speedLabel.Content = $"Speed: {speed} chars/min";
            failsLabel.Content = $"Fails: {fails}";
            MessageBox.Show("The End! Check out your results! ", "end", MessageBoxButton.OK, MessageBoxImage.Information);
           
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
            foreach (var item in buttonsGrid.Children)
            {
                if (item is Grid)
                {


                    foreach (var it in (item as Grid).Children)
                    {
                        if (e.Key.ToString()== (it as Button).Name.ToString())
                        {
                           
                            (it as Button).Opacity = 0.5;
                         
                            if (e.Key==Key.LeftShift||e.Key==Key.RightShift)
                            {
                                if (capslockIsPressed)
                                {
                                    ToLowerButtons();
                                }
                                else
                                ToUpperButtons();

                                CapitalSymbols();

                            }
                            else if (e.Key == Key.CapsLock)
                            {
                                if (capslockIsPressed)
                                {
                                    ToLowerButtons();
                                    capslockIsPressed = false;
                                }
                                else
                                {
                                    capslockIsPressed = true;
                                    ToUpperButtons();
                                }
                            }
                            if (!(e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.Tab || e.Key == Key.CapsLock || e.Key == Key.Enter || e.Key == Key.Back || e.Key == Key.Space))
                            {
                                textBox2.Text += (it as Button).Content;
                                numOfChars++;
                            }
                            if (e.Key==Key.Space)
                            {
                                textBox2.Text += " ";
                                numOfChars++;
                            }
                            if (e.Key==Key.Back)
                            {
                                textBox2.Text= textBox2.Text.Remove(textBox2.Text.Length - 1);
                               
                            }
                            if (isEnd() && startButton.IsEnabled == false)
                            {
                                EndSession();
                            }
                        }
                    }
                }

               

            }

        }

       
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (var item in buttonsGrid.Children)
            {
                if (item is Grid)
                {


                    foreach (var it in (item as Grid).Children)
                    {
                        if (e.Key.ToString() == (it as Button).Name.ToString())
                        {
                            (it as Button).Opacity = 1;
                            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                            {
                                LowerSymbols();
                                if (capslockIsPressed)
                                {
                                    ToUpperButtons();
                                }
                                else ToLowerButtons();
                            }

                        }

                    }
                }
            }
        }
       

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            difficultyLevel =(int) e.NewValue;
            difficultyLabel.Content =$"Difficulty: {difficultyLevel}";
        }


        private void ToUpperButtons()
        {
            foreach (var item in buttonsGrid.Children)
            {
                if (item is Grid)
                {


                    foreach (var it in (item as Grid).Children)
                    {
                        if ((it as Button).Content.ToString().Length < 2)
                        {
                            (it as Button).Content = (it as Button).Content.ToString().ToUpper();
                        }
                    }
                }
            }


        }
        private void ToLowerButtons()
        {
            foreach (var item in buttonsGrid.Children)
            {
                if (item is Grid)
                {


                    foreach (var it in (item as Grid).Children)
                    {
                        if ((it as Button).Content.ToString().Length < 2)
                        {
                            (it as Button).Content = (it as Button).Content.ToString().ToLower();
                        }
                    }
                }
            }


        }
        private void CapitalSymbols()
        {
            Oem3.Content = "~";
            D1.Content = "!";
            D2.Content = "@";
            D3.Content = "#";
            D4.Content = "$";
            D5.Content = "%";
            D6.Content = "^";
            D7.Content = "&";
            D8.Content = "*";
            D9.Content = "(";
            D0.Content = ")";
            OemMinus.Content = "_";
            OemPlus.Content = "+";
            OemOpenBrackets.Content = "{";
            Oem6.Content = "}";
            Oem5.Content = "|";
            Oem1.Content = ":";
            OemQuotes.Content = "\"";
            OemComma.Content = "<";
            OemPeriod.Content = ">";
            OemQuestion.Content = "?";
        }
        private void LowerSymbols()
        {
            Oem3.Content = "`";
            D1.Content = "1";
            D2.Content = "2";
            D3.Content = "3";
            D4.Content = "4";
            D5.Content = "5";
            D6.Content = "6";
            D7.Content = "7";
            D8.Content = "8";
            D9.Content = "9";
            D0.Content = "0";
            OemMinus.Content = "-";
            OemPlus.Content = "=";
            OemOpenBrackets.Content = "[";
            Oem6.Content = "]";
            Oem5.Content = "\\";
            Oem1.Content = ";";
            OemQuotes.Content = "'";
            OemComma.Content = ",";
            OemPeriod.Content = ".";
            OemQuestion.Content = "/";
        }

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public string Shuffle(string str)
        {
            char[] array = str.ToCharArray();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k;
                lock (syncLock)
                { // synchronize
                    k = random.Next(n + 1);
                }
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }
        private void GenerateRandomString()
        {
           
            string text = string.Empty;
            for (int i = 0; i < difficultyLevel+1; i++)
            {
                text += Shuffle(allLetters).Substring(0, difficultyLevel + 3);
                if (i != difficultyLevel)
                {
                    text += " ";
                }
            }
            textBox1.Text = text;

        }
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            numOfChars = 0;
            startTime = DateTime.Now;
            textBox1.Text = "";
            textBox2.Text = "";
            fails = 0;
            speed = 0;
            startButton.IsEnabled = false;
            stopButton.IsEnabled = true;
            if (caseCheckBox.IsChecked==true)
            {
                allLetters = "`~1234567890-=_+qwertyuiop[]{}\\|asdfghjkl;':\"zxcvbnm,./<>?QWERTYUIOPLKJHGFDSAZXCVBNM";
            }
            else
            {
                allLetters = "`~1234567890-=_+qwertyuiop[]{}\\|asdfghjkl;':\"zxcvbnm,./<>?qwertyuiopasdfghjklzxcvbnm";
            }
            GenerateRandomString();
            speedLabel.Content = "Speed: 0 chars/min";
            failsLabel.Content = "Fails: 0";
            
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            EndSession();
        }
    }
}
