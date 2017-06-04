(function (self) {

    self.GetInitialAnswers = function (skip, take) {
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

})(window.dataServices = window.dataServices || {});