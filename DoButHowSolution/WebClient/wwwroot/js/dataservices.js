(function (self) {

    self.GetAnwseredQuestions = function (skip, take) {
        return $.ajax({
            url: utils.baseUrl() + '/' + 'Home/GetInitialAnswers',
            data: {
                take: take,
                skip: skip
            },
            dataType: 'json',
            type: 'GET',
            contentType: 'application/json; charset=utf-8'
        });
    };

    self.GetQuestionWithAnswers = function (questionId) {
        return $.ajax({
            url: utils.baseUrl() + '/' + 'AnswerFor/GetQuestionWithAnswers',
            data: { questionId: questionId },
            dataType: 'json',
            type: 'GET',
            contentType: 'application/json; charset=utf-8'
        });
    };

    self.RateAnswer = function (answerId, rate) {
        return $.ajax({
            beforeSend: function (request) {
                //request.setRequestHeader("Authority", "blabla");
                //request.setRequestHeader("X-NToastNotify-Request-Type", "afsgtdhjasd");
                //request.
                //request.setRequestHeader("X-Requested-With", "XMLHttpRequest");
            },
            url: utils.baseUrl() + '/' + 'AnswerFor/RateAnswer',
            data: {
                answerId: answerId,
                rate: rate
            },
            dataType: 'json',
            type: 'GET',
            contentType: 'application/json; charset=utf-8'
        });
    };


})(window.dataServices = window.dataServices || {});