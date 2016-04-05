using System;
using System.Windows;
using System.Windows.Controls;
using Flashcards.DataTyps;

namespace Flashcards.Steuerelemente
{
    /// <summary>
    /// Interaktionslogik für CardControl.xaml
    /// </summary>
    public partial class CardControl : UserControl
    {
        private string CategoryId;
        private Card CurrentCard;
        private EnumCardSide Side;
        private Image IMG;
        private TextBlock TB;

        public CardControl(string categoryId, Card card)
        {
            InitializeComponent();

            CategoryId = categoryId;
            CurrentCard = card;
            Side = EnumCardSide.Front;

            BuildLayout();
        }

        /// <summary>
        /// Überprüft welche Komponenten für die grafische Darstellung zur Verfügung stehen und weist ihnen einen Fall zu
        /// 1 = Image ohne Text
        /// 2 = Text ohne Image
        /// 3 = Text und Image
        /// </summary>
        /// <returns></returns>
        private int GetVisualCase()
        {
            int cardCase = 0;
            if (Side == EnumCardSide.Front)
            {
                cardCase = (String.IsNullOrWhiteSpace(CurrentCard.Question) && !String.IsNullOrWhiteSpace(CurrentCard.ImageForFront)) ? 1 : 0;
                cardCase = (!String.IsNullOrWhiteSpace(CurrentCard.Question) && String.IsNullOrWhiteSpace(CurrentCard.ImageForFront) && cardCase == 0) ? 2 : cardCase;
                cardCase = (!String.IsNullOrWhiteSpace(CurrentCard.Question) && !String.IsNullOrWhiteSpace(CurrentCard.ImageForFront) && cardCase == 0) ? 3 : cardCase;
            }
            else
            {
                cardCase = (String.IsNullOrWhiteSpace(CurrentCard.Answer) && !String.IsNullOrWhiteSpace(CurrentCard.ImageForBack)) ? 1 : 0;
                cardCase = (!String.IsNullOrWhiteSpace(CurrentCard.Answer) && String.IsNullOrWhiteSpace(CurrentCard.ImageForBack) && cardCase == 0) ? 2 : cardCase;
                cardCase = (!String.IsNullOrWhiteSpace(CurrentCard.Answer) && !String.IsNullOrWhiteSpace(CurrentCard.ImageForBack) && cardCase == 0) ? 3 : cardCase;
            }

            return cardCase;
        }

        /// <summary>
        /// Ruft abhängig vom Darstellungsfall die entsprechende Methode auf
        /// </summary>
        private void BuildLayout()
        {
            int cardCase = GetVisualCase();

            switch (cardCase)
            {
                case 1:
                    BuildImageOnlyLayout();
                    break;
                case 2:
                    BuildTextOnlyLeayout();
                    break;
                case 3:
                    BuildTextImageLayout();
                    break;
            }
        }


        private void BuildImageOnlyLayout()
        {
            IMG = new Image();
            IMG.Height = 100;
            IMG.Width = 100;
            IMG.Margin = new Thickness(20);
            IMG.Source = (Side == EnumCardSide.Front) ? ImageHandling.ImageFromPath(CurrentCard.ImageForFront) : ImageHandling.ImageFromPath(CurrentCard.ImageForBack);

            sp_card.Children.Add(IMG);
        }

        private void BuildTextOnlyLeayout()
        {
            TB = new TextBlock();
            TB.Height = 150;
            TB.Width = 120;
            TB.Margin = new Thickness(20);
            TB.TextWrapping = TextWrapping.Wrap;
            TB.TextTrimming = TextTrimming.CharacterEllipsis;
            TB.FontSize = 10;
            TB.Text = (Side == EnumCardSide.Front) ? CurrentCard.Question : CurrentCard.Answer;

            sp_card.Children.Add(TB);
        }

        private void BuildTextImageLayout()
        {
            IMG = new Image();
            IMG.Height = 50;
            IMG.Width = 50;
            IMG.Margin = new Thickness(10);
            IMG.Source = (Side == EnumCardSide.Front) ? ImageHandling.ImageFromPath(CurrentCard.ImageForFront) : ImageHandling.ImageFromPath(CurrentCard.ImageForBack);

            TB = new TextBlock();
            TB.Height = 90;
            TB.Width = 120;
            TB.TextWrapping = TextWrapping.Wrap;
            TB.TextTrimming = TextTrimming.CharacterEllipsis;
            TB.FontSize = 10;
            TB.Text = (Side == EnumCardSide.Front) ? CurrentCard.Question : CurrentCard.Answer;

            sp_card.Children.Add(IMG);
            sp_card.Children.Add(TB);            
        }
        //private void ShowImage(string path)
        //{
        //    try {
        //        Uri src = new Uri(path, UriKind.Absolute);
        //        BitmapImage img = new BitmapImage();

        //        img.BeginInit();
        //        img.CacheOption = BitmapCacheOption.OnLoad;
        //        img.UriSource = src;
        //        img.EndInit();
        //        img_Image.Source = img;
        //    }
        //    catch { }
        //}

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            P_NewCard page = new P_NewCard(CategoryId, CurrentCard);
            MainWindow.Frame.Content = page;
        }

        /// <summary>
        /// Wechselt die Aktive Kartenseite und definiert das Layout neu. Die Alten Elemente werden gelöscht.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TurnButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Side)
            {
                case (EnumCardSide.Back):
                    Side = EnumCardSide.Front;
                    sp_card.Children.Remove(IMG); //test ob geht auch wenn nicht vorhanden!
                    sp_card.Children.Remove(TB);
                    IMG = null;
                    TB = null;
                    BuildLayout();
                    break;
                case (EnumCardSide.Front):
                    Side = EnumCardSide.Back;
                    sp_card.Children.Remove(IMG); 
                    sp_card.Children.Remove(TB);
                    IMG = null;
                    TB = null;
                    BuildLayout();
                    break;
            }            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //event werfen
            //Sender == auslösende Karte im auffangenden Event Löschung vornehmen //Ressourcenfreigabe Problem
            //Card.DeleteCard(CurrentCard.Id, CategoryId);
        }
    }
}
