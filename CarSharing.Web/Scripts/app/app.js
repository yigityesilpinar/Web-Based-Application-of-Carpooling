(function ($) {
    require(["utils", "webRequest"], function (utils, webRequest) {
        app = $.sammy('#mainContent', function () {
            this.get('#/', function (context) {
                utils.changeView("Membership/Login", null);
            });
            this.get('#/Register', function (context) {
                utils.changeView("Membership/Register", null);
            });

            this.get('#/Home', function (context) {
                utils.changeView("Home", null);
            });
            this.get('#/Home/Add', function (context) {
                utils.changeView("Trip/Add", null);
            });
            this.get('#/Home/Search', function (context) {
                utils.changeView("Trip/Search", null);
            });
            // route'lar buraya eklenecek
        });
        $(function () {
            app.run('#/');
        });
    });
})(jQuery);