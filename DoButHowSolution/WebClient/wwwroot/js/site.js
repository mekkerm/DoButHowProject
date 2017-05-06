// Write your Javascript code.

(function (self) {
    self.openNav = function () {
        $("#mySidenav").width("250px");
        $("#bodyPart").css('margin-left', "250px");
        $("#openNavBar").css('display', "none");
        $("#closeNavBar").css('display', "inline-block");
    };

    /* Set the width of the side navigation to 0 and the left margin of the page content to 0 */
    self.closeNav = function () {
        $("#mySidenav").width("0px");
        $("#bodyPart").css('margin-left', 0);
        $("#openNavBar").css('display', "inline-block");
        $("#closeNavBar").css('display', "none");
    };
    ko.applyBindings(self);
})(window.navbar = window.navbar || {});