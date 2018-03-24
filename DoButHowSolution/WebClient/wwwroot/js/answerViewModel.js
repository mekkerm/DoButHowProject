window.viewModels = window.viewModels || [];


var answerVM = function () {
    
    this.skip = this.initialSkip;
    this.take = this.initialTake;

    this.anwseredQuestions = ko.observableArray([]);
    var that = this;

    this.onDownloadSucceeded = function (results) {
        if (results.length > 0) {
            var answers = jQuery.parseJSON(ko.toJSON(results));
            ko.utils.arrayPushAll(that.anwseredQuestions, answers);
            that.adjustParameters();
        }
    };

    this.onDownloadFail = function (deff) {
        if (deff.readyState > 0) {
            alert("GetAnsweredQuestions failed");
        }
    }
    var deffered = null;

    this.LoadMore = function (callback) {
        if (deffered && deffered.readyState != 4) {
            deffered.abort();
        }
         
        deffered = window.dataServices.GetAnwseredQuestions(this.skip, this.take).done(this.onDownloadSucceeded).fail(this.onDownloadFail);
        if ($.isFunction(callback)){
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
