using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Flashcards.DataTyps;
using Flashcards.Steuerelemente;
using System.Windows.Media;

namespace Flashcards
{
    /// <summary>
    /// Interaktionslogik für P_CardStack.xaml
    /// </summary>
    public partial class P_CardStack : System.Windows.Controls.Page
    {
        private string CategoryId;     

        public P_CardStack(string categoryId)
        {
            InitializeComponent();
            CategoryId = categoryId;
            var cards = Card.GetCards(categoryId);
            if (cards.Count > 0)
                FillGrid(cards);
        }

        private void newCard_Click(object sender, RoutedEventArgs e)
        {
            Page page = new P_NewCard(CategoryId);
            MainWindow.Frame.Content = page;
        }

        private void FillGrid(List<Card> cards)
        {
            int amount = cards.Count;
            int positionInRow = 0;

            for(int i = 0; i< amount; i++)
            {
                switch (positionInRow)
                {
                    case 0:
                        var item0 = BuildCardItem(cards[i]);
                        sp_0.Children.Add(item0);
                        positionInRow++;
                        break;
                    case 1:
                        var item1 = BuildCardItem(cards[i]);
                        sp_1.Children.Add(item1);
                        positionInRow++;
                        break;
                    case 2:
                        var item2 = BuildCardItem(cards[i]);
                        sp_2.Children.Add(item2);
                        positionInRow = 0;
                        break;
                }
            }
        }

        private Border BuildCardItem(Card card)
        {
            Border border = new Border();
            border.Height = 180;
            border.Width = 200;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1);
            border.Child = new CardControl(CategoryId, card);
            border.Margin = new Thickness(10);

            return border;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Page page = new P_CardSelection();
            MainWindow.Frame.Content = page;
        }
    }
}
