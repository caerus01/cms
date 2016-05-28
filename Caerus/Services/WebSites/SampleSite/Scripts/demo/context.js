function context($scope) {
    toastr.options = {
        closeButton: true,
        debug: false,
        progressBar: true,
        positionClass: "toast-bottom-right",
        preventDuplicates: true,
        onclick: null,
        showDuration: 300,
        hideDuration: 1000,
        timeOut: 5000,
        extendedTimeOut: 1000,
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: 'fadeIn',
        hideMethod: 'fadeOut'
    }

    var handleCustomFunction = function (customHandlers, data, textStatus, xhr) {
        var result = false;
        if (customHandlers)
            if ($.isArray(customHandlers)) {
                $.each(customHandlers, function (index, value) {
                    if (value.level) {
                        if (data.ReplyStatus == value.level) {
                            if (value.handle && $.isFunction(value.handle)) {
                                value.handle(data, textStatus, xhr);
                                result = true; //function was executed... lets go back
                                return;
                            }
                        }
                    }
                });
            }
        //function was not found, we return false, and handle from caller
        return result;
    };
    var execAjax = function (urlLoc, restCallType, successFunction, errorFunction, jsonData, beforeFunction, completeFunction, criticalErrorFunction, replyObjectCollection, contentOptions, caller, skipAbort) {
        //do sync call setup
        //construct default object
        var _contentOptions = {
            callType: restCallType,
            contentType: "application/json; charset=utf8",
            timeout: 60000,
            async: true,
            dataType: "JSON"
        };
        //check if we have contentOption overrides... and apply
        if (contentOptions) {
            _contentOptions.callType = restCallType;
            if (contentOptions.contentType != null || contentOptions.contentType == 'undefined')
                _contentOptions.contentType = contentOptions.contentType;
            if (contentOptions.timeout != null || contentOptions.timeout == 'undefined')
                _contentOptions.timeout = contentOptions.timeout;
            if (contentOptions.dataType != null || contentOptions.dataType == 'undefined')
                _contentOptions.dataType = contentOptions.dataType;
            if (contentOptions.async != null || contentOptions.async == 'undefined')
                _contentOptions.async = contentOptions.async;
            if (contentOptions.processData != null || contentOptions.processData == 'undefined')
                _contentOptions.processData = contentOptions.processData;
            else _contentOptions.processData = true;
        }
        $.support.cors = false; //disabling cors for now... no need running in same domain
        /*  
         Success = 0,
         Warning = 1,
         Redirect = 2,
         Invalid = 3,
         Error = 4,
         Failure = 5
        */
        //do jquery call
        $.ajax({
            type: _contentOptions.callType,
            contentType: _contentOptions.contentType,
            timeout: _contentOptions.timeout,
            async: _contentOptions.async,
            url: urlLoc,
            dataType: _contentOptions.dataType,
            data: jsonData,
            processData: _contentOptions.processData,
            beforeSend: function (xhr, settings) {
                if (beforeFunction && $.isFunction(beforeFunction))
                    beforeFunction(xhr, settings);
            },
            success: function (data, textStatus, xhr) {
                if (data) {
                    if (data.ReplyStatus != null) //check if object returned contains property of AltasReplyStatus
                    {
                        if (data.ReplyStatus === 0) {
                            if (successFunction && $.isFunction(successFunction))
                                successFunction(data, textStatus, xhr);


                         
                            return;
                        } else {

                            if (!handleCustomFunction(replyObjectCollection, data, textStatus, xhr)) {//if function was not executed, handle as standard error
                                if (errorFunction && $.isFunction(errorFunction))
                                    errorFunction(data, textStatus, xhr);
                            }
                            return;
                        }
                    } else //another type of object was returned... handle as success
                    {
                        if (successFunction && $.isFunction(successFunction))
                            successFunction(data, textStatus, xhr);
                    }
                }
                $scope.$apply();
                return;
            },
            error: function (xhr, textStatus, errorContent) {
                if (xhr.status == 401) {
                    window.location.reload();
                    return;
                }
                if (xhr.status === 0 || xhr.status === 408) {
                    if (!skipAbort)
                        toastr.warning("Request to server timed out. Please try again");
                    {
                        if (criticalErrorFunction) {
                            criticalErrorFunction(xhr, textStatus, errorContent);
                        }
                    }
                    return;
                }


                if (criticalErrorFunction) {
                    criticalErrorFunction(xhr, textStatus, errorContent);
                }

            },
            complete: function (xhr, textstatus) {
                if (completeFunction && $.isFunction(completeFunction))
                    completeFunction(xhr, textstatus);

                $scope.$apply();
            }
        });
    };
    var mapCallObject = function (customcall) {
        var call = new callObject();
        for (var i = 0; i < Object.getOwnPropertyNames(call).length; i++) {
            if (customcall != undefined)
                for (var j = 0; j < Object.getOwnPropertyNames(customcall).length; j++) {
                    if (Object.getOwnPropertyNames(call)[i] == Object.getOwnPropertyNames(customcall)[j]) {
                        var func = customcall[Object.getOwnPropertyNames(call)[i]];
                        if (func != undefined && func != null) {
                            call[Object.getOwnPropertyNames(call)[i]] = customcall[Object.getOwnPropertyNames(customcall)[j]];
                            continue;
                        }
                    }
                }
        }
        if (call["customerror"] == undefined)
            call["customerror"] = call["error"];
        if (call.url != "" && call.url != undefined && call.url != null)
            return call;
        return undefined;
    };

    var callObject = function () {
        return {
            url: "",
            type: "GET",
            contentType: "application/json; charset=utf8",
            timeout: 180000,
            async: true,
            dataType: "JSON",
            processData: true,
            success: undefined,
            customerror: undefined,
            error: undefined,
            complete: undefined,
            before: undefined,
            data: undefined,
            replyobjectoverrides: undefined,
            contentoptions: undefined,
            callback: undefined,
            skipAbort: undefined
        };
    };

    return {
        CustomCall: function () {
            return new callObject();
        },
        MappedCall: function (customcall) {
            return mapCallObject(customcall);
        },
        ExecCustomCall: function (customcall) {
            var call = mapCallObject(customcall);
            call.contentoptions = {
                contentType: customcall.contentType,
                timeout: customcall.timeout,
                async: customcall.async,
                dataType: customcall.dataType,
                processData: customcall.processData,
                skipAbort: customcall.skipAbort
            };

            if (call != undefined)
                execAjax(call.url, call.type, call.success, call.customerror, call.data, call.before, call.complete, call.error, call.replyobjectoverride, call.contentoptions, call.callback, call.skipAbort);
        },
        ExecAjax: function (urlLoc, restCallType, successFunction, errorFunction, jsonData, beforeFunction, completeFunction, criticalErrorFunction, replyObjectCollection, contentOptions) {
            execAjax(urlLoc, restCallType, successFunction, errorFunction, jsonData, beforeFunction, completeFunction, criticalErrorFunction, replyObjectCollection, contentOptions);
        },
        DisplayConfirmationDialog: function (title, message, closefunction) {
            //confirmationModal
            $("#confirmationModal #confirmationText").html(message);
            $("#confirmationModal #confirmationTitle").html(title);

            $("#confirmationModal #confirmationOk").unbind("click");
            $("#confirmationModal #confirmationOk").bind("click", closefunction);

            $("#confirmationModal").modal();
        },
        DisplayErrorDialog: function (title, message, okfunction, buttonText) {
            //errorModal
            $("#errorModal #errorText").html(message);
            $("#errorModal #errorTitle").html(title);

            if (buttonText)
                $("#errorModal #errorOk").text(buttonText);
            $("#errorModal #errorOk").unbind("click");
            $("#errorModal #errorOk").bind("click", okfunction);

            $("#errorModal").modal();
        },
        DisplayOkCancelConfirmationDialog: function (title, message, okfunction, cancelfunction, oktext, canceltext) {
            //OkCancel
            $("#OkCancel #OkCancelText").html(message);
            $("#OkCancel #OkCancelTitle").html(title);

            if (oktext)
                $("#OkCancel #Ok").text(oktext);
            $("#OkCancel #Ok").unbind("click");
            $("#OkCancel #Ok").bind("click", okfunction);

            if (canceltext)
                $("#OkCancel #Cancel").text(canceltext);
            $("#OkCancel #Cancel").unbind("click");
            $("#OkCancel #Cancel").bind("click", cancelfunction);

            $("#OkCancel").modal();
        },
        DisplayError: function (title, message) {
            //toastr.error(title, message);
            if (message == undefined || message == null || message == "")
                toastr.error(title);
            else
                toastr.error(message);
        },
        DisplayMessage: function (title, message) {
            //toastr.info(title, message);
            if (message == undefined || message == null || message == "")
                toastr.info(title);
            else
                toastr.info(message);
        },
        DisplaySuccess: function (title, message) {
            //toastr.success(title, message);
            if (message == undefined || message == null || message == "")
                toastr.success(title);
            else
                toastr.success(messages);
        },
        DisplayWarning: function (title, message) {
            //toastr.warning(title, message);
            if (message == undefined || message == null || message == "")
                toastr.warning(title);
            else
                toastr.warning(message);
        }
    }
};
