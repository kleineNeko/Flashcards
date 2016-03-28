using Flashcards.DataTyps;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Flashcards.Steuerelemente
{
    /// <summary>
    /// Interaktionslogik für FlashcardControl.xaml
    /// </summary>
    public partial class FlashcardControl : UserControl
    {
        string CardText;
        string ImageSourceUrl;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card">Karte die dargestellt werden soll</param>
        /// <param name="side">Seite der Karte die dargestellt werden soll</param>
        public FlashcardControl(Card card, EnumCardSide side)
        {
            InitializeComponent();

            SelectNeededInformations(card, side);
            BuildVisualObjects();
            
        }

        /// <summary>
        /// Holt die Informationen von der Ausgewählten Cartenseite
        /// </summary>
        /// <param name="card">Karte</param>
        /// <param name="side">Seite von der Informationen benötigt werden</param>
        private void SelectNeededInformations(Card card, EnumCardSide side)
        {
            if(side == EnumCardSide.Front)
            {
                CardText = card.Question;
                ImageSourceUrl = card.ImageForFront;
            }
            else
            {
                CardText = card.Answer;
                ImageSourceUrl = card.ImageForBack;
            }
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
            int cardCase;

            cardCase = (String.IsNullOrWhiteSpace(CardText) && !String.IsNullOrWhiteSpace(ImageSourceUrl)) ? 1 : 0; 
            cardCase = (!String.IsNullOrWhiteSpace(CardText) && String.IsNullOrWhiteSpace(ImageSourceUrl) && cardCase == 0) ? 2 : cardCase; 
            cardCase = (!String.IsNullOrWhiteSpace(CardText) && !String.IsNullOrWhiteSpace(ImageSourceUrl) && cardCase == 0) ? 3 : cardCase; 

            return cardCase;
        }

        /// <summary>
        /// Ruft je nach grafischen Fall die entsprechende Methode zur Erstellung der grafischen Objekte auf
        /// </summary>
        private void BuildVisualObjects()
        {
            var cardCase = GetVisualCase();
            switch (cardCase)
            {
                case 1:
                    BuildImageOnlyCard();
                    break;
                case 2:
                    BuildTextOnlyCard();
                    break;
                case 3:
                    BuildImageTextCard();
                    break;
            }
        }

        /// <summary>
        /// Aufbau der Karte wenn nur ein Text vorliegt
        /// </summary>
        private void BuildTextOnlyCard()
        {
            TextBlock text = new TextBlock();
            text.TextWrapping = TextWrapping.Wrap;
            text.TextTrimming = TextTrimming.CharacterEllipsis;
            text.Text = CardText;
            text.Height = 150;
            text.Width = 300;
            text.Margin = new Thickness(50, 20, 50, 30);

            dp_Content.Children.Add(text);
        }

        /// <summary>
        /// Aufbau der Karte wenn nur ein Image vorliegt
        /// </summary>
        private void BuildImageOnlyCard()
        {
            try
            {
                Image img = new Image();
                img.Source = ImageHandling.ImageFromPath(ImageSourceUrl);
                img.Height = 150;
                img.Width = 150;
                img.Margin = new Thickness(120, 10, 120, 0);

                DockPanel.SetDock(img, Dock.Left);
                dp_Content.Children.Add(img);
            }
            catch
            {
                ShowErrorLable();
            }
        }

        /// <summary>
        /// Aufbau der Karte wenn sowohl Text als auch Image vorliegt
        /// </summary>
        private void BuildImageTextCard()
        {
            try
            {
                Image img = new Image();
                img.Source = ImageHandling.ImageFromPath(ImageSourceUrl);
                img.Height = 150;
                img.Width = 150;
                img.Margin = new Thickness(10, 10, 0, 0);

                StackPanel panel = new StackPanel();
                panel.Height = 150;
                panel.Width = 200;
                panel.Margin = new Thickness(30, 10, 10, 30);

                TextBlock text = new TextBlock();
                text.TextWrapping = TextWrapping.Wrap;
                text.TextTrimming = TextTrimming.CharacterEllipsis;
                text.Text = CardText;
                
                panel.Children.Add(text);

                DockPanel.SetDock(img, Dock.Left);
                DockPanel.SetDock(panel, Dock.Right);
                dp_Content.Children.Add(img);
                dp_Content.Children.Add(panel);
            }
            catch
            {
                ShowErrorLable();
            }
        }

        /// <summary>
        /// Fehleranzeige im Falle dass eine Bilddatei nicht geladen werden kann
        /// </summary>
        private void ShowErrorLable()
        {
            Label lb = new Label();
            lb.Content = "Die Karte konnte nicht geladen werden./n Fehlende Bilddatei.";
            lb.Margin = new Thickness(20, 20, 0, 0);

            dp_Content.Children.Add(lb);
        }
    }
}
