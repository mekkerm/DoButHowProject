window.viewModels = window.viewModels || [];


var init = function () {
    this.skip = 0;
    this.take = 15;

    this.answers = ko.observableArray([]);
    var that = this;

    this.onDownloadSucceeded = function (results) {
        if (results.length > 0) {
            var answers = jQuery.parseJSON(ko.toJSON(results));
            ko.utils.arrayPushAll(that.answers, answers);
            that.increaseSkip();
        }
    };

    this.onDownloadFail = function () {
        alert("GetInitialAnswers failed");
    }

    this.LoadMore = function (callback) {
        window.dataServices.GetInitialAnswers(this.skip, this.take).done(this.onDownloadSucceeded).fail(this.onDownloadFail);
        if ($.isFunction(callback)){
            callback();
        };
    }
    

    this.LoadMore();
}

init.prototype.increaseSkip = function () {
    this.skip += 15;
}




window.viewModels.push({
    name: "answerViewModel",
    containerId: "answerContainer",
    init: init
});
