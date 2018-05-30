using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StartWebSiteEnglish.Models
{

    public class QuizModel
    {
        public int QuizID { get; set; }
        public string QuizName { get; set; }
        public int MaterialId { get; set; }
        public int GrammerId { get; set; }
        //public List<SelectListItem> ListOfQuizz { get; set; }
    }

    public class QuestionModel
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int QuizID { get; set; }
        //public string QuestionType { get; set; }
        public string Anwser { get; set; }
        public ICollection<ChoiceModel> Choices { get; set; }
    }

    public class ChoiceModel
    {
        public int ChoiceID { get; set; }
        public string ChoiceText { get; set; }
        public int QuestionID { get; set; }
    }

    public class AnswerMV
    {
        public int AnswerID { get; set; }
        //public string QuestionText { get; set; }
        public string AnswerQ { get; set; }
        public int QuestionID { get; set; }
        public bool isCorrect { get; set; }
    }

    //public class QuastionsMV<T>
    //{
    //    public T quastion { get; set; }

    //    public T[] answers { get; set; }

    //    public int correctAnwer { get; set; } = 0;

    //    public QuastionsMV(T quest, T[] words, int correct = 0)
    //    {
    //        quastion = quest;
    //        correctAnwer = correct;
    //        answers = words;
    //    }

    //    public bool ReplyCheck(int check)
    //    {
    //        if (correctAnwer == check)
    //            return true;
    //        return false;
    //    }
    //}

    public class TestTextModel
    {
        public ICollection<QuestionModel> Questions { get; set; }

        public string Text { get; set; }
    }

    public class AddTestModel
    {
        public string QuestionName { get; set; }
        public string[] Choise { get; set; }
        public int Answer { get; set; }
    }

    //[JsonObject]
    //public class WordTranslate
    //{
       
    //}

    [JsonObject]
    public class WordCard
    {
        [JsonProperty("word")]
        public string Word { get; set; }
        [JsonProperty("translate")]
        public string Translate { get; set; }
        [JsonProperty("image")]
        public string UrlPhoto { get; set; }
    }

    [JsonObject]
    public class TestWords
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("idquest")]
        public int IdQuestion { get; set; }

        [JsonProperty("variants")]
        public string[] Variants { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }
    }

}