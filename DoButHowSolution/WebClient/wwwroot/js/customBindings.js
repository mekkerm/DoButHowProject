﻿ko.bindingHandlers.quillControl = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var quillId = "quill_" + window.guidGenerator();
        $(element).hide(); // hide the textarea
        var inputValue = $(element).val();
        $(element).parent().append("<div id='" + quillId + "'  class='form-control'>" + inputValue + "</div>")

        var isDisabled = $(element).attr("disabled") === "disabled";
        var toolbar = isDisabled ? false : [
            ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
            ['blockquote', 'code-block'],

            [{ 'header': 1 }, { 'header': 2 }],               // custom button values
            [{ 'list': 'ordered' }, { 'list': 'bullet' }],
            [{ 'script': 'sub' }, { 'script': 'super' }],      // superscript/subscript
            [{ 'indent': '-1' }, { 'indent': '+1' }],          // outdent/indent
            //[{ 'direction': 'rtl' }],                         // text direction

            [{ 'size': ['small', false, 'large'/*, 'huge'*/] }],  // custom dropdown
            //[{ 'header': [1, 2, 3, false] }],

            [{ 'color': [] }/*, { 'background': [] }*/],          // dropdown with defaults from theme
            [{ 'font': [] }],
            [{ 'align': [] }],

            ['clean']
        ]

        var quill = new Quill("#" + quillId, {
            readOnly: isDisabled,
            modules: {
                toolbar: toolbar
            },
            theme: 'snow'
        });



        var correctButton = $("#pushButton");
        correctButton.click(function (event) {
            
            var value = $("#" + quillId).find(".ql-editor")./*find("p").*/html();
            $(element).val(value);
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        // This will be called once when the binding is first applied to an element,
        // and again whenever any observables/computeds that are accessed change
        // Update the DOM element based on the supplied values here.
    }
};

ko.bindingHandlers.moveFromQuillControl = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        // This will be called once when the binding is first applied to an element,
        // and again whenever any observables/computeds that are accessed change
        // Update the DOM element based on the supplied values here.
    }
};

window.guidGenerator = function() {
    var S4 = function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    };
    return (S4() + S4());//+ "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}