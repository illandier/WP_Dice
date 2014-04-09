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
        private int[] diceRoll;
        Random rnd;
        private int PlayerSum;
        private int DealerSum;
        private int Dice1Temp;
        private int Dice2Temp;
        private int IntTimes6;
        private bool TestPossible;
        private bool EndTestPossible;
        private bool FirstRoll;
        private int MatchResult;//0-nie skonczono, 1-wygrana, 2-remis, 3-przegrana
        private int Wins;
        // Constructor
        public MainPage()
        {
            Restart();
            InitializeComponent();
        }

        private void Restart()
        {
            Wins = 0;
            MatchResult = 0;
            TestPossible = false;
            EndTestPossible = false;
            FirstRoll = true;
            diceUri = new string[6];
            rnd = new Random();
            PlayerSum = 0;
            DealerSum = 0;
            diceRoll[0] = 1;
            diceRoll[1] = 2;
            diceRoll[2] = 3;
            diceRoll[3] = 4;
            diceRoll[4] = 5;
            diceRoll[5] = 6;
            diceUri[0] = "images/1.jpg";
            diceUri[1] = "images/2.jpg";
            diceUri[2] = "images/3.jpg";
            diceUri[3] = "images/4.jpg";
            diceUri[4] = "images/5.jpg";
            diceUri[5] = "images/6.jpg";
            Dice1Temp = 0;
            Dice2Temp = 0;
            IntTimes6 = 6 * 123456789;
            Check.Visibility = Visibility.Collapsed;
            RollButton.Visibility = Visibility.Visible;
        }

        private void CheckIfWin()
        {
            if (PlayerSum == 11)
            {
                MatchResult = 1;
                Wins++;
            }
            if (PlayerSum > 11)
            {
                MatchResult = 3;
            }
            if (MatchResult == 0 && PlayerSum > DealerSum && EndTestPossible==true)
            {
                MatchResult = 1;
                Wins++;
            }
            if (MatchResult == 0 && PlayerSum == DealerSum && EndTestPossible == true)
            {
                MatchResult = 2;
            }
            if (MatchResult == 0 && PlayerSum < DealerSum && EndTestPossible == true)
            {
                MatchResult = 3;

            }
            if (MatchResult != 0)
            {
                Check.Visibility = Visibility.Collapsed;
                RollButton.Visibility = Visibility.Collapsed;
            }

            PlayerWins.Text = Wins.ToString();
            PlayerCount.Text = PlayerSum.ToString();
            DealerCount.Text = DealerSum.ToString();

        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstRoll)
            {
                Dice1Temp = rnd.Next(IntTimes6);
                Dice2Temp = rnd.Next(IntTimes6);

                //usunac potem
                Rank1.Text = Dice1Temp.ToString();
                Rank2.Text = Dice2Temp.ToString();
                Rank3.Text = (IntTimes6 % 6).ToString();

                Dice1.Source = new BitmapImage(new Uri(@diceUri[Dice1Temp % 6], UriKind.RelativeOrAbsolute));
                Dice2.Source = new BitmapImage(new Uri(@diceUri[Dice2Temp % 6], UriKind.RelativeOrAbsolute));
                FirstRoll = false;
                PlayerSum = diceRoll[(Dice1Temp % 6)] + diceRoll[(Dice2Temp % 6)];
                Check.Visibility = Visibility.Visible;
            }





        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            if (TestPossible)
            {

            }
        }

       

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }
    }
}