
var questions = [
    {
        type: "choose",
        question: "<h3>My name’s Tamara. I __________ twenty-one years old</h3>",
        answers: [
            "is", "am", "have", "got"
        ],
        correct: [2]
    },
    {
        type: "choose",
        question: "<h3>“Is Igor a teacher?” “ No, __________ .”</h3>",
        answers: [
            "he not", "it isn't", "he doesn't", "he isn't"
        ],
        correct: [4]
    },
    {
        type: "choose",
        question: "<h3>Найтите правильный гагол: to play, to smile, to laugh, to see.</h3>",
        answers: [
            "to play", "to smile", "to laugh", "to see"
        ],
        correct: [4]
    } //,
    //{
    //    type: "choose",
    //    question: "<h3>Найдите ошибку в трех формах глагола:</h3>",
    //    answers: [
    //        "teach - taught - taught", "catch - caugth - caught", "bring - braught - braught", "seek - sought - sought"
    //    ],
    //    correct: [3]
    //},
    //{
    //    type: "choose",
    //    question: "<h3>Karina never minds ....... the movie again.</h3>",
    //    answers: [
    //        "to watch", "to be watched", "watch", "watching"
    //    ],
    //    correct: [4]
    //},
    //{
    //    type: "choose",
    //    question: "<h3>I couldn'd help ........... .</h3>",
    //    answers: [
    //        "for laughing", "and laughed", "laughing", "to laughed"
    //    ],
    //    correct: [3]
    //},
    //{
    //    type: "choose",
    //     question: "<h3>Я знаю его четыре года.</h3>",
    //    answers: [
    //        "I know him four years.", "I have been him for four years.", "I know him for four years", "I have known him for four years"
    //    ],
    //    correct: [4]
    //},
    //{
    //    type: "choose",
    //     question: "<h3>Выберите наиболее подходящий ответ! “What is she doing?”</h3>",
    //    answers: [
    //        "She is playing with the bunny.", "She is a manager.", "She cleans the house every day.", "She is clean the carpet."
    //    ],
    //    correct: [1]
    //},
    //{
    //    type: "choose",
    //      question: "<h3>This particular college has a very selective ................. policy</h3>",
    //    answers: [
    //        "acceptance", "entrance", "admissions", "admittance"
    //    ],
    //    correct: [3]
    //},
    //{
    //    type: "choose",
    //       question: "<h3>An obstetrician/gynecologist at the pre-conception clinic suggests we ................. some further tests.</h3>",
    //    answers: [
    //        "doing", "to do", "are doing", "should do"
    //    ],
    //    correct: [4]
    //},
    //{
    //    type: "choose",
    //     question: "<h3>Igor and Tamara are __________ people.</h3>",
    //    answers: [
    //        "teacher", "nice", "long", "intelligents"
    //    ],
    //    correct: [2]
    //},


     //{
    //    type: "choose",
    //    question: "<h3>Найдите ошибку в трех формах глагола:</h3>",
    //    answers: [
    //        "teach - taught - taught", "catch - caugth - caught", "bring - braught - braught", "seek - sought - sought"
    //    ],
    //    correct: [3]
    //},
    //{
    //    type: "choose",
    //    question: "<h3>Karina never minds ....... the movie again.</h3>",
    //    answers: [
    //        "to watch", "to be watched", "watch", "watching"
    //    ],
    //    correct: [4]
    //},
    //{
    //    type: "choose",
    //    question: "<h3>I couldn'd help ........... .</h3>",
    //    answers: [
    //        "for laughing", "and laughed", "laughing", "to laughed"
    //    ],
    //    correct: [3]
    //},
    //{
    //    type: "choose",
    //     question: "<h3>Я знаю его четыре года.</h3>",
    //    answers: [
    //        "I know him four years.", "I have been him for four years.", "I know him for four years", "I have known him for four years"
    //    ],
    //    correct: [4]
    //},
    //{
    //    type: "choose",
    //     question: "<h3>Выберите наиболее подходящий ответ! “What is she doing?”</h3>",
    //    answers: [
    //        "She is playing with the bunny.", "She is a manager.", "She cleans the house every day.", "She is clean the carpet."
    //    ],
    //    correct: [1]
    //},
    //{
    //    type: "choose",
    //      question: "<h3>This particular college has a very selective ................. policy</h3>",
    //    answers: [
    //        "acceptance", "entrance", "admissions", "admittance"
    //    ],
    //    correct: [3]
    //},
    //{
    //    type: "choose",
    //       question: "<h3>An obstetrician/gynecologist at the pre-conception clinic suggests we ................. some further tests.</h3>",
    //    answers: [
    //        "doing", "to do", "are doing", "should do"
    //    ],
    //    correct: [4]
    //},
    //{
    //    type: "choose",
    //     question: "<h3>Igor and Tamara are __________ people.</h3>",
    //    answers: [
    //        "teacher", "nice", "long", "intelligents"
    //    ],
    //    correct: [2]
    //},


     //{
    //    type: "choose",
    //    question: "<h3>Найдите ошибку в трех формах глагола:</h3>",
    //    answers: [
    //        "teach - taught - taught", "catch - caugth - caught", "bring - braught - braught", "seek - sought - sought"
    //    ],
    //    correct: [3]
    //},
    //{
    //    type: "choose",
    //    question: "<h3>Karina never minds ....... the movie again.</h3>",
    //    answers: [
    //        "to watch", "to be watched", "watch", "watching"
    //    ],
    //    correct: [4]
    //},
    //{
    //    type: "choose",
    //    question: "<h3>I couldn'd help ........... .</h3>",
    //    answers: [
    //        "for laughing", "and laughed", "laughing", "to laughed"
    //    ],
    //    correct: [3]
    //},
    //{
    //    type: "choose",
    //     question: "<h3>Я знаю его четыре года.</h3>",
    //    answers: [
    //        "I know him four years.", "I have been him for four years.", "I know him for four years", "I have known him for four years"
    //    ],
    //    correct: [4]
    //},
    //{
    //    type: "choose",
    //     question: "<h3>Выберите наиболее подходящий ответ! “What is she doing?”</h3>",
    //    answers: [
    //        "She is playing with the bunny.", "She is a manager.", "She cleans the house every day.", "She is clean the carpet."
    //    ],
    //    correct: [1]
    //},
    //{
    //    type: "choose",
    //      question: "<h3>This particular college has a very selective ................. policy</h3>",
    //    answers: [
    //        "acceptance", "entrance", "admissions", "admittance"
    //    ],
    //    correct: [3]
    //},
    //{
    //    type: "choose",
    //       question: "<h3>An obstetrician/gynecologist at the pre-conception clinic suggests we ................. some further tests.</h3>",
    //    answers: [
    //        "doing", "to do", "are doing", "should do"
    //    ],
    //    correct: [4]
    //},
    //{
    //    type: "choose",
    //     question: "<h3>Igor and Tamara are __________ people.</h3>",
    //    answers: [
    //        "teacher", "nice", "long", "intelligents"
    //    ],
    //    correct: [2]
    //},

];