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

    self.UnRateAnswer = function (answerId) {
        return $.ajax({
            url: utils.baseUrl() + '/' + 'AnswerFor/UnRateAnswer',
            data: {
                answerId: answerId
            },
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json; charset=utf-8'
        });
    };

})(window.dataServices = window.dataServices || {});