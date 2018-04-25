(function (self) {

    self.GetAnwseredQuestions = function (skip, take) {
        return $.ajax({
            url: utils.baseUrl() + '/' + 'Home/GetQuestions',
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

    self.GetQuestions = function (skip, take, type) {
        return $.ajax({
            url: utils.baseUrl() + '/' + 'Questions/GetQuestions',
            data: {
                take: take,
                skip: skip,
                type: type
            },
            dataType: 'json',
            type: 'GET',
            contentType: 'application/json; charset=utf-8'
        });
    }

    self.GetAnswers = function (skip, take, type) {
        return $.ajax({
            url: utils.baseUrl() + '/' + 'Answers/GetAnswers',
            data: {
                take: take,
                skip: skip,
                type: type
            },
            dataType: 'json',
            type: 'GET',
            contentType: 'application/json; charset=utf-8'
        });
    }


})(window.dataServices = window.dataServices || {});