using System.Windows;
using Flashcards.DataTyps;
using System.Windows.Controls;
using System.Windows.Media;
using Flashcards.Steuerelemente;

namespace Flashcards
{
    /// <summary>
    /// Interaktionslogik für P_CardBack.xaml
    /// </summary>
    public partial class P_CardBack : System.Windows.Controls.Page
    {
        private CardRun Run;
        EnumCardSide Side;
        private Card CurrentCard;

        private int points = 1;
        //private int LinkedPageId = 0;        

        public P_CardBack(CardRun run, EnumCardSide side, Card currentCard)
        {
            InitializeComponent();
            Run = run;

            switch (side)
            {
                case EnumCardSide.Front:
                    Side = EnumCardSide.Back;
                    break;
                case EnumCardSide.Back:
                    Side = EnumCardSide.Front;
                    break;
            }

            CurrentCard = currentCard;
            SetValues();
        }

        private void SetValues()
        {
            Border border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1);
            border.Width = 400;
            border.Height = 250;

            FlashcardControl card = new FlashcardControl(CurrentCard, Side);
            border.SetValue(Grid.RowProperty, 1);

            border.Child = card;
            sp_cards.Children.Add(border);
        }        

        /// <summary>
        /// Button der den Aufruf der nächsten Karte einleitet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (points == 1)
            {
                MessageBox.Show("Bitte bewerten Sie den Schwierigkeitsgrad der Karte.");
            }
            else
            {
                try
                {                    
                    Run.Counter++;
                                       
                    CurrentCard.CardValue += points;
                    CurrentCard.Counter += 1;
                    switch (points)
                    {
                        case 0:
                            CurrentCard.Grading = (byte)EnumCardGrading.Normal;
                            Run.CardsManaged += 1;
                            Run.NormalGraded += 1;
                            break;
                        case 4:
                            CurrentCard.Grading =(byte)EnumCardGrading.Hard;
                            Run.Unsolved += 1;
                            break;
                        case -2:
                            CurrentCard.Grading = (byte)EnumCardGrading.Easy;
                            Run.CardsManaged += 1;
                            Run.EasyGraded += 1;
                            break;
                        case 2:
                            CurrentCard.Grading = (byte)EnumCardGrading.Hard;
                            Run.CardsManaged += 1;
                            Run.HardGraded += 1;
                            break;
                        default:
                            CurrentCard.Grading = 0;
                            break;
                    }

                    bool success = CurrentCard.UpdateCard(Run.CategoryId);
                    if (!success)
                    {
                        //fehler beim speichern der Karte
                    }
                    Run.UsedCards.Add(CurrentCard);

                    Run.Cards.Remove(CurrentCard);
                    P_CardFront front = new P_CardFront(Run);
                    MainWindow.Frame.Content = front;
                }
                catch
                {
                    P_Result page = new P_Result(Run);
                    MainWindow.Frame.Content = page;
                }
            }            
        }

        private void chxFailed_Clicked(object sender, RoutedEventArgs e)
        {
            chb_success.IsChecked = false;
            rb_easy.IsChecked = false;
            rb_hard.IsChecked = false;
            rb_normal.IsChecked = false;
            points = 4;
        }

        private void chbSuccess_Cklicked(object sender, RoutedEventArgs e)
        {
            failed.IsChecked = false;
            rb_normal.IsChecked = true;
            rb_easy.IsChecked = false;
            rb_hard.IsChecked = false;
            points = 0;
        }

        private void rbEasy_Click(object sender, RoutedEventArgs e)
        {
            failed.IsChecked = false;
            chb_success.IsChecked = true;
            points = -2;
        }

        private void rbNormal_Click(object sender, RoutedEventArgs e)
        {
            failed.IsChecked = false;
            chb_success.IsChecked = true;
            points = 0;
        }

        private void rbHard_Click(object sender, RoutedEventArgs e)
        {
            failed.IsChecked = false;
            chb_success.IsChecked = true;
            points = 2;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            P_CardSelection page = new P_CardSelection();
            MainWindow.Frame.Content = page;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Der Durchgang wird abgebrochen damit die aktuelle Karte bearbeitet werden kann.");

            P_NewCard page = new P_NewCard(Run.CategoryId, CurrentCard);
            MainWindow.Frame.Content = page;
        }
    }
}
