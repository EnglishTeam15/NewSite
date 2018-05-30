using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StartWebSiteEnglish.Models;

namespace StartWebSiteEnglish.AdminClasses
{
    public static class EditMaterial
    {
        static MaterialContext db = new MaterialContext();

        //material
        public static bool AddMaterial(string name, string nametranslate, string text, string translate, string complexity)
        {
            db.Materialtexts.Add(new MaterialText { Name = name, NameTranslate = nametranslate, Text = text, Translate = translate, Complexity = complexity, Date = DateTime.Now });
            db.SaveChanges();
            return true;
        }

        public static bool ChangeText(int Id, string Name, string Text, string Translate, string Complexity)
        {
            var mater = db.Materialtexts.FirstOrDefault(c => c.Id == Id);
            mater.Name = Name;
            mater.Text = Text;
            mater.Complexity = Complexity;
            mater.Translate = Translate;
            mater.Date = DateTime.Now;
            db.SaveChanges();
            return true;
        }

        public static bool DeleteText(int Id)
        {
            MaterialText mater = db.Materialtexts.Where(m => m.Id == Id).FirstOrDefault();
            db.Materialtexts.Remove(mater);
            db.SaveChanges();

            return true;
        }

        internal static void AddTest(MaterialText material)
        {
            throw new NotImplementedException();
        }

        //grammer
        public static bool AddGrammer(string name, string text)
        {
            db.GrammerTexts.Add(new GrammerText { Name = name, Text = text, Date = DateTime.Now });
            db.SaveChanges();
            return true;
        }

        public static bool ChangeGremmer(int Id, string Name, string Text)
        {
            MaterialContext context = new MaterialContext();
            var grammer = context.GrammerTexts.FirstOrDefault(c => c.Id == Id);
            grammer.Name = Name;
            grammer.Text = Text;
            grammer.Date = DateTime.Now;
            context.SaveChanges();
            return true;
        }

        public static bool DeleteGrammer(int Id)
        {
            GrammerText grammer = db.GrammerTexts.Where(m => m.Id == Id).FirstOrDefault();
            db.GrammerTexts.Remove(grammer);
            db.SaveChanges();

            return true;
        }
    }
}