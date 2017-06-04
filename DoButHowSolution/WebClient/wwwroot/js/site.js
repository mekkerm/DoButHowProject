// Write your Javascript code.

if (window.viewModels) {
    $.each(window.viewModels, function (index, item) {
        window[item.name] = new item.init();
        ko.applyBindings(window[item.name], $("#" + item.containerId)[0]);
    });
}


$(document).ready(function () {
    var win = $(window);
    $('#loading').hide();
    // Each time the user scrolls
    win.scroll(function () {
        // End of the document reached?
        if ($(document).height() - win.height() === win.scrollTop()) {
            $('#loading').show();

            window.answerViewModel.LoadMore(function () {
                $('#loading').hide();
            });
        }
    });
});









