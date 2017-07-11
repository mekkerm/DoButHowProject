(function (self) {
    self.baseUrl = function () {
        return window.location.protocol + "//" + window.location.host;
    }

    self.answerFor = function () {
        return "AnswerFor/Index";
    }

    self.separator = function () {
        return '/';
    }
})(window.utils = window.utils || {});