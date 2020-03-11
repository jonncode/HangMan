﻿using System;
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

namespace HangMan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Player playerHost = new Player();
        GuestPlayer playerGuest = new GuestPlayer();
        List<String> hiddenWord = new List<string>();
        List<String> guessedWord = new List<string>();
        List<String> incorrectChars = new List<string>();
        string guessedChar;
        public MainWindow()
        {
            InitializeComponent();
            playerGuest.SetTries(8);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (playerGuest.GetTries() > 0)
                {
                    guessedChar = GuessWordXAML.Text.ToUpper();
                    if (guessedChar.All(char.IsDigit))
                    {
                        return;
                    }
                    else
                    {
                        bool correctWord = false;
                        int i = 0;
                        foreach (string ch in hiddenWord)
                        {
                            if (ch == guessedChar)
                            {
                                guessedWord[i] = guessedChar.ToUpper();
                                correctWord = true;
                            }
                            i++;
                        }
                        if (correctWord == false)
                        {
                            playerGuest.SetTries(playerGuest.GetTries() - 1);
                            TriesLeft.Text = "Tries Left: " + playerGuest.GetTries().ToString();
                            incorrectChars.Add(guessedChar);
                            tBlIncorrectChars.Text = string.Join("", incorrectChars);

                        }
                        HiddenWordXAML.Text = HiddenWordXAML.Text = string.Join("", guessedWord);
                    }
                }

            }
        }

        private void GuessWordXAML_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                hiddenWord.Clear();

                foreach (char ch in EnterWordXAML.Text)
                {
                    hiddenWord.Add(ch.ToString().ToUpper());
                    guessedWord.Add("_");
                }
                HiddenWordXAML.Text = string.Join("", guessedWord);
                EnterWordXAML.IsEnabled = false;
            }
        }
        private void RestartGame(object sender, RoutedEventArgs e)
        {
            hiddenWord.Clear();
            guessedWord.Clear();
            incorrectChars.Clear();
            playerGuest.SetTries(8);
            GuessWordXAML.Text = "";
            HiddenWordXAML.Text = "";
            EnterWordXAML.Text = "";
            TriesLeft.Text = "Tries Left: 8";
            tBlIncorrectChars.Text = "";
            tblGameStatus.Text = "";
            EnterWordXAML.IsEnabled = true;
        }
    }
}
