using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Flashcards.DataTyps;
using Microsoft.Win32;

namespace Flashcards
{ 

    /// <summary>
    /// Interaktionslogik für NewSubject.xaml
    /// </summary>
    public partial class NewSubject : Window
    {
        Category Category = new Category();        
        Category Original;
        P_CardSelection MainPage;

        /// <summary>
        /// Konstruktor der mit der neu Erstellung einer Kategorie aufgerufen wird
        /// </summary>
        /// <param name="page"></param>
        public NewSubject(P_CardSelection page)
        {
            InitializeComponent();
            MainPage = page;
            tbx_amoutOfCards.Text = "30";
            Category.ImagePath = "";
        }

        /// <summary>
        /// Konstruktor der zum Bearbeiten einer Kategorie aufgerufen wird
        /// </summary>
        /// <param name="page">Referenz der Aufrufenden Page</param>
        /// <param name="category">Referenz bereits bestehender Kategorie</param>
        public NewSubject(P_CardSelection page, Category category)
        {
            InitializeComponent();
            MainPage = page;
            Original = category;
            Category.ImagePath = category.ImagePath;

            ShowImage(category.ImagePath);
            name.Text = category.Name;
            Category.Name = category.Name;

            cbx_showArticles.IsChecked = category.ShowPage;
            cbx_usebothSides.IsChecked = category.RoleBothCardSides;
            tbx_amoutOfCards.Text = category.CardAmountPerSession.ToString();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {  
            this.Close();
        }

        private void uploading_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF";
            dialog.ShowDialog();
            string file = dialog.FileName;

            if (!String.IsNullOrWhiteSpace(file))
            {
                if (file.ToUpper().EndsWith(".JPG") || file.ToUpper().EndsWith(".GIF") || file.ToUpper().EndsWith(".PNG") || file.ToUpper().EndsWith(".TIFF") || file.ToUpper().EndsWith(".BMP"))
                {
                    Category.ImagePath = file;
                    ShowImage(file);                    
                }
            }
        }

        /// <summary>
        /// Setzt das Bild das unter dem angegebenem Pfad liegt
        /// </summary>
        /// <param name="path"></param>
        private void ShowImage(string path)
        {
            try
            {
                Uri src = new Uri(path, UriKind.Absolute);

                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.UriSource = src;
                img.EndInit();
                picture.Source = img;        
            }
            catch { }
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            bool checkPositive = CheckSettings();
            if(checkPositive)
            {
                if (Original == null)
                {
                    Category.Name = name.Text;
                    Category.CreateCategory();
                    MainPage.AddNewCategorie(Category);
                    this.Close();
                }
                else if (Original != null && !String.IsNullOrWhiteSpace(Original.Id))
                {
                    Category.Name = name.Text;
                    Category.UpdateCategory(Original.Id);
                    MainWindow.Frame.Content = new P_CardSelection();
                    this.Close();
                }
            }           
        }
        
        private bool CheckSettings()
        {
            if(String.IsNullOrWhiteSpace(tbx_amoutOfCards.Text))
            {
                MessageBox.Show("Es muss angegeben werden wie viele Karteikarten in einem Durchlauf enthalten seinen sollen!");
                return false;
            }

            if (String.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show("Sie müssen zumindest einen Namen für die Kategorie festlegen!");
                return false;
            }

            int amount = 0;
            Int32.TryParse(tbx_amoutOfCards.Text, out amount);
            if(amount >= 5 && amount <= 500)
            {
                Category.CardAmountPerSession = amount;
            }
            else
            {
                MessageBox.Show("Die Anzahl Karten pro Durchlauf muss zwischen 5 und 500 liegen!");
                return false;
            }

            Category.RoleBothCardSides = (cbx_usebothSides.IsChecked == true) ? true : false;
            Category.ShowPage = (cbx_showArticles.IsChecked == true) ? true : false;

            return true;
        }

        private void deleteImage_Click(object sender, RoutedEventArgs e)
        {
            Category.ImagePath = "";
            picture.Source = null;
        }
    }
}
