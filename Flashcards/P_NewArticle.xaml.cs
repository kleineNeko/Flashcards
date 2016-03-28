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
    /// Interaktionslogik für P_NewArticle.xaml
    /// </summary>
    public partial class P_NewArticle : System.Windows.Controls.Page
    {
        string CategoryId;

        public P_NewArticle(string categoryId)
        {
            InitializeComponent();
            CategoryId = categoryId;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            P_Articles page = new P_Articles(CategoryId);
            MainWindow.Frame.Content = page;
        }


        //public int HasContent
        //{
        //    get { return hasContent; }
        //}

        //public Article Article
        //{
        //    get { return _Article; }
        //}

        //public P_NewArticle(ref Article article, string name)
        //{
        //    InitializeComponent();
        //    Name = name;
        //    _Article = new Article();
        //    _Article = article;
        //    if (article.PageId == 0)
        //    {
        //        _Article.ReferenceLinks = "";
        //    }
        //    if (article.PageId != 0)
        //    {
        //        FillPage();
        //    }
        //}

        ///// <summary>
        ///// Weist den Steuerelemente ihre Inhalte zu, wenn von außen ein gefülltest Artikel-Objekt kommt
        ///// </summary>
        //private void FillPage()
        //{
        //    if (!String.IsNullOrEmpty(_Article.Headline))
        //    {
        //        txb_articleName.Text = _Article.Headline;
        //    }
        //    if (_Article.Text != null)
        //    {
        //        tbx_articlecontent.Text = Encoding.UTF8.GetString(_Article.Text);
        //    }
        //    if (_Article.Picture != null)
        //    {
        //        ///*img_Display*/.Source = ImageHandling.ByteArrayToBitmapImage(_Article.Picture);
        //    }
        //    if (!String.IsNullOrEmpty(_Article.ListHeadline))
        //    {
        //        tbx_listEntry.Text = _Article.ListHeadline;
        //    }
        //    if (!String.IsNullOrEmpty(_Article.ListItems))
        //    {
        //        string[] items = _Article.ListItems.Split(';');
        //        foreach (string item in items)
        //        {
        //            //string entry = 
        //            cbx_listEntrys.Items.Add(item);
        //        }
        //    }
        //    if(!String.IsNullOrEmpty(_Article.ReferenceLinks) && !_Article.ReferenceLinks.Equals(";;")){
        //        string[] items = _Article.ReferenceLinks.Split(';');
        //        tbx_link1.Text = items[0];
        //        if (items.Count() > 1)
        //        {
        //            tbx_link2.Text = items[1];
        //        }
        //        if (items.Count() > 2)
        //        {
        //            tbx_link3.Text = items[2];
        //        }
        //    }

        //}




        ///// <summary>
        ///// Aktualisiert die Bildanzeige und legt das Bild im Artikel an
        ///// </summary>
        ///// <param name="picture"></param>
        //public void SetPicture(byte[] picture)
        //{
        //    //img_Display.Source = ImageHandling.ByteArrayToBitmapImage(picture);
        //    //_Article.Picture = picture;
        //    hasContent += 1;
        //}

        ///// <summary>
        ///// Aktualisiert das Artikel-Objekt sobald das Feld für das setzten der Überschrift für den Absatz verlassen wurde
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ArticleName_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    if (String.IsNullOrWhiteSpace(txb_articleName.Text))
        //    {
        //        if (!String.IsNullOrWhiteSpace(_Article.Headline))
        //        {
        //            _Article.Headline = "";
        //            hasContent -= 1;
        //        }                
        //    }
        //    else
        //    {
        //        _Article.Headline = txb_articleName.Text;
        //        hasContent += 1;
        //    }
        //}

        ///// <summary>
        ///// Aktualisiert das Artikel-Objekt sobald das Feld für das setzten des Textes für den Absatz verlassen wurde
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ArticleContent_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    if (!String.IsNullOrWhiteSpace(tbx_articlecontent.Text))
        //    {
        //        _Article.Text = Encoding.UTF8.GetBytes(tbx_articlecontent.Text);
        //        hasContent += 1;
        //    }
        //    else
        //    {
        //        if (_Article.Text != null)
        //        {
        //            _Article.Text = new byte[0];
        //            hasContent -= 1;
        //        }
        //    }
        //}

        //private void ListHeadline_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    if (String.IsNullOrWhiteSpace(tbx_listHeadline.Text))
        //    {
        //        if (!String.IsNullOrWhiteSpace(_Article.ListHeadline))
        //        {
        //            _Article.ListHeadline = "";
        //            hasContent -= 1;
        //        }
        //    }
        //    else
        //    {
        //        _Article.ListHeadline = tbx_listHeadline.Text;
        //        hasContent += 1;
        //    }
        //}

        //private void btn_addListEntry_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!String.IsNullOrWhiteSpace(tbx_listEntry.Text))
        //    {
        //        cbx_listEntrys.Items.Add(tbx_listEntry.Text);
        //        _Article.ListItems += tbx_listEntry.Text + ";";
        //        tbx_listEntry.Text = " ";
        //        hasContent += 1;
        //    }
        //}

        //private void ListEntrys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (cbx_listEntrys.SelectedIndex != -1)
        //    {
        //        string item = cbx_listEntrys.SelectedItem.ToString();
        //        tbx_listEntry.Text = item;
        //    }

        //}

        //private void btn_removeListEntry_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!String.IsNullOrWhiteSpace(tbx_listEntry.Text))
        //    {
        //        string unwantedEntry = tbx_listEntry.Text;
        //        foreach (var item in cbx_listEntrys.Items)
        //        {
        //            if (item.Equals(unwantedEntry))
        //            {
        //                cbx_listEntrys.SelectedIndex = -1;
        //                cbx_listEntrys.Items.Remove(item);
        //                int length = unwantedEntry.Length;
        //                int startAt = _Article.ListItems.IndexOf(unwantedEntry);
        //                _Article.ListItems = _Article.ListItems.Remove(startAt, (length + 1));
        //                tbx_listEntry.Text = " ";

        //                if (cbx_listEntrys.Items.Count == 0)
        //                {
        //                    hasContent -= 1;
        //                }
        //                break;
        //            }
        //        }
        //    }
        //}

        //private void link_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    string links = tbx_link1.Text + ";" + tbx_link2.Text + ";" + tbx_link3.Text;
        //    links = links.Trim();
        //    if (!links.Equals(";;") && String.IsNullOrWhiteSpace(_Article.ReferenceLinks))
        //    {
        //        hasContent += 1;
        //    }            

        //    if (links.Equals(";;") &&  !_Article.ReferenceLinks.Equals(";;"))
        //    {
        //        hasContent -= 1;
        //    }
        //    _Article.ReferenceLinks = links;
        //}


        //private void Right_Click(object sender, RoutedEventArgs e)
        //{
        //    _Article.PictureToLeftSide = false;
        //}

        //private void Left_Click(object sender, RoutedEventArgs e)
        //{
        //    _Article.PictureToLeftSide = true;
        //}

    }
}
