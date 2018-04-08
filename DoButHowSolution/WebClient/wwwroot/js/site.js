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
    window.esClient = $.es.Client({
        hosts: "http://127.0.0.1:9200/"
    });

    window.esClient.ping({
        requestTimeout: 30000,
    }, function (error) {
        if (error) {
            console.error('elasticsearch cluster is down!');
        } else {
            console.log('All is well');
        }
    });

    $("#searchInput").keydown(function (x) {
        if (x.key === "Tab") {
            x.preventDefault();
            x.stopPropagation();
        }
    });
    $("#searchInput").autocomplete({
        appendTo: "#options-menu",
        autoFocus: true,
        focus: function (e) {
            //console.log(e);
        },
        select: function (event, ui) {
            //debugger;
        },
        change: function (event, ui) {
            //debugger;
        },
        source: function (request, response) {
            var text = request.term;
            var parts = text.split(" ");
            
            var searchTerm = parts[parts.length - 1];
            parts.pop();
            var initialText = parts.join(" ");
            
            window.esClient.search({
                index: 'questions',
                type: '_doc',
                body: {
                    "suggest": {
                        "word-suggest": {
                            "text": searchTerm,
                            "completion": {
                                "field": "suggest",
                                "fuzzy": true
                            }
                        }
                    }
                }
            }).then(function (resp) { 
                //console.log(resp.suggest["word-suggest"][0].options);
                var texts = [];
                var duplicates = {};
                $.each(resp.suggest["word-suggest"][0].options, function (index, item) {
                    if (!duplicates[item.text]) {
                        texts.push(initialText + " " + item.text);
                        duplicates[item.text] = {};
                    }
                });
                
                response(texts);
            }, function (err) {
                console.trace(err.message);
            });
        },
        minLength: 2
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









