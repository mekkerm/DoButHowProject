window.viewModels = window.viewModels || [];


var answerVM = function () {
    Object.defineProperty(this, "type", {
        value: function () {
            var urlParts = window.location.href.split('/');
            var lastPart = urlParts[urlParts.length - 1];
            if (lastPart === "AnswersToApprove" || lastPart === "MyRejectedAnswers") {
                return lastPart;
            } else {
                return "all";
            }
        }(),
        writeable: false
    });

    this.skip = this.initialSkip;
    this.take = this.initialTake;

    this.noMoreItems = ko.observable(false);
    this.answers = ko.observableArray([]);
    var that = this;

    this.onDownloadSucceeded = function (results) {
        if (results.length > 0) {
            var answers = jQuery.parseJSON(ko.toJSON(results));
            ko.utils.arrayPushAll(that.answers, answers);
            that.adjustParameters();
        } else {
            that.noMoreItems(true);
        }
    };

    this.onDownloadFail = function (deff) {
        if (deff.readyState > 0) {
            alert("GetAnswers failed");
        }
    }
    var deffered = null;

    this.LoadMore = function (callback) {
        if (deffered && deffered.readyState !== 4) {
            deffered.abort();
        }

        deffered = window.dataServices.GetAnswers(this.skip, this.take, this.type).done(this.onDownloadSucceeded).fail(this.onDownloadFail);
        if ($.isFunction(callback)) {
            callback();
        };
    }


    this.LoadMore(() => { setTimeout(() => { $('#loading').hide(); }, 0); });
}

answerVM.prototype.initialTake = 40;
answerVM.prototype.initialSkip = 0;

answerVM.prototype.adjustParameters = function () {
    this.skip += 20;
    this.take = 20;
}

window.viewModels.push({
    name: "answerViewModel",
    containerId: "answerContainer",
    init: answerVM
});
