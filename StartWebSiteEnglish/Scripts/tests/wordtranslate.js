function QuizItem(idquest, question, variants, answer, enabled, replied, selectionOfUser) {
    this.idquest = idquest;
    this.question = question;
    this.variants = variants;
    this.answer = answer;
    this.enabled = enabled;
    this.replied = replied;
    this.selectionOfUser = selectionOfUser;
}

function CurrentWord(idquest) {
    this.idquest = idquest;
}

Array.prototype.rand = function () {
    return this.sort(function () { return 0.5 - Math.random(); });
}
var quizQuestions = [];
var currentidword = [];
var dates = document.getElementById("datewords").innerText;
var questions = JSON.parse(dates);
for (var i = 0; i < questions.length; i++) {
    quizQuestions.push(new QuizItem(questions[i].idquest, questions[i].question, questions[i].variants, questions[i].answer, false, false, undefined));
}

// pTag.innerHTML = test1.question;
// console.log(quizQuestions[0]);
var currentIndex = 0, numOfAnswered = 0;
var currentQuestion = quizQuestions.rand()[currentIndex];
//second ulTag 
var ulTag = document.getElementsByTagName('ul')[1];
var liTags = ulTag.getElementsByTagName('li');
/*
	this function inserts the current question into the layout
	of the page: p tag which is a question and ul tag meaning
	an options
*/


function showCurrentQuestion(IsRand) {

    var headerOfDropdow = document.getElementsByClassName('wrapper')[0];
    //parse into integer, because it interpretes it as a string
    var numQuestion = parseInt(currentIndex) + 1;
    headerOfDropdow.getElementsByTagName('span')[0].innerHTML = numQuestion;
    var pTag = document.getElementsByTagName('p')[0];
    // console.log(liTags);
    var ulTag = document.getElementsByTagName('ul')[1];
    var liTags = ulTag.getElementsByTagName('li');
    pTag.innerHTML = currentQuestion.question;
    // currentQuestion.variants = shuffle(currentQuestion.variants);
    if (IsRand == true) {
        currentQuestion.variants.rand();
    }
    for (var i = 0; i < liTags.length; i++) {
        //in case the number of variants is less than 4 (e.g. when it's
        // undefined) disable li tag
        if (currentQuestion.variants[i] == undefined) {
            console.log(currentQuestion.variants[i]);
            liTags[i].className = "doNotDisplay";
        } else {
            console.log(currentQuestion.variants[i]);
            liTags[i].innerHTML = currentQuestion.variants[i];//assign question
            liTags[i].className = "";
        }
    }
};

enableLiOnClickEvents();
showCurrentQuestion(true);

//when a variant is selected it becomes highlighted 
function changeLiStyle() {
    var selectedItem = document.getElementsByClassName('selected')[0];
    //disable previously selected item and enable the clicked one
    if (selectedItem) selectedItem.className = "";
    this.className = "selected";
}


//self-invoking function to find all li tags 
// and assing them text from the object 
// and assign event listeners
function enableLiOnClickEvents() {
    for (var i = 0; i < liTags.length; i++) {
        console.log(liTags[i]);
        liTags[i].onclick = changeLiStyle;
    }
};

var button = document.getElementsByClassName('submit')[0];
button.onclick = submitAndCheckAnswer;

function submitAndCheckAnswer() {
    var selectedItem = document.getElementsByClassName('selected')[0];
    /*	console.log(selectedItem.innerHTML);*/
    if (selectedItem == undefined)
        alert("Вы ничего не выбрали! Пожалуйста выберите ответ!");
    else {
        
        currentQuestion.enabled = true;
        if (selectedItem.innerHTML == currentQuestion.answer) {
            currentidword.push(currentQuestion.idquest);
            console.log("Correct " + currentQuestion.variants.indexOf(selectedItem.innerHTML));
            changeTheLayoutAccordingTheResult(selectedItem, "correct", true);
            checkIfTheLastQuestion(this);//sending button obj as a parameter
            numOfAnswered++;

        } else {
            console.log("Wrong!");
            changeTheLayoutAccordingTheResult(selectedItem, "wrong", false);
            checkIfTheLastQuestion(this);
            //указание правльного варианта ответа на вопрос
            var index = find(currentQuestion.variants, currentQuestion.answer)
            liTags[index].className = "correct";
        }
    }
}

//поиск правильного ответа в массиве вариантов
function find(array, value) {
    if (array.indexOf) { // если метод существует
        return array.indexOf(value);
    }
    for (var i = 0; i < array.length; i++) {
        if (array[i] === value) return i;
    }
    return -1;
}

function changeTheLayoutAccordingTheResult(selectedItem, result, replied) {
    console.log(result);
    currentQuestion.replied = replied;
    //the index corresponding to the selection of user is selectiOfUser
    currentQuestion.selectionOfUser = currentQuestion.variants.indexOf(selectedItem.innerHTML);
    selectedItem.className = result;//changing color of selected item by changing className
    disableLiOnClickEvents();//cannot click on the other litags anymore
}

//if the current question is the last one then change button style
//and onclick event(function)
//to finalize, otherwise continue to the next question 
function checkIfTheLastQuestion(button) {
    console.log("currentIndex: ", currentIndex);
    if (currentIndex == quizQuestions.length - 1) {
        console.log(currentIndex + " " + quizQuestions.length);
        button.className = "finalize";//change the color
        button.innerHTML = "Завершить";
        button.onclick = finalize;//change event listener

    } else {
      //  console.log(currentIndex + "fdsf " + quizQuestions.length);
        currentIndex++;
        button.innerHTML = "Следующий";
        button.className = "next";
        button.onclick = goToNextQuestion;
    }
}

function disableLiOnClickEvents() {
    for (var i = 0; i < liTags.length; i++) {
        liTags[i].onclick = "";
    }
}

function goToNextQuestion() {
    // if (currentIndex == quizQuestions.length) {
    // 	finalize();
    // 	return alert("Quiz is over. Your result: " + numOfAnswered);
    // }
    //changes the current question index before moving to the next one
    currentQuestion = quizQuestions[currentIndex];
    //change button's label and event handler
    this.innerHTML = "Проверить";
    this.onclick = submitAndCheckAnswer;
    this.className = "submit";
    showCurrentQuestion(true);
    enableLiOnClickEvents();
}

function cleanUpTheLayout() {
    var mainDiv = document.getElementsByClassName('main')[0];
    // deleting all child nodes 
    while (mainDiv.hasChildNodes()) {
        mainDiv.removeChild(mainDiv.firstChild);
    }
    console.log("clean UPP!!");
}

$(".finalize").click(function () {
             $.ajax({
            type: 'POST',
            url: '/Main/WordTranslate',
                 data: { id: currentidword },
            datatupe: 'json',
            success: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log("Error");
            }
    });
    //$('#finishTraing').css('display', 'block');
    });

function finalize() {
    //function() {
    //    $.ajax({
    //        type: 'POST',
    //        url: '/Main/WordTranslate',
    //        data: { currentidword },
    //        success: function (response) {
    //            console.log(response);
    //        },
    //        error: function (response) {
    //            console.log(response);
    //        }
    //    });
    //};
   

    //var request = new XMLHttpRequest();
    //var body = urrentidword;
    //request.open("POST", "/Main/WordTranslate");
    //request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    //request.onreadystatechange = reqReadyStateChange;
    //request.send(body);


    cleanUpTheLayout();

    var mainDiv = document.getElementsByClassName('main')[0];

    var tHeader = document.createElement("p");
    tHeader.appendChild(document.createTextNode("Посмотрите ваши результаты"));
    tHeader.setAttribute("class", "pAboveTable");
    mainDiv.appendChild(tHeader);
    var table = document.createElement("table");
    // table.border='1px';
    var tr = document.createElement("tr");
    table.appendChild(tr);
    var heading = ["Вопросы", "Ваши результаты", "Правильные варианты"];

    for (var i = 0; i < heading.length; i++) {
        var th = document.createElement("th");
        th.width = '200px';
        th.appendChild(document.createTextNode(heading[i]));
        tr.appendChild(th);
        console.log(tr);
    }

    for (var i = 0; i < quizQuestions.length; i++) {

        var tr = document.createElement('tr');
        var td = document.createElement('td');
        td.appendChild(document.createTextNode(quizQuestions[i].question));
        td.setAttribute("class", "questionCol");
        tr.appendChild(td);
        var td = document.createElement('td');

        var answer = quizQuestions[i].replied ? (
            td.className = "correctCol",
            quizQuestions[i].answer
        ) : (
                td.className = "wrongCol",
                quizQuestions[i].variants[quizQuestions[i].selectionOfUser]
            );

        td.appendChild(document.createTextNode(answer));
        tr.appendChild(td);
        var td = document.createElement('td');
        if (!quizQuestions[i].replied) {
            var correctAns = quizQuestions[i].answer;
            td.appendChild(document.createTextNode(correctAns));
            td.setAttribute("class", "correctCol");
        }
        tr.appendChild(td);

        table.appendChild(tr);
      
    }
    

    mainDiv.appendChild(table);
    var trAll = document.getElementsByTagName("tr");
    console.log(trAll);

    //for (var i = 1; i < trAll.length; i++) {
    //    trAll[i].onclick = returnToQuestion;
    //    console.log("Assigned!");
    //}
    //document.getElementById('finishTraing').style.display = "block";

    // var head2 = document.createElement("th");
    // head2.appendChild(document.createTextNode("Your Result"));
    // tr.appendChild(head2);
    // document.body.appendChild(table);

    
}
//dynamicaally creates the question layout when clicked on any of the questions in the result table
function createQuestionLayout() {
    var mainDiv = document.getElementsByClassName('main')[0];
    var wrapperDiv = document.createElement('div');
    wrapperDiv.className = "wrapper";
    wrapperDiv.onclick = "showDropdown";
    mainDiv.appendChild(wrapperDiv);
    for (var j = 0; j < 2; j++) {
        var span = document.createElement('span');
        wrapperDiv.appendChild(span);
    }
    span.innerHTML = "/ " + quizQuestions.length;
    var ulDdown = document.createElement('ul');
    ulDdown.className = "dropdown";
    mainDiv.appendChild(ulDdown);
    var pTag = document.createElement('p');
    pTag.className = "question";
    var ulTag = document.createElement('ul');
    mainDiv.appendChild(pTag);
    mainDiv.appendChild(ulTag);
    for (var i = 0; i < 4; i++) {
        var liTag = document.createElement('li');
        ulTag.appendChild(liTag);
        var liTag1 = document.createElement('li');
        ulDdown.appendChild(liTag1);
    }
    var button = document.createElement('button');
    button.innerHTML = "Назад";
    button.className = "back";
    //goes back to the table layout when clicked
    button.onclick = finalize;
    mainDiv.appendChild(button);
}

function returnToQuestion() {
    console.log(this);
    var questionTitle = this.getElementsByClassName("questionCol")[0].innerHTML;
    var questionNum = questionTitle[questionTitle.length - 1];


    cleanUpTheLayout();
    createQuestionLayout();
    currentQuestion = quizQuestions[questionNum - 1];
    // change currentIndex in orderto correctly display
    // it on the new layout
   // currentIndex = questionNum - 1;
    showCurrentQuestion(false);
   // var correctLiNum = quizQuestions[questionNum - 1].answer;
    
    if (quizQuestions[questionNum - 1].enabled) {
        var index = find(quizQuestions[questionNum - 1], quizQuestions[questionNum - 1].answer);
        if (quizQuestions[questionNum - 1].replied) {
            document.getElementsByTagName("li")[index+4].className = "correct";
        } else {
            var selectedLiNum = quizQuestions[questionNum - 1].selectionOfUser;
            document.getElementsByTagName("li")[selectedLiNum + 4].className = "wrong";
            document.getElementsByTagName("li")[index+4].className = "correct";

        }
    }
}
/*
the number of action taken when any of the ul items is clicked on:
getting the number of question and show the current question
*/


