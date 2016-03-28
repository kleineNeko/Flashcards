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
    /// Interaktionslogik für P_Articles.xaml
    /// </summary>
    public partial class P_Articles : Page
    {

        string CategoryId;        

        public P_Articles(string categoryId)
        {
            InitializeComponent();
            CategoryId = categoryId;
            //FillLists();
        }

        /// <summary>
        /// Füllt die Listen für die angelegten Artikel
        /// </summary>
        private void FillLists()
        {
            //List<DatabaseTyps.Page> pages = new List<DatabaseTyps.Page>();
            //DatabaseInteraction di = new DatabaseInteraction();
            //pages = di.GetPagesBySubject(CategoryId);
            //bool left = true;

            //foreach (DatabaseTyps.Page item in pages)
            //{
            //    Button btn_OpenPage = new Button(); btn_OpenPage.Name = "p" + item.PageId; btn_OpenPage.Content = item.Headline;
            //    btn_OpenPage.Click += OpenPage_Click;
            //    if (left == true)
            //    {
            //        lbx_left.Items.Add(btn_OpenPage);
            //        left = false;
            //    }
            //    else
            //    {
            //        lbx_right.Items.Add(btn_OpenPage);
            //        left = true;                   
            //    }
            //}
        }

        private void OpenPage_Click(object sender, RoutedEventArgs e)
        {
            //Button btn = (Button)sender;
            //int pageId = Convert.ToInt32(btn.Name.Substring(1));
            //P_Page page = new P_Page(pageId);
            //MainWindow.f_tab2.Content = page;

        }

        private void btn_NewArticle_Click(object sender, RoutedEventArgs e)
        {
            P_NewArticle page = new P_NewArticle(CategoryId);
            MainWindow.Frame.Content = page;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Page page =  new P_CardSelection();
            MainWindow.Frame.Content = page;
        }
    }
}
