(function (self) {
    self.baseUrl = function () {
        return window.location.protocol + "//" + window.location.host;
    }
})(window.utils = window.utils || {});