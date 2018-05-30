(function(){
  
  function ready(fn) {
    if (document.attachEvent ? document.readyState === "complete" : document.readyState !== "loading"){
      fn();
    } else {
      document.addEventListener('DOMContentLoaded', fn);
    }
  }
  
  function Speaker() {
    this.init()
  }
  
  Speaker.prototype = {
    
    browserSpeaks: 'speechSynthesis' in window,
    
    dom : {
      container: document.getElementById('container'),
      speakForm: document.getElementById('speak'),
      wordsInput: document.getElementById('words'),
      speakerBtn: document.getElementById('speaker'),
      spokenList: document.getElementById('spoken'),
      voicesSelect: document.getElementById('voices'),
    },
    
    options : {
      // welcome: 'Привет.',
      inputMsg: 'Что я должен сказать?',
      noSupportMsg: 'Your browser doesn\'t know how to talk, too bad'
    },
    
    speechConfig: {
      voices : [], // array of browser voices
      voice: null, // current voice
      rate: 1, // 0.1 - 10
      volume: 1, // 0 - 1
      pitch: 1, // 0 - 2
      lang: 'en-US'
    },
    
    init : function init() {
      var _this = this;
      this.dom.wordsInput.placeholder = (this.browserSpeaks) ? this.options.inputMsg : this.options.noSupportMsg;
      
      window.speechSynthesis.onvoiceschanged = function() {
        var voices = window.speechSynthesis.getVoices();
        _this.speechConfig.voices = voices;
        _this.speechConfig.voice = _this.speechConfig.voice || voices[0];
        _this.buildVoiceOptions(voices);
      };
      
      if (this.browserSpeaks) {
        speechSynthesis.speak(new SpeechSynthesisUtterance(this.options.welcome));
      }
      
      this.dom.speakForm.addEventListener("submit", function(e){
        var words = _this.dom.wordsInput.value;
        e.preventDefault();
        if (words === '') return;
        _this.addSpoken(words);
        _this.dom.wordsInput.value = '';
      });
      
      this.dom.voicesSelect.addEventListener("change", function(e){
        _this.speechConfig.voice = _this.speechConfig.voices.find(function(voice) {return voice.name === e.target.value});
      });
    },
    
    speak : function speak(words, callback) {
      var utterance = new SpeechSynthesisUtterance(words);
      utterance.voice = this.speechConfig.voice;
      speechSynthesis.speak(utterance);
      utterance.onend = function(){
        if (callback) callback();
      }
    },
    
    addSpoken : function addSpoken(words) {
      var _this = this;
      var spokenItem = document.createElement('li');
      var spokenBtn = document.createElement('button');
      var spokenWords = document.createTextNode(words);
      spokenBtn.appendChild(spokenWords);
      spokenItem.appendChild(spokenBtn);
      this.dom.spokenList.appendChild(spokenItem);

      setTimeout(function(){
        spokenItem.className='visible';
        spokenBtn.className='speaking';
        _this.dom.wordsInput.focus();
        _this.speak(words, function(){
          spokenBtn.className="";
        });
      });

      spokenBtn.addEventListener("click", function(e){
        spokenBtn.focus();
        spokenBtn.className="speaking";
        _this.speak(words, function(){
          spokenBtn.className="";
        });
      });
    },
    
    buildVoiceOptions : function buildVoiceOptions(voices) {
      var _this = this;
      _this.dom.voicesSelect.innerHTML = null;
      var voiceOptions = voices.map(function(voice) {
        var opt = document.createElement('option');
        var optName = document.createTextNode(voice.name);
        var optLang = document.createTextNode(' (' + voice.lang + ')');
        opt.value = voice.name;
        if (voice.name === _this.speechConfig.voice.name) opt.selected
        opt.appendChild(optName);
        opt.appendChild(optLang);
        return opt;
      });
      return voiceOptions.forEach(function(option) {
        _this.dom.voicesSelect.appendChild(option);
      });
    }
  }
    
  ready(function() {
    return new Speaker();
  });
  
})();  