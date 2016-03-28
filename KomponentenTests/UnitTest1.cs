using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flashcards.Classes;
using Flashcards;
using System.IO;
using System.Linq;
using Flashcards.DataTyps;
using System.Windows.Media.Imaging;
using System.Text;

namespace KomponentenTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            FileManagement m = new FileManagement();
            m.GetFileDirectories();
            //var drives = DriveInfo.GetDrives().Select(x => x.Name).ToList(); 
            int i = 0;
        }

        [TestMethod]
        public void CreateCategoryTest()
        {
            DataManagement.MainFolder();
            Category cat = new Category();
            cat.Name = "T>est  >hedf<<<<suihf#hsfu";
            var n = cat.Name;
            cat.ImagePath =@"D:\Images\___kiwi_by_picolo_kun-d8v446v.jpg";
            cat.CreateCategory();
        }

        [TestMethod]
        public void GetDirectories()
        {
            DataManagement.MainFolder();
            Category.DeleteCategory("a89f2968");

            //var i = StringBuilderExtended.GetEntries(new StringBuilder("[]"));
            //var r = i[0].ToString();

            //DataManagement.MainFolder();
            //var items = Category.GetCategories();
        }

        [TestMethod]
        public void CreateNewCard()
        {
            DataManagement.MainFolder();
            Card card = new Card();
            card.Question = "ch<sihghyughyfhgoiysijgpydijvgyjdigjyogiyhgijyfidguhfli";
            card.Answer = "fsuih<dfk<opkfo<kofk<opdfkp<ojfjifhghybfjvnynvyjknvfylkgykfjg yjgkl<gklf<lkyg kl< gfglk<yfglkyfsohdfoidsf";
            card.ImageForFront = @"D:\Images\___kiwi_by_picolo_kun-d8v446v.jpg";
            card.ImageForBack = @"D:\Images\___kiwi_by_picolo_kun-d8v446v.jpg";

            card.CreateCard("f24ffd2d");
        }



    }
}
