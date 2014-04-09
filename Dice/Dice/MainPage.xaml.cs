using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;

namespace Dice
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string[] diceUri;
        Random rnd;
        private int PlayerSum;
        private int DealerSum;
        private int Dice1Temp;
        private int Dice2Temp;
        private int IntTimes6;
        // Constructor
        public MainPage()
        {
            diceUri = new string[6];
            rnd = new Random();
            PlayerSum = 0;
            DealerSum = 0;
            diceUri[0] = "images/1.jpg";
            diceUri[1] = "images/2.jpg";
            diceUri[2] = "images/3.jpg";
            diceUri[3] = "images/4.jpg";
            diceUri[4] = "images/5.jpg";
            diceUri[5] = "images/6.jpg";
            Dice1Temp = 0;
            Dice2Temp = 0;
            IntTimes6=6*123456789;
            InitializeComponent();
        }
        

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            Dice1Temp = rnd.Next(IntTimes6);
            Dice2Temp = rnd.Next(IntTimes6);

            //usunac potem
            Rank1.Text = Dice1Temp.ToString();
            Rank2.Text = Dice2Temp.ToString();
            Rank3.Text = (IntTimes6 % 6).ToString();

            Dice1.Source = new BitmapImage(new Uri(@diceUri[Dice1Temp%6], UriKind.RelativeOrAbsolute));
            Dice2.Source = new BitmapImage(new Uri(@diceUri[Dice2Temp%6], UriKind.RelativeOrAbsolute));
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}