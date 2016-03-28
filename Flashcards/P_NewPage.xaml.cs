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
    /// Interaktionslogik für P_NewPage.xaml
    /// </summary>
    public partial class P_NewPage : System.Windows.Controls.Page
    {
        private int SubjectId;
        private int PageId;
        //private List<Article> Articles = new List<Article>();
        List<P_NewArticle> Pages = new List<P_NewArticle>();
        private int Index = 0;
        private Button btnRef;

        public P_NewPage()
        {
            InitializeComponent();
        }

        public P_NewPage(int subjectId)
        {
            InitializeComponent();
            SubjectId = subjectId;
            PageId = 0;
            //Article article = new Article();
            //article.SubjectId = subjectId;
            //NewArticle(article);
        }

        /// <summary>
        /// Aufruf zum Editieren eines bestehenden Page-Objekts
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="articles"></param>
        /// <param name="pageId"></param>
        //public P_NewPage(int subjectId, List<Article> articles, int pageId, string pageHeadline)
        //{
        //    InitializeComponent();
        //    SubjectId = subjectId;
        //    PageId = pageId;
        //    tbx_pageName.Text = pageHeadline;
            
        //    foreach (Article item in articles)
        //    {
        //        Article article = item;
        //        NewArticle(article);
        //    }
        //}

        /// <summary>
        /// Leitet eine Bilddatei an eines der existirenden P_NewArticle Pages weiter
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="target"></param>
        public void UpdatePicture(byte[] picture, string target)
        {
            int id = Convert.ToInt32(target.Substring(1));
            P_NewArticle article = Pages[id];
            //article.SetPicture(picture);
        }

        //Überarbeiten 
        //private void btn_SavePage_Click(object sender, RoutedEventArgs e)
        //{
        //    if (String.IsNullOrWhiteSpace(tbx_pageName.ToString()))
        //    {
        //        MessageBox.Show("Sie müssen dem Artikel mindestens einen Namen geben.");
        //    }
        //    else
        //    {
        //        int index = 0;
        //        Articles = new List<Article>();
        //        foreach (P_NewArticle item in Pages)
        //        {
        //            //Article article = item.Article;
        //            //article.Index = index;
        //            //Articles.Add(article);
        //            //index += 1;
        //        }
        //        DatabaseInteraction di = new DatabaseInteraction();
        //        if (PageId == 0)
        //        {
        //            bool saved = di.SavePage(tbx_pageName.Text, SubjectId, Articles);
        //            if (saved == true)
        //            {
        //                MessageBox.Show("Der Artikel wurde gespeichert.");
        //                CleanPage();
        //            }
        //        }
        //        else
        //        {
        //            //Datensatz editieren
        //            di.EditPage(tbx_pageName.Text, PageId, Articles);
        //        }
        //    }
        //}

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            //P_PageSelection page = new P_PageSelection();
            //MainWindow.f_tab2.Content = page;
        }

        /// <summary>
        /// Ruft die Seite neu auf
        /// </summary>
        private void CleanPage()
        {
            //MainWindow.f_tab2.Content = new P_NewPage(SubjectId);
        }

        /// <summary>
        /// Erstellt einen neuen Expander mit einem Artikel-Objekt darin
        /// </summary>
        /// <param name="article"></param>
        //private void NewArticle(Article article)
        //{
        //    string name = "a" + Index;
        //    int row = (g_Content.RowDefinitions.Count() - 1);
        //    if (row > 0)
        //    {
        //        g_Content.Children.Remove(btnRef);
        //    }            
        //    Expander container = new Expander(); container.Header = "Absatz " + (Index + 1); container.SetValue(Grid.RowProperty, row); 
        //    Frame frame = new Frame(); frame.Name = name;            
        //    //Articles.Add(article);
        //    //P_NewArticle page = new P_NewArticle(ref article, name);
        //    //Pages.Add(page);

        //    //frame.Content = page;
        //    //container.Content = frame;
        //    //g_Content.Children.Add(container);

        //    RowDefinition newRow = new RowDefinition(); 
        //    Button add = new Button(); add.Name = "Add_Article"; add.SetValue(Grid.RowProperty, (row + 1)); add.Height = 25; add.Width = 25; add.VerticalAlignment = VerticalAlignment.Top; add.HorizontalAlignment = HorizontalAlignment.Left; add.Margin = new Thickness(10); add.Click += Add_Article_Click; add.Content = "+";
        //    g_Content.RowDefinitions.Add(newRow);
        //    g_Content.Children.Add(add);
        //    btnRef = add;
        //    Index += 1;
        //}


        private void Add_Article_Click(object sender, RoutedEventArgs e)
        {
            //Article article = new Article();
            //article.SubjectId = SubjectId;
            //NewArticle(article);
        }
    }
}
