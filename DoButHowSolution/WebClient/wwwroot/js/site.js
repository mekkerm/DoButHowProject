// Write your Javascript code.

if (window.viewModels) {
    $.each(window.viewModels, function (index, item) {
        window[item.name] = new item.init();
        ko.applyBindings(window[item.name], $("#" + item.containerId)[0]);
    });
}


$(document).ready(function () {
    var win = $(window);
    
    // Each time the user scrolls
    win.scroll(function () {
        // End of the document reached?
        if ($(document).height() - win.height() === win.scrollTop()) {
            setTimeout(() => { $('#loading').show(); }, 0);
            if (window.answerViewModel) {
                window.answerViewModel.LoadMore(function () {
                    setTimeout(() => { $('#loading').hide(); }, 1000);
                });
            }
            if (window.questionsViewModel) {
                window.questionsViewModel.LoadMore(function () {
                    setTimeout(() => { $('#loading').hide(); }, 1000);
                });
            }
        }
    });
});


// Could be stored in a separate utility library
ko.bindingHandlers.fadeVisible = {
    init: function (element, valueAccessor) {
        // Initially set the element to be instantly visible/hidden depending on the value
        var value = valueAccessor();
        //$(element).toggle(ko.utils.unwrapObservable(value)); // Use "unwrapObservable" so we can handle values that may or may not be observable
    },
    update: function (element, valueAccessor) {
        // Whenever the value subsequently changes, slowly fade the element in or out
        var value = valueAccessor();
        $(element).fadeIn(1500);
    }
};









