(function (self) {
    self.baseUrl = function () {
        return window.location.protocol + "//" + window.location.host;
    }

    self.answerFor = function () {
        return "AnswerFor" + self.separator() +"Index";
    }

    self.question = function () {
        return "Question" + self.separator() + "Index";
    }

    self.separator = function () {
        return '/';
    }
})(window.utils = window.utils || {});