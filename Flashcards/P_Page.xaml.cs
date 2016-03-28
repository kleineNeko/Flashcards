using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flashcards
{
    /// <summary>
    /// Interaktionslogik für P_Page.xaml
    /// </summary>
    public partial class P_Page : System.Windows.Controls.Page
    {
        int SubjectId;
        int PageId;
        //List<Article> Articles;
        string PageName;

        public P_Page(int pageId)
        {
            InitializeComponent();
            PageId = pageId;
            GetValues(pageId);
            
        }

        private void GetValues(int pageId)
        {
            //DatabaseInteraction di = new DatabaseInteraction();
            //DatabaseTyps.Page page = di.GetPage(pageId);
            //SubjectId = page._SubjectId;
            //PageName = page.Headline;
            //lbl_headline.Content = page.Headline;
            //Articles = new List<Article>();
            //Articles = di.GetArticles(pageId);
            //FillPage();            
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            //P_Articles page = new P_Articles(SubjectId);
            ////MainWindow.f_tab2.Content = page;
        }

        private void FillPage()
        {
            //int index = Articles.Count();
            //for (int i = 0; i < index; i++)
            //{
            //    FillArticle(Articles[i], i);
            //}
        }

        /// <summary>
        /// Setztet die einzelnen Absätze zusammen
        /// </summary>
        /// <param name="article"></param>
        /// <param name="index"></param>
        //private void FillArticle(Article article, int index)
        //{
        //    //if (!String.IsNullOrEmpty(article.Headline))
        //    //{
        //    //    Paragraph headline = new Paragraph(new Run(article.Headline)); headline.FontSize = 20; headline.TextAlignment = TextAlignment.Center;
        //    //    a0.Blocks.Add(headline);                
        //    //}

        //    //if (article.Text != null)
        //    //{
        //    //    Paragraph p = new Paragraph();
        //    //    p = BuildTextBlock(article.Text, article.Picture, article.PictureToLeftSide);
        //    //    a0.Blocks.Add(p);
        //    //}

        //    //if (!String.IsNullOrEmpty(article.ListHeadline))
        //    //{
        //    //    Paragraph p = new Paragraph(new Run(article.ListHeadline)); p.TextAlignment = TextAlignment.Center; p.FontWeight = FontWeights.Bold;
        //    //    a0.Blocks.Add(p);
        //    //}

        //    //if (!String.IsNullOrEmpty(article.ListItems))
        //    //{
        //    //    List list = new List(); list.TextAlignment = TextAlignment.Center;
        //    //    string[] items = article.ListItems.Split(';');
        //    //    foreach (string item in items)
        //    //    {
        //    //        ListItem container = new ListItem();
        //    //        Paragraph p = new Paragraph(new Run(item));
        //    //        container.Blocks.Add(p);
        //    //        list.ListItems.Add(container);
        //    //    }
        //    //    a0.Blocks.Add(list);
        //    //}

        //    //if (!String.IsNullOrEmpty(article.ReferenceLinks) || article.ReferenceLinks.Equals(";;"))
        //    //{
        //    //    string[] links = article.ReferenceLinks.Split(';');
        //    //    foreach (var item in links)
        //    //    {
        //    //        Hyperlink link = new Hyperlink(new Run(item));
        //    //    }
                
        //    //}
                     
        //}

        /// <summary>
        /// Stellt den TextBlock zusammen, und bindet dass Bild mit ein
        /// </summary>
        /// <param name="text"></param>
        /// <param name="picture"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        //private Paragraph BuildTextBlock(byte[] text, byte[] picture, bool left)
        //{
        //    //Paragraph p = new Paragraph();
        //    //Run r = new Run(Encoding.UTF8.GetString(text));
        //    //p.Inlines.Add(r);
        //    //Figure f = new Figure(); f.VerticalAnchor = FigureVerticalAnchor.PageTop;
        //    //if (picture != null)
        //    //{
        //    //    BlockUIContainer container = new BlockUIContainer();
        //    //    Image img = new Image(); img.Source = ImageHandling.ByteArrayToBitmapImage(picture); img.Height = 200; img.Width = 200; img.Margin = new Thickness(10);  
        //    //    if (left == false)
        //    //    {
        //    //        f.HorizontalAnchor = FigureHorizontalAnchor.PageRight;
        //    //    }
        //    //    else
        //    //    {
        //    //        f.HorizontalAnchor = FigureHorizontalAnchor.PageLeft;
        //    //    }
        //    //    container.Child = img;
        //    //    f.Blocks.Add(container);
        //    //    p.Inlines.Add(f);
        //    //}
        //    //return p;
        //}

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            //DatabaseInteraction di = new DatabaseInteraction();
            //di.DeletePage(PageId);
            //P_Articles page = new P_Articles(SubjectId);
            ////MainWindow.f_tab2.Content = page;
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.EditNewPage(SubjectId, Articles, PageId, PageName);
            //MainWindow.f_tab2.Content = MainWindow.NewPage;
        }
    }
}
