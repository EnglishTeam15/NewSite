using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartWebSiteEnglish.Test
{
    public partial class Answer
    {
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public Nullable<int> QuestionID { get; set; }

        public virtual Question Question { get; set; }
    }

    public partial class Choice
    {
        public int ChoiceID { get; set; }
        public string ChoiceText { get; set; }
        public Nullable<int> QuestionID { get; set; }

        public virtual Question Question { get; set; }
    }
    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            this.Answers = new HashSet<Answer>();
            this.Choices = new HashSet<Choice>();
        }

        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public Nullable<int> QuizID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Choice> Choices { get; set; }
        public virtual Quiz Quiz { get; set; }
    }

    public partial class Quiz
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quiz()
        {
            this.Questions = new HashSet<Question>();
        }

        public int QuizID { get; set; }
        public string QuizName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
        public int? MaterilId { get; set; }
        public int? GrammerId { get; set; }
    }
}