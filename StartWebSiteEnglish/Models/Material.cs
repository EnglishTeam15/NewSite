using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StartWebSiteEnglish.Models
{
    //Words
    #region
    public class LearnedMaterial
    {
        public int Id { get; set; }
    }

    public class CategoryWord
    {
        public int ID { get; set; }

        public string CategoryName { get; set; }
        public string PictureUrl { get; set; }
    }

    public abstract class AllWord
    {
        [Key]
        public int Id { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }

        public int Сomplexity { get; set; }
    }


    public class Words: AllWord
    {
        public string PictureUrl { get; set; }

        public string Type { get; set; }

        public int CategoryID { get; set; }
    }

    public class WordAdjective : AllWord
    {
        public string AntonymsID { get; set; }
    }
    #endregion

    public abstract class Material
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }

    }

    public class GrammerText:Material
    {
    }

    public class MaterialText:Material
    {
        public string NameTranslate { get; set; }

        public string Translate { get; set; }

        public string Complexity { get; set; }

        public int GetCountPage()
        {
            int count_page = 1 + Text.Length / 5000;
            return count_page;
        }
    }

    public class MaterialViewModel<T>
    {
        public IEnumerable<T> Materials { get; set; }
        public PageInfo pageInfo { get; set; }
    }
}