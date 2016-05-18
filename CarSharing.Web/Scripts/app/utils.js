define(function () {
    return {
        showInfo: function(message) {
            toastr.info(message);
        },
        showServerErrors: function(result) {
            for (var i = 0; i < result.messages.length; i++) {
                var localizedMessage = this.getLocaleResourceValue(result.messages[i].message);
                if(!!localizedMessage){
                    this.showError(localizedMessage);
                }
                else{
                    this.showError(result.messages[i].message);
                }
            }
        },

        showError: function(message) {
            toastr.error(message);
        },

        showWarning: function(message) {
            toastr.warning(message);
        },

        showSuccess: function(message) {
            toastr.success(message);
        },

        showWindow: function(windowId) {
            $(windowId).modal('show');
        },

        closeWindow: function(windowId) {
            $(windowId).modal('hide');
        },

        loadTemplate: function(template, container, callback) {
            var templatePath = "/Main/Template?name=" + template;
            $.ajax({
                type: "GET",
                url: templatePath,
                dataType: "html",
                cache: false,
                success: function (htmlContent) {
                    $(container).fadeOut('slow', 'linear', function () {
                        $(container).html(htmlContent);
                        if (!!callback) {
                            callback();
                        }
                        $(container).fadeIn('slow', 'linear');
                    });
                }
            });
        },
        changeView: function(view, callback) {   

            activeView = view;

            var viewPath = "/Main/Template?name=" + view;
            $.ajax({
                type: "GET",
                url: viewPath,
                dataType: "html",
                cache: false,
                success: function (htmlContent) {
                    $('#mainContent').fadeOut('slow', 'linear', function () {
                        $("#mainContent").html(htmlContent);
                        if (!!callback) {
                            callback();
                        }
                        $('#mainContent').fadeIn('slow', 'linear');
                    });
                }
            });
        },
        GUID: function() {
            var S4 = function () {
                return Math.floor(
			        Math.random() * 0x10000 /* 65536 */
		        ).toString(16);
            };

            return (
		        S4() + S4() + "-" +
		        S4() + "-" +
		        S4() + "-" +
		        S4() + "-" +
		        S4() + S4() + S4());
        }
    }
});