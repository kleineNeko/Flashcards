using System;
using System.Linq;
using System.Windows;
using Flashcards.DataTyps;
using Flashcards.Steuerelemente;
using System.Windows.Controls;
using System.Windows.Media;

namespace Flashcards
{
    /// <summary>
    /// Interaktionslogik für P_CardFront.xaml
    /// </summary>
    public partial class P_CardFront : System.Windows.Controls.Page
    {
        private CardRun Run;
        private EnumCardSide Side;
        private Card CurrentCard;
        
        public P_CardFront(CardRun run)
        {
            InitializeComponent();
            Run = run;
            SelectRandomCard();
        }


        private void SelectRandomCard()
        {
            Random randum = new Random();
            int index = randum.Next(0, (Run.Cards.Count() - 1));
            CurrentCard = Run.Cards[index];

            if(Run.CardFlippingOn == true)
            {
                Random randomSide = new Random();
                int side = randomSide.Next(0, 1);
                SetValues(side);
            }
            SetValues(0);
        }

        private void SetValues(int side)
        {
            switch (side)
            {
                case 0:
                    Side = EnumCardSide.Front;
                    break;
                case 1:
                    Side = EnumCardSide.Back;
                    break;
            }

            Border border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1);
            border.Width = 400;
            border.Height = 250;

            FlashcardControl card = new FlashcardControl(CurrentCard, Side);
            border.SetValue(Grid.RowProperty, 1);

            border.Child = card;
            g_PageContent.Children.Add(border);
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            P_CardBack back = new P_CardBack(Run, Side, CurrentCard);
            MainWindow.Frame.Content = back;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Run = null;
            P_CardSelection page = new P_CardSelection();
            MainWindow.Frame.Content = page;
        }

        private void btn_EditCard_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Der Durchgang wird abgebrochen um die Karte ändern zu können.");   //ersetzten durch ja/nein MessageBox
            
            P_NewCard page = new P_NewCard(Run.CategoryId, CurrentCard);
            MainWindow.Frame.Content = page;
        }
    }
}
