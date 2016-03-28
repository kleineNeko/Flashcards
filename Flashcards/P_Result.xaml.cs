using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Flashcards.DataTyps;

namespace Flashcards
{
    /// <summary>
    /// Interaktionslogik für P_Result.xaml
    /// </summary>
    public partial class P_Result : Page
    {
        CardRun Run;

        public P_Result(CardRun run)
        {
            InitializeComponent();
            Run = run;

            lbl_ManagedCards.Content = run.CardsManaged.ToString();
            lbl_easyCards.Content = run.EasyGraded.ToString();
            lbl_normalCards.Content = run.NormalGraded.ToString();
            lbl_hardCards.Content = run.HardGraded.ToString();
            lbl_unsolved.Content = run.Unsolved.ToString();
        }

        private void btn_end_Click(object sender, RoutedEventArgs e)
        {
            Run = null;
            MainWindow.Frame.Content = new P_CardSelection();
        }

        private void btn_repeat_Click(object sender, RoutedEventArgs e)
        {
            Run.Counter = 0;
            Run.CardsManaged = 0;
            Run.EasyGraded = 0;
            Run.NormalGraded = 0;
            Run.HardGraded = 0;
            Run.Unsolved = 0;
            Run.Cards = Run.UsedCards;
            Run.UsedCards = new List<Card>();
            P_CardFront page = new P_CardFront(Run);
            MainWindow.Frame.Content = page;            
        }
    }
}
