define(["utils"], function (utils) {
    return {
        request: function (url, params, method, async, contentType, successCallback, failureCallback, errorCallback) {
            $.ajax({
                type: method,
                url: url,
                data: params,
                async: async,
                dataType: "json",
                contentType: contentType,
                cache: false,
                beforeSend: function (request) {
                },
                success: function (result, status, xhr) {
                    if (result !== undefined && result !== null) {
                        if (result.error) {
                            utils.showServerErrors(result);
                            if (!!failureCallback) {
                                failureCallback();
                            }
                            return;
                        }
                        else if (!!successCallback) {
                            successCallback(result);
                        }
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (!!errorCallback) {
                        errorCallback(xhr, ajaxOptions, thrownError);
                    }
                    else {
                        utils.showError("UnexpectedError");
                    }
                }
            });
        },
        post: function (url, params, successCallback, failureCallback, errorCallback) {
            return this.request(url, params, "POST", true, "application/json", successCallback, failureCallback, errorCallback);
        },
        postSync: function (url, params, successCallback, failureCallback, errorCallback) {
            return this.request(url, params, "POST", false, "application/json", successCallback, failureCallback, errorCallback);
        },
        get: function (url, params, successCallback, failureCallback, errorCallback) {
            return this.request(url, params, "GET", true, "application/json", successCallback, failureCallback, errorCallback);
        },
        getSync: function (url, params, successCallback, failureCallback, errorCallback) {
            return this.request(url, params, "GET", false, "application/json", successCallback, failureCallback, errorCallback);
        }
    }
});