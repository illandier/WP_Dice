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
        private bool DealerTurn;
        private int MatchResult;//0-nie skonczono, 1-wygrana, 2-remis, 3-przegrana
        private int Wins;
        private string ResultInfo;
        private List<PlayerResult> Ranking;
        // Constructor
        public MainPage()
        {
            Ranking = new List<PlayerResult>();

            InitializeComponent();
            Wins = 0;
            Restart();
        }

        private void Restart()
        {
            if (Ranking.Count == 0)
            {
                Rank1.Text = "";
                Rank2.Text = "";
                Rank3.Text = "";

            }
            if (Ranking.Count > 0)
            {
                Rank1.Text = Ranking.ElementAt(0).PlayerName + " won: " + Ranking.ElementAt(0).Wins;
                
            }
            if (Ranking.Count > 1)
            {
                Rank2.Text = Ranking.ElementAt(1).PlayerName + " won: " + Ranking.ElementAt(1).Wins;
                
            }
            if (Ranking.Count > 2)
            {
                Rank3.Text = Ranking.ElementAt(2).PlayerName + " won: " + Ranking.ElementAt(2).Wins;
            }
            
            if (MatchResult == 0 && DealerTurn==false)
            {
                Wins = 0;
            }
            MatchResult = 0;
            TestPossible = false;
            EndTestPossible = false;
            FirstRoll = true;
            diceUri = new string[6];
            diceRoll = new int[6];
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
            ResultInfo = "";
            DealerTurn = false;
            CheckButton.Visibility = Visibility.Collapsed;
            RollButton.Visibility = Visibility.Visible;
            Dice2.Visibility = Visibility.Visible;
            InfoBox.Visibility = Visibility.Collapsed;
            CheckButton.Content = "End player turn";
            PlayerCount.Text = PlayerSum.ToString();
            DealerCount.Text = DealerSum.ToString();
            if (Wins == 0)
            {
                PlayerWins.Text = Wins.ToString();
            }
        }



        private void AddToRank()
        {
            PlayerResult result = new PlayerResult();
            result.Wins = Wins;
            result.PlayerName = PlayerNameBox.Text;
            result.WinDate = DateTime.Now;
            

            for (int i = 0; i < Ranking.Count; i++)
            {
                if (result.Wins > Ranking.ElementAt(i).Wins)
                {
                    Ranking.Insert(i, result);
                    break;
                }
                if (result.Wins <= Ranking.ElementAt(i).Wins)
                {
                    Ranking.Insert(i+1, result);
                    break;
                }
                
            }
            if (Ranking.Count == 0)// || ((result.Wins<=Ranking.ElementAt(Ranking.Count-1).Wins) && (Ranking.Count < 3))
            {
                Ranking.Add(result);
            }
            if (Ranking.Count >= 4)
            {
                Ranking.RemoveAt(3);
            }
            
        }

        private void CheckIfWin()
        {
            PlayerWins.Text = Wins.ToString();
            PlayerCount.Text = PlayerSum.ToString();
            DealerCount.Text = DealerSum.ToString();
            if (PlayerSum == 11 || DealerSum>11)
            {
                MatchResult = 1;
                ResultInfo = PlayerNameBox.Text + " wins";
                Wins++;
                PlayerWins.Text = Wins.ToString();
            }
            if (PlayerSum > 11 || DealerSum==11)
            {
                MatchResult = 3;
                ResultInfo = PlayerNameBox.Text + " loses";
                PlayerWins.Text = Wins.ToString();
                AddToRank();
                Wins = 0;
            }
            if (MatchResult == 0 && PlayerSum > DealerSum && EndTestPossible==true)
            {
                MatchResult = 1;
                ResultInfo = PlayerNameBox.Text + " wins";
                Wins++;
                PlayerWins.Text = Wins.ToString();
            }
            if (MatchResult == 0 && PlayerSum == DealerSum && EndTestPossible == true)
            {
                MatchResult = 2;
                ResultInfo = "Draw";
            }
            if (MatchResult == 0 && PlayerSum < DealerSum && EndTestPossible == true)
            {
                MatchResult = 3;
                ResultInfo = PlayerNameBox.Text + " loses";
                PlayerWins.Text = Wins.ToString();
                AddToRank();
                Wins = 0;
            }
            if (MatchResult != 0)
            {
                CheckButton.Visibility = Visibility.Collapsed;
                RollButton.Visibility = Visibility.Collapsed;
                InfoBox.Text = ResultInfo;
                InfoBox.Visibility = Visibility.Visible;
                
            }

            

        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstRoll==false && DealerTurn==false)
            {
                Dice2.Visibility = Visibility.Collapsed;
                Dice1Temp = rnd.Next(IntTimes6);
                Dice1.Source = new BitmapImage(new Uri(@diceUri[Dice1Temp % 6], UriKind.RelativeOrAbsolute));
                PlayerSum += diceRoll[(Dice1Temp % 6)];
            }

        
            if (FirstRoll==true && DealerTurn==false)
            {
                Dice1Temp = rnd.Next(IntTimes6);
                Dice2Temp = rnd.Next(IntTimes6);


                Dice1.Source = new BitmapImage(new Uri(@diceUri[Dice1Temp % 6], UriKind.RelativeOrAbsolute));
                Dice2.Source = new BitmapImage(new Uri(@diceUri[Dice2Temp % 6], UriKind.RelativeOrAbsolute));
                FirstRoll = false;
                PlayerSum = diceRoll[(Dice1Temp % 6)] + diceRoll[(Dice2Temp % 6)];
                CheckButton.Visibility = Visibility.Visible;
            }

            if (FirstRoll == false && DealerTurn == true)
            {
                Dice2.Visibility = Visibility.Collapsed;
                Dice1Temp = rnd.Next(IntTimes6);
                Dice1.Source = new BitmapImage(new Uri(@diceUri[Dice1Temp % 6], UriKind.RelativeOrAbsolute));
                DealerSum += diceRoll[(Dice1Temp % 6)];
            }


            if (FirstRoll == true && DealerTurn == true)
            {
                Dice1Temp = rnd.Next(IntTimes6);
                Dice2Temp = rnd.Next(IntTimes6);


                Dice1.Source = new BitmapImage(new Uri(@diceUri[Dice1Temp % 6], UriKind.RelativeOrAbsolute));
                Dice2.Source = new BitmapImage(new Uri(@diceUri[Dice2Temp % 6], UriKind.RelativeOrAbsolute));
                FirstRoll = false;
                DealerSum = diceRoll[(Dice1Temp % 6)] + diceRoll[(Dice2Temp % 6)];
                CheckButton.Visibility = Visibility.Visible;
            }


            CheckIfWin();
            

        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            if (DealerTurn == true)
            {
                EndTestPossible = true;
                RollButton.Visibility = Visibility.Collapsed;
                CheckIfWin();
            }
            if (DealerTurn == false)
            {
                CheckButton.Content = "End Dealer turn";
                FirstRoll = true;
                DealerTurn = true;
            }
            
            
        }

       

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }
    }
}