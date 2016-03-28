using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Flashcards.DataTyps;

namespace Flashcards
{
    /// <summary>
    /// Interaktionslogik für P_NewCard.xaml
    /// </summary>
    public partial class P_NewCard : Page
    {
        private string CategoryId;
        private Card NewCard;        

        public P_NewCard(string categoryId)
        {
            InitializeComponent();
            NewCard = new Card();
            CategoryId = categoryId;
        }

        //for updating
        public P_NewCard(string categoryId, Card existingCard)
        {
            InitializeComponent();
            CategoryId = categoryId;
            NewCard = existingCard;

            //Fill page
            textF.Text = existingCard.Question;
            textB.Text = existingCard.Answer;
            ShowImage(existingCard.ImageForFront, true);
            ShowImage(existingCard.ImageForBack, false);
        }

        /// <summary>
        /// Holt sich alle Artikel die in dieser kategorie angelegt wurden und schreibt deren Bezeichner in die ComboBox
        /// </summary>
        private void FillComboBox(int pageId)
        {
            //DatabaseInteraction di = new DatabaseInteraction();
            //Pages = di.GetPagesBySubject(CategoryId);
            //if (Pages.Count() > 0)
            //{
            //    cbx_Pages.Items.Add("Keine Page zuweisen");
            //    foreach (DatabaseTyps.Page item in Pages)
            //    {
            //        cbx_Pages.Items.Add(item.Headline);
            //        if (item.PageId == pageId)
            //        {
            //            cbx_Pages.SelectedItem = item.Headline;
            //        }
            //    }
            //    if (pageId == 0)
            //    {
            //        cbx_Pages.SelectedIndex = 0; // festgelegt
            //    }
            //}
            //else
            //{
            //    cbx_Pages.Items.Add("Keine Page zuweisen");
            //    cbx_Pages.SelectedIndex = 0;
            //}
        }   

        private void addImgF_Click(object sender, RoutedEventArgs e)
        {
            NewCard.ImageForFront= OpenExplorer();
            imgF.Source = null;
            ShowImage(NewCard.ImageForFront, true);
        }


        private string OpenExplorer()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF";
            dialog.ShowDialog();
            string file = dialog.FileName;

            if (!String.IsNullOrWhiteSpace(file))
            {
                if (file.ToUpper().EndsWith(".JPG") || file.ToUpper().EndsWith(".GIF") || file.ToUpper().EndsWith(".PNG") || file.ToUpper().EndsWith(".TIFF") || file.ToUpper().EndsWith(".BMP"))
                {
                    return file;
                }
            }
            return "";
        }


        private void ShowImage(string path, bool front)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(path))
                {
                    Uri src = new Uri(path, UriKind.Absolute);

                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.UriSource = src;
                    img.EndInit();                    

                    if (front)
                    {
                        imgF.Source = img;
                        NewCard.ImageForFront = path;
                    }
                    else
                    {
                        imgB.Source = img;
                        NewCard.ImageForBack = path;
                    }
                }                    
            }
            catch { }
        }

        private void addImgB_Click(object sender, RoutedEventArgs e)
        {
            var frontImage = OpenExplorer();
            imgB.Source = null;
            ShowImage(frontImage, false);
        }

        private void resetImgB_Click(object sender, RoutedEventArgs e)
        {
            NewCard.ImageForBack = null;
            imgB.Source = null;
        }

        private void resetImgF_Click(object sender, RoutedEventArgs e)
        {
            NewCard.ImageForFront = null;
            imgF.Source = null;
        }

        /// <summary>
        /// Gibt der Id der Page zurück, die über die ComboBox ausgewählt wurde
        /// </summary>
        /// <returns></returns>
        private void SelectedPage()
        {
            //int pageId = 0;
            //string headline = "";
            //try
            //{
            //    headline = cbx_Pages.SelectedItem.ToString();
            //    if (!headline.Equals("Keine Page zuweisen"))
            //    {
            //        foreach (DatabaseTyps.Page item in Pages)
            //        {
            //            if (item.Headline.Equals(headline))
            //            {
            //                pageId = item.PageId;
            //            }
            //        }
            //    }                
            //}
            //catch { }      
            
            //return pageId;
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (ReadyToSave())
            {
                if (String.IsNullOrWhiteSpace(NewCard.Id))
                {
                    bool saved = NewCard.CreateCard(CategoryId);
                    if (saved)
                    {
                        MessageBox.Show("Die Karte wurde angelegt");
                    }
                }
                else
                {
                    bool saved = NewCard.UpdateCard(CategoryId);
                    if (saved)
                    {
                        MessageBox.Show("Die Karte wurde geupdated");
                        P_CardStack page = new P_CardStack(CategoryId);
                        MainWindow.Frame.Content = page;
                    }
                }
                ResetPage();
            }
        }

        private bool ReadyToSave()
        {
            if(String.IsNullOrWhiteSpace(NewCard.ImageForFront) && String.IsNullOrWhiteSpace(textF.Text))
            {
                MessageBox.Show("Sie müssen entweder ein Bild oder einen Text für die forderseite der Karte definieren!");
                return false;
            }
            if(String.IsNullOrWhiteSpace(NewCard.ImageForBack) && String.IsNullOrWhiteSpace(textB.Text))
            {
                MessageBox.Show("Sie müssen entweder ein Bild oder einen Text für die rückseite der Karte definieren!");
                return false;
            }

            if (!String.IsNullOrWhiteSpace(textF.Text))
                NewCard.Question = textF.Text;
            if (!String.IsNullOrWhiteSpace(textB.Text))
                NewCard.Answer = textB.Text;

            //selected Index mit beachten
            return true;
        }

        private void ResetPage()
        {
            NewCard = new Card();
            textF.Text = " ";
            textB.Text = " ";
            imgF.Source = null;
            imgB.Source = null;
            cbx_Pages.SelectedIndex = 0;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Page page = new P_CardStack(CategoryId);
            MainWindow.Frame.Content = page;
        }
    }
}
