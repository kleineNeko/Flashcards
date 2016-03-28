using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Flashcards.DataTyps;
using System.Windows.Forms;

namespace Flashcards.Steuerelemente
{
    /// <summary>
    /// Interaktionslogik für CategoryBanner.xaml
    /// </summary>
    public partial class CategoryBanner : System.Windows.Controls.UserControl
    {
        private Category Item;
        private P_CardSelection SourcePage;

        public CategoryBanner(Category category, P_CardSelection page)
        {
            InitializeComponent();

            name.Content = category.Name;
            Item = category;
            SourcePage = page;

            try
            {
                Uri src = new Uri(Item.ImagePath, UriKind.Absolute);
                BitmapImage image = new BitmapImage();

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = src;
                image.EndInit();
                img.Source = image;
            }
            catch { }

            //img_article.Source = this.FindResource("defaultArticleIcon") as BitmapImage;
        }      


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var myResult = System.Windows.Forms.MessageBox.Show("Wollen Sie diese Kategorie und alle darin enthaltenen Karten und Artikel wirklich löschen?", "Kategorie löschen", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(myResult == DialogResult.OK)
            {
                Category.DeleteCategory(Item.Id);
                P_CardSelection page = new P_CardSelection();
                MainWindow.Frame.Content = page;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Window window = new NewSubject(SourcePage, Item);
            window.Show();
        }

        private void btnCards_Click(object sender, RoutedEventArgs e)
        {
            Page page = new P_CardStack(Item.Id);
            MainWindow.Frame.Content = page;            
        }

        private void btnArticles_Click(object sender, RoutedEventArgs e)
        {
            Page page = new P_Articles(Item.Id);
            MainWindow.Frame.Content = page;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            var cards = Card.GetCards(Item.Id);
            int amount = Item.CardAmountPerSession;
            if (cards.Count > 0)
            {
                CardRun run = new CardRun();
                CardHandling cardhandler = new CardHandling(cards, Item);
                List<Card> Cards= cardhandler.GetCards();
                run.UsedCards = new List<Card>();
                run.CategoryId = Item.Id;
                run.Cards = Cards;
                run.CardFlippingOn = Item.RoleBothCardSides;
                run.PagesOn = Item.ShowPage;

                P_CardFront page = new P_CardFront(run);
                MainWindow.Frame.Content = page;
            }
            else
            {
                System.Windows.MessageBox.Show("Sie müssen mindestens eine Karte angelegt haben, bevor Sie einen Durchgang starten können!");
            }
        }

        private void btnArticle_MouseOver(object sender, DependencyPropertyChangedEventArgs e)
        {
            //img_article.Source = this.FindResource("focusedArticleIcon") as BitmapImage;
        }
    }
}
