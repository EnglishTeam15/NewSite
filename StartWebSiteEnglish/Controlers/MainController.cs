using StartWebSiteEnglish.AdminClasses;
using StartWebSiteEnglish.Attribute;
using StartWebSiteEnglish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace StartWebSiteEnglish.Controlers
{
    [Authorize]
    public class MainController : Controller
    {
        MaterialContext db = new MaterialContext();
        [HttpGet]
        public ActionResult Main()
        {
            Session["WordQuestions"] = null;
            return View();
        }

        //text materials
        #region
        public ViewResult Material(string sortOrder, string searchString, int page = 1)
        {
            Session["WordQuestions"] = null;
            MaterialViewModel<MaterialText> viewModel = new MaterialViewModel<MaterialText>();
            //using (MaterialContext db = new MaterialContext())
            //{
            int pageSize = 25;
            if (sortOrder == null && searchString == null)
            {
                ViewBag.CountText = db.Materialtexts.Count();
            }
            //ViewBag.VolumeSortParm = String.IsNullOrEmpty(sortOrder) ? "Volume desc" : "";
            ViewBag.LevelSortParm = String.IsNullOrEmpty(sortOrder) ? "Level desc" : "";
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "Date desc";

            var mattext = (from s in db.Materialtexts select s).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                mattext = mattext.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            switch (sortOrder)
            {
                case "Level desc":
                    mattext = mattext.OrderByDescending(s => s.Complexity).ToList();
                    break;
                case "Date":
                    mattext = mattext.OrderByDescending(s => s.Date).ToList();
                    break;
                case "Date desc":
                    mattext = mattext.OrderBy(s => s.Date).ToList();
                    break;
                default:
                    mattext = mattext.OrderBy(s => s.Id).ToList();
                    break;
            }
            viewModel = new MaterialViewModel<MaterialText>
            {
                Materials = mattext
           //.OrderBy(material => material.Id)
           .Skip((page - 1) * pageSize)
           .Take(pageSize),

                pageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = mattext.Count()
                }
            };
            ViewBag.CountText = mattext.Count();
            // return View(mattext.ToPagedList(pageIndex,pageSize));
            //}
            return View(viewModel);
        }

        [HttpGet]
        public ViewResult TextReading(int id)
        {
            using (MaterialContext db = new MaterialContext())
            {
                try
                {
                    ViewBag.CountText = db.Materialtexts.Count();
                    MaterialText material = db.Materialtexts.FirstOrDefault(u => u.Id == id);
                    ViewBag.TitlePage = material.Name;
                    Session["TextReading"] = material;
                    return View(material);
                }
                catch
                {
                    return View();
                }
            }
        }

        public ActionResult TestsText(int id)
        {
            //MaterialText material = (MaterialText)Session["TextReading"];
            //if (material != null)
            //{
            using (MaterialContext db = new MaterialContext())
            {
                try
                {
                    var material = db.Materialtexts.FirstOrDefault(u => u.Id == id);
                    var quiz = db.Quizs.Where(s => s.MaterilId == id).FirstOrDefault();
                    ViewBag.Title = quiz.QuizName;
                    IQueryable<QuestionModel> questions = db.Questions.Where(q => q.QuizID == quiz.QuizID)
                       .Select(q => new QuestionModel
                       {
                           QuestionID = q.QuestionID,
                           QuestionText = q.QuestionText,
                           Choices = q.Choices.Select(c => new ChoiceModel
                           {
                               ChoiceID = c.ChoiceID,
                               ChoiceText = c.ChoiceText
                           }).ToList()

                       });
                    TestTextModel testText = new TestTextModel();
                    testText.Text = material.Text;
                    testText.Questions = questions.ToList();
                    return View(testText);
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            //}
            //return View("Material");
        }

        [HttpPost]
        public ActionResult TestsText(List<AnswerMV> resultQuiz)
        {
            using (MaterialContext db = new MaterialContext())
            {
                List<AnswerMV> finalResultQuiz = new List<AnswerMV>();
                foreach (AnswerMV answser in resultQuiz)
                {
                    AnswerMV result = db.Answers.Where(a => a.QuestionID == answser.QuestionID).Select(a => new AnswerMV
                    {
                        QuestionID = a.QuestionID.Value,
                        AnswerQ = a.AnswerText,
                        isCorrect = (answser.AnswerQ.ToLower().Equals(a.AnswerText.ToLower()))

                    }).FirstOrDefault();

                    finalResultQuiz.Add(result);
                }
                return Json(new { result = finalResultQuiz }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //traing 
        #region
        public ActionResult CardWord()
        {
            ViewBag.Title = "Тренировка Карточки слов";
            List<Words> words = Session["WordQuestions"] as List<Words>;
            //return View(words);
            List<WordCard> cards = new List<WordCard>();
            foreach (var x in words) {
                cards.Add(new WordCard {Word=x.Word, Translate=x.Translation, UrlPhoto=x.PictureUrl });
            }
            string sJon = JsonConvert.SerializeObject(cards);
            return View(cards);
        }

        [HttpGet]
        public ActionResult Traning()
        {
            List<Words> word = Session["WordQuestions"] as List<Words>;
            if (word != null)
            {
                ViewBag.ErrorLearn = string.Format("Вы выбрали {0} слов", word.Count);
                return View();
            }
            else
            {
                DropDowmModel dropDowmModel = new DropDowmModel();
                List<SelectListItem> listItems = new List<SelectListItem>();
                listItems.Add(new SelectListItem() { Value = "5", Text= "5 слов" });
                listItems.Add(new SelectListItem() { Value = "10", Text = "10 слов" });
                listItems.Add(new SelectListItem() { Value = "15", Text = "15 слов" });
                ViewBag.DropDownValues = new SelectList(listItems, "Text", "Value");
                ViewBag.ErrorLearn = "Выберите минимум 5 слов в наборах слов";
                dropDowmModel.CountWord = listItems;
                return View(dropDowmModel);
            }
        }

       [HttpPost]
       public ActionResult ShowWords(DropDowmModel drop)
        {
            Random rand = new Random();
            List<Words> words = Session["WordQuestions"] as List<Words>;
            if (words == null)
            {
                words = new List<Words>();
                for (int i = 0; i < drop.Id; i++)
                {
                    int id = rand.Next(1, 220);
                    words.Add(db.Words.FirstOrDefault(s => s.Id == id));
                }
            }
            return View("ChoiceWord",words );
        }

        public ActionResult WordTranslate()
        {
            ViewBag.Title = "Тренировка Слово - Перевод";
            List<Words> words = Session["WordQuestions"] as List<Words>;
            int traint = 1;
            if (GetQuesstionWors(traint, words))
            {
                return View();
            }
            return RedirectToAction("Traning");
        }

        //метод для добавления правильных отвечанных слов в словарь изучения пользователя
        //нужно сделать
        [HttpPost]
        public JsonResult WordTranslate(int[] id)
        {
            List<Words> words = Session["WordQuestions"] as List<Words>;
            for(int i=0; i<id.Length; i++)
            {
                var word =words.FirstOrDefault(s => s.Id == id[i]);
                //ApplicationUser user = Session["User"] as ApplicationUser;
                //using(ApplicationContext dbapp = new ApplicationContext())
                //{
                    
                //}
            }


            return Json(new { response ="Sucsses"  }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TranslateWord()
        {
            ViewBag.Title = "Тренировка Перевод - Слово";
            int traint = 2;
            List<Words> words = Session["WordQuestions"] as List<Words>;
            if (GetQuesstionWors(traint, words))
            {
                return View("WordTranslate");
            }
            else return RedirectToAction("Traning");

        }

        public bool GetQuesstionWors(int traning, List<Words> words)
        {
            int countchoice = 4;
            Random rand = new Random();
            List<TestWords> quastions = new List<TestWords>();
            //List<Words> words = (List<Words>)Session["WordQuestions"];
            if (words != null)
            {
                try
                {
                    int count = words.Count;
                    ViewBag.CountQuest = count;
                    int countword = db.Words.Count();
                    for (int i = 0; i < count; i++)
                    {
                        string[] varians = new string[countchoice];
                        if (traning == 1)
                        {
                            varians[0] = words[i].Translation;
                            for (int j = 1; j < 4; j++)
                            {
                                int wordid = rand.Next(1, 200);
                                varians[j] = db.Words.FirstOrDefault(w => w.Id == wordid).Translation;
                            }
                            quastions.Add(new TestWords() {IdQuestion=words[i].Id, Question = words[i].Word, Variants = varians, Answer = varians[0] });

                        }
                        else if (traning == 2)
                        {
                            varians[0] = words[i].Word;
                            for (int j = 1; j < 4; j++)
                            {
                                int wordid = rand.Next(1, 200);
                                varians[j] = db.Words.FirstOrDefault(w => w.Id == wordid).Word;
                            }
                            quastions.Add(new TestWords() { IdQuestion = words[i].Id, Question = words[i].Translation, Variants = varians, Answer = varians[0] });
                        }
                    }
                    //}
                    string sJon = JsonConvert.SerializeObject(quastions);
                    ViewBag.WordQuestions = sJon;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            ViewBag.ErrorLearn = "Выберите минимум 5 слов в наборах слов";
            return false;
        }
        #endregion

        //grammer
        #region
        public ActionResult Grammer(int page = 1)
        {
            Session["WordQuestions"] = null;
            int pageSize = 10;
            MaterialViewModel<GrammerText> viewModel = new MaterialViewModel<GrammerText>
            {
                Materials = db.GrammerTexts
               .OrderBy(material => material.Id)
               .Skip((page - 1) * pageSize)
               .Take(pageSize),

                pageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = db.GrammerTexts.Count()
                }
            };
            return View(viewModel);
        }

        public ActionResult LearnGrammer(int id)
        {
            GrammerText material = db.GrammerTexts.FirstOrDefault(u => u.Id == id);
            ViewBag.TitlePage = material.Name;
            ViewData["GrammerTest"] = material;
            return View(material);
        }
        //прохождение теста для определения уровня
        [HttpGet]
        public ActionResult FirstTest()
        {
            return View();
        }

        //доработать добавления уровня занания языка после прохождения теста
        [HttpPost]
        public JsonResult FirstTest(int count, int allquestion)
        {
            int level = 0;
            double prec = count * 100 / allquestion;
            if (prec < 30)
            {
                level = 1;
            }
            else if (prec > 30 && prec < 60)
            {
                level = 2;
            }
            else
                level = 3;
            ApplicationUser user = Session["User"] as ApplicationUser;
            user.LevelProgress = level;
            ViewBag.YourLevel = string.Format("Ваш уровень знания языка " + level.ToString());
            return Json(new { result = ViewBag.YourLevel });

        }

        public ActionResult GrammerTest(int id)
        {
            using (MaterialContext db = new MaterialContext())
            {
                try
                {
                    GrammerText grammer = db.GrammerTexts.FirstOrDefault(u => u.Id == id);
                    var quiz = db.Quizs.Where(s => s.GrammerId == grammer.Id).FirstOrDefault();
                    ViewBag.Title = quiz.QuizName;
                    IQueryable<QuestionModel> questions = db.Questions.Where(q => q.QuizID == quiz.QuizID)
                       .Select(q => new QuestionModel
                       {
                           QuestionID = q.QuestionID,
                           QuestionText = q.QuestionText,
                           Choices = q.Choices.Select(c => new ChoiceModel
                           {
                               ChoiceID = c.ChoiceID,
                               ChoiceText = c.ChoiceText
                           }).ToList()

                       });
                    TestTextModel testText = new TestTextModel();
                    testText.Text = null;
                    testText.Questions = questions.ToList();
                    return View("TestsText", testText);
                }
                catch (Exception ex)
                {
                    return View("TestsText");
                }
            }
        }
        #endregion

        //words
        #region
        public ActionResult AllDictionary()
        {
            Session["WordQuestions"] = null;
            using (MaterialContext db = new MaterialContext())
            {
                List<CategoryWord> viewModel = db.CategoryWords.ToList();
                return View(viewModel);
            }
        }

        public ActionResult ChoiceWord(int Id)
        {
            ViewBag.TitleChoiceWord = db.CategoryWords.FirstOrDefault(s => s.ID == Id).CategoryName;
            var words = db.Words.Where(s => s.CategoryID == Id).ToList();
            ViewBag.ChoiceWordsMessage = "Выберите минимум 5 слов для изучения";
          //  Session["WordQuestions"] = words;
            return View(words);
        }

        [HttpPost]
        public ActionResult ChoiceWord(string[] id)
        {
            List<Words> words = new List<Words>();
            for (int i = 0; i < id.Length; i++)
            {
                int a = int.Parse(id[i]);
                var x = db.Words.FirstOrDefault(s => s.Id == a);
                words.Add(x);
            }
            Session["WordQuestions"] = words;
            return Json(new { success = id });
        }

        public JsonResult ReplyAnswer(string answerId, string questId)
        {
            int result = 0;
            using (MaterialContext dbcon = new MaterialContext())
            {
                var word = dbcon.Words.FirstOrDefault(s => s.Id == int.Parse(answerId));
                if (word.Word == questId)
                {
                    result = 1;
                }
            }
            return Json(result);
        }
        #endregion
    }
}