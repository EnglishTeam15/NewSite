var totalQuestions = $('.question').length;

$('.question').on('click','li',function(){
  var id = $(this).parents('div').attr('id'),
      answer = $(this).parent('ul').siblings('.answer'),
      correctAnswer = 0;
  
  if($(this).parents('#' + id).hasClass('answered')){
    return false;
  } else {
    if($(this).data('correct') == '1'){
      $('<p>' + $(this).data('answer') +'</p>').prependTo(answer);
      $(this).addClass('correct');
      correctAnswer++;
    } else {
      $('<p>' + $(this).data('answer') +'</p>').prependTo(answer);
      $(this).addClass('incorrect').siblings('[data-correct="1"]').addClass('correct').siblings('[data-correct="0"]').addClass('incorrect');
    }
    $(this).parent().next('.answer').show().parent('div').addClass('answered');
  }
  
});