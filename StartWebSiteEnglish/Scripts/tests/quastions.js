//Массив вопросов и ответа  var res="24212332423133343132431 2421141";
var data_array = [
    ["My name’s Tamara. I __________ twenty-one years old", "is", "am", "have", "got", 2],
    ["“Is Igor a teacher?” “ No, __________ .”", "he not", "it isn't", "he doesn't", "he isn't", 4],
    ["Найтите правильный гагол: to play, to smile, to laugh, to see.", "to play", "to smile", "to laugh", "to see", 4],
    ["Найдите ошибку в трех формах глагола:", "teach - taught - taught", "catch - caugth - caught", "bring - braught - braught", "seek - sought - sought", 3],
    ["Karina never minds ....... the movie again.", "to watch", "to be watched", "watch", "watching", 4],
    ["I couldn'd help ........... .", "for laughing", "and laughed", "laughing", "to laughed", 3],
    ["Я знаю его четыре года.", "I know him four years.", "I have been him for four years.", "I know him for four years", "I have known him for four years", 4],
    ["Выберите наиболее подходящий ответ! “What is she doing?”", "She is playing with the bunny.", "She is a manager.", "She cleans the house every day.", "She is clean the carpet.", 1],
    ["This particular college has a very selective ................. policy", "acceptance", "entrance", "admissions", "admittance", 3],
    ["An obstetrician/gynecologist at the pre-conception clinic suggests we ................. some further tests.", "doing", "to do", "are doing", "should do", 4],
    //["We try to be ................. to the needs of the customer.", "responsible", "responsive", "respondent", "response", 2],
    //[" I am looking for an ................. method of heating.", "economics", "economy", "economic", "economical", 4],
    //["Igor and Tamara are __________ people.", "teacher", "nice", "long", "intelligents", 2],
    //["My brother  __________ like to go to school.", "doesn't", "don't", "not", "hasn't", 1],
    //["My friend and me __________.", "like rock both", "both like rock", "both rock like ", "like both rock", 2],
    //["I __________ the Beatles last year, but now I do.", "not like", "don't like", "didn’t like", "didn't liked", 3],
    //["__________ milk in the bottle.", "it is any", "there is any", "there is some", "there are some", 3],
    //[" My mother  __________ me to buy some bread from the shop.", "said", "told", "say to", "told to  ", 2],
    //["Have you __________ been to Paris?", "often", "never", "usually", "ever", 4],
    //["Tamara didn't meet  __________ friends in the street.", "none", "any", "someone", "a", 2],
    //[" “__________ you speak English Igor?” “Yes, a little.”", "have", "has", "can", "are", 3],
    //["“We can see an English film __________ Thursday night.”", "on", "in", "at", "of", 1],
    //["“Oh, I __________ this Thursday.”", "am work", "work", "am working", "be working", 3],
    //["“5th Element __________ to be shown at the weekend.”", "have", "can", "is going", "will", 3],
    //["Oh I __________ 5th Element. It’s a good film.", "already seen", "have saw", "have already seen", "seen already", 3],
    //["Yes, it is but Titanic is __________.", "badly", "best", "well", "better", 4],
    //["We __________ go to the cinema a lot but we don’t go very often now.", "use", "did used", "used to", "used", 3],
    //["We __________ too much since Christmas.", "are working ", "'ve been working", "'ve been work", "seen alreadywill be working ", 2],
    //["I have exams in June so I __________ study every day.", "am", "should to", "have to", "must to", 3],
    //["If you __________ the exams, we’ll take a long holiday.", "will pass", "pass", "passing", "to pass", 2],
    //["Igor __________ in a bank when he met Tamara.", "has worked", "works", "is working", "was working", 4],
    //["There was a robbery at the bank and all the money __________.", "stolen", "was stealing", "was stolen", "was being stolen", 3],
    //[" Ivan had to do more work. Vika thinks he __________ take it easy and relax.", "should", "has to", "might to", "must", 1],
    //[" If Igor __________ time, he would go out more often.", "would have more", "had more", "will have more", "would had ", 2],
    //["As soon as Ivan has more money, __________ a car.", "he buy", "he had bought ", "he’ll buying", "he’ll buy", 4],
    //["The Titanic__________ 11 Oscars.", "is given", "was given", "was gave", "gave  ", 2],
    //["Tamara said she  __________ Coca Cola.", "didn’t like", "don’t like", "might like", "won't like", 1],
    //["By next year Igor __________ from the University.", "will have graduated", "graduates", "graduated ", "will be graduating  ", 1],
    //["Yesterday Tamara said that she __________ in three months.", "will marrying", "has married", "marrying", "would marry", 4],
    //["It's cold today,  __________?.", "isn't it", "haasn't it", "it is", "has it", 1],
];
var plus = 0;
var cur_answer = 0;
var count_answer = data_array.length;

function check(num) {

    if (num == 0) {

        document.getElementById('text_test').style.display = 'none';
        document.getElementById('option1').style.display = 'block';
        document.getElementById('option2').style.display = 'block';
        document.getElementById('option3').style.display = 'block';
        document.getElementById('option4').style.display = 'block';
        document.getElementById('option5').style.display = 'block';
        document.getElementById('question').style.display = 'block';

        document.getElementById('option1').innerHTML = data_array[cur_answer][1];
        document.getElementById('option2').innerHTML = data_array[cur_answer][2];
        document.getElementById('option3').innerHTML = data_array[cur_answer][3];
        document.getElementById('option4').innerHTML = data_array[cur_answer][4];
        document.getElementById('option5').innerHTML = 'Не знаю';
        document.getElementById('question').innerHTML = data_array[cur_answer][0];

        document.getElementById('start').style.display = 'none';
        document.getElementById('end').style.display = 'inline';

        var intervalID = setInterval(sec, 1000);

    } else {

        if (num == data_array[cur_answer][5]) {
            plus++;
        }
        cur_answer++;
        if (cur_answer < count_answer) {
            document.getElementById('option1').innerHTML = data_array[cur_answer][1];
            document.getElementById('option2').innerHTML = data_array[cur_answer][2];
            document.getElementById('option3').innerHTML = data_array[cur_answer][3];
            document.getElementById('option4').innerHTML = data_array[cur_answer][4];
            document.getElementById('option5').innerHTML = 'Не знаю';
            document.getElementById('question').innerHTML = data_array[cur_answer][0];
        }
        else {
            document.getElementById('option1').style.display = 'none';
            document.getElementById('option2').style.display = 'none';
            document.getElementById('option3').style.display = 'none';
            document.getElementById('option4').style.display = 'none';
            document.getElementById('option5').style.display = 'none';
            document.getElementById('question').style.display = 'none';
            document.getElementById('end').style.display = 'inline';

            var percent = Math.round(plus / count_answer * 100);
            var res;
            if (count_answer <= 12)
                res = "Начальный А0 (Beginer)";
            else if (nok <= 18)
                res ="Cлабый средний A1 (Elementary)";
            else if (nok <= 26)
                res ="Cредний A2 (Pre-intermediate/Intermediate)";
            else
                res ="Hе ниже среднего (Upper-intermediate)";            document.getElementById('result').innerHTML = 'Правильных ответов: ' + plus + ' из ' + count_answer + ' (' + percent + '%)<br/>Ваш уровень знания английского языка - '+res;
        }
    }
}