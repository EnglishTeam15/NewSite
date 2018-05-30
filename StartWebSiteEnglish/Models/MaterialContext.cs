using StartWebSiteEnglish.Test;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StartWebSiteEnglish.Models
{
    public class MaterialContext : DbContext
    {
        public MaterialContext() : base("materialsDB")
        {
        }

        public static MaterialContext Create()
        {
            return new MaterialContext();
        }
        public DbSet<MaterialText> Materialtexts { get; set; }

        public DbSet<GrammerText> GrammerTexts { get; set; }

        public DbSet<Words> Words { get; set; }

        public DbSet<CategoryWord> CategoryWords { get; set; }


        //test tables
        public DbSet<Quiz> Quizs { get; set; }

        public DbSet<Question> Questions { get; set; }

       public DbSet<Answer> Answers { get; set; }

        public DbSet<Choice> Choices { get; set; }

        public DbSet<WordAdjective> WordsAdjectives { get; set; }
    }

    //public class MaterialDbInitializer : DropCreateDatabaseAlways<MaterialContext>
    //{
    //    protected override void Seed(MaterialContext context)
    //    {
    //        context.Materials.Add(new Material
    //        {
    //            Id=1,
    //            Name = "Apple",
    //            Text = "Основой биполярного транзистора является кристалл полупроводника" +
    //            " также как и вывод от него называется базой. Диффузией примеси или сплавлением с двух сторон от базы образуются области" +
    //            " с противоположным типом проводимости, нежели база Область,имеющая бóльшую площадь p - n перехода, и вывод от неѐ называют коллектором.Область",
    //            Ratting = 1,
    //            CountPage = 1,
    //            dateTime = DateTime.Now
    //        });
    //        context.Materials.Add(new Material
    //        {
    //            Id =2,
    //            Name = "Orange",
    //            Text = "Основой биполярного транзистора является кристалл полупроводника" +
    //            " p-типа или n-типа проводимости, который также как и вывод от него называется базой. Диффузией примеси или сплавлением с двух сторон от базы образуются области" +
    //            " с противоположным типом проводимости, нежели база Область,имеющая бóльшую площадь p - n перехода, и вывод от неѐ называют коллектором.Область",
    //            Ratting = 1,
    //            CountPage = 1,
    //            dateTime = DateTime.Now
    //        });
    //        context.Materials.Add(new Material
    //        {
    //            Id =3,
    //            Name = "Peach",
    //            Text = "Основой биполярного транзистора является кристалл полупроводника" +
    //            " p-типа или n-типа проводимости, который также как и вывод от него называется базой. Диффузией примеси или сплавлением с двух сторон от базы образуются области" +
    //            " с противоположным типом проводимости, нежели база Область,имеющая бóльшую площадь p - n перехода, и вывод от неѐ называют коллектором.Область",
    //            Ratting = 1,
    //            CountPage = 1,
    //            dateTime = DateTime.Now
    //        });
    //        base.Seed(context);
    //    }
    //}
}