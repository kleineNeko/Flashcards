using System.Windows;
using System.Windows.Controls;
using Flashcards.Classes;

namespace Flashcards
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame Frame;

        public MainWindow()
        {
            InitializeComponent();

            bool allFine = DataManagement.MainFolder();
            if (!allFine)
            {
                MessageBox.Show("Schwerwiegender Fehler, es können weder Daten angelegt noch gelesen werden.");
                this.Close();
            }
            else
            {
                //Anwendung normal starten
                Frame = f_content;
                f_content.Content = new P_CardSelection();
            }
        }
    }
}
