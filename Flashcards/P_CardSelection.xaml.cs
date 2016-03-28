using System.Windows;
using Flashcards.DataTyps;
using Flashcards.Steuerelemente;

namespace Flashcards
{
    /// <summary>
    /// Interaktionslogik für P_CardSelection.xaml
    /// </summary>
    public partial class P_CardSelection : System.Windows.Controls.Page
    {
        public P_CardSelection()
        {
            InitializeComponent();
            var categories = Category.GetCategories();
            foreach (var item in categories)
            {
                panel_categories.Children.Add(new CategoryBanner(item, this));
            }
        }

        /// <summary>
        /// Verknüpfung zum Anlegen einer neuen Kategorie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newSubject_Click(object sender, RoutedEventArgs e)
        {
            NewSubject window = new NewSubject(this);
            window.Show();
        }

        public void AddNewCategorie(Category category)
        {
            panel_categories.Children.Add(new CategoryBanner(category, this));
        }
    }
}
