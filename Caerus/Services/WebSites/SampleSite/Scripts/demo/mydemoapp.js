var mainApp = angular.module('mainApp', ['ngRoute', 'ngResource', 'ui.bootstrap']);     //Define the main module
mainApp.controller('baseController', ['$scope', '$rootScope', '$location', function ($scope, $rootScope, $location) {

    var self = $scope;
    self.context = new context($scope);
    //#region Members
    $scope.Message = "";
    $scope.ReplyStatus = 0;

    $scope.ClearMessage = function () {
        $scope.ReplyStatus = 0;
    };

    $scope.GoBack = function () {
        window.history.back();
    }
    $scope.safeApply = function (fn) {
        if (!this.$root) {
            this.$apply(fn);
            return;
        }
        var phase = this.$root.$$phase;
        if (phase == '$apply' || phase == '$digest') {
            if (fn && (typeof (fn) === 'function')) {
                fn();
            }
        } else {
            this.$apply(fn);
        }
    };

    self.ApplyScope = function () {
        $scope.safeApply();
    }


    self.GetURLParameter = function (name) {
        return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [, ""])[1].replace(/\+/g, '%20')) || null
    }

    //#endregion


    self.ExecAjax = function (customcall) {
        $rootScope.CriticalFail = false;
        //set default objects
        if (customcall.before == undefined) {
            customcall.before = function (xhr, settings) {
                $scope.ClearMessage();
                $rootScope.IsLoading = true;
                self.ApplyScope();
            };
        }
        if (customcall.complete == undefined) {
            customcall.complete = function () {
                $rootScope.IsLoading = false;
                self.ApplyScope();
            };
        }
        if (customcall.error == undefined) {
            customcall.error = function (xhr, textStatus, errorContent) {
                $scope.ClearMessage();
                if (xhr && xhr.status && xhr.status >= 500) {
                    //self.context.DisplayError("Fiddle sticks... A server error has occurred");
                    self.ApplyScope();
                    return;
                }
                if (xhr && xhr.status && xhr.status == 404) {
                    //self.context.DisplayError("Fiddle sticks... Your request could not be found");
                    self.ApplyScope();
                    return;
                }

                //self.context.DisplayError("Fiddle sticks... A unknown error has occurred");
                self.ApplyScope();
                return;
            };
        }
        if (customcall.customerror == undefined) {
            customcall.customerror = function (result) {
                if (result.hasOwnProperty('ReplyStatus')) {
                    $scope.ReplyStatus = result.ReplyStatus;
                }
                if (result.hasOwnProperty('ReplyStatus')) {
                    if (result.ReplyStatus > 0) {
                        self.context.DisplayError("Fiddle sticks... " + result.ReplyMessage);
                    }
                }
                self.ApplyScope();
            };
        }
        var call = new self.context.MappedCall(customcall);
        call.callback = arguments.callee.caller;
        self.context.ExecCustomCall(call);
    };

    self.MapObject = function (source, destination) {
        for (var i = 0; i < Object.getOwnPropertyNames(destination).length; i++) {
            if (source != undefined)
                for (var j = 0; j < Object.getOwnPropertyNames(source).length; j++) {
                    if (Object.getOwnPropertyNames(destination)[i] == Object.getOwnPropertyNames(source)[j]) {
                        var func = source[Object.getOwnPropertyNames(destination)[i]];
                        if (func != undefined && func != null) {
                            destination[Object.getOwnPropertyNames(destination)[i]] = source[Object.getOwnPropertyNames(source)[j]];
                            continue;
                        }
                    }
                }
        }
    }

    self.DisplayError = function (title, message) {
        self.context.DisplayError(title, message);
    },
    self.DisplayMessage = function (title, message) {
        self.context.DisplayMessage(title, message);
    },
    self.DisplaySuccess = function (title, message) {
        self.context.DisplaySuccess(title, message);
    },
    self.DisplayWarning = function (title, message) {
        self.context.DisplayWarning(title, message);
    }

    self.DisplayConfirmationDialog = function (title, message, closefunction) {
        self.context.DisplayConfirmationDialog(title, message, closefunction);
    };
    self.DisplayErrorDialog = function (title, message, okfunction, buttonText) {
        self.context.DisplayErrorDialog(title, message, okfunction, buttonText);
    };
    self.DisplayOkCancelConfirmationDialog = function (title, message, okfunction, cancelfunction, oktext, canceltext) {
        self.context.DisplayOkCancelConfirmationDialog(title, message, okfunction, cancelfunction, oktext, canceltext);
    };


    //#endregion 

}]);

mainApp.controller('mainController', ['$scope', '$rootScope', '$location', '$controller', function ($scope, $rootScope, $location, $controller) {
    angular.extend(this, $controller('baseController', { $scope: $scope, $rootScope: $rootScope, $location: $location }));

    $scope.DataModel = {
        Fields: []
    }

    $scope.GetFields = function () {
        try {
            $scope.ExecAjax({
                url: "/api/Fields/GetFields?clientRefId="+ 3,
                success: function (result) {
                    $scope.DataModel = result;
                }
            });
        } catch (e) {
            $rootScope.IsLoading = false;
            $scope.DisplayError(e);
        }
    }


    $scope.SaveFields = function () {
        try {
            $scope.ExecAjax({
                url: "/api/Fields/SaveFields",
                type: "POST",
                data: angular.toJson($scope.DataModel),
                success: function () {
                    $scope.DisplaySuccess("Yippee");
                }
            });
        } catch (e) {
            $rootScope.IsLoading = false;
            $scope.DisplayError(e);
        }
    }
    $scope.GetFields();
}]);
mainApp.directive('dynamicctrl', ['$compile', '$location', '$http', function ($compile, $location, $http) {
    return {
        restrict: 'E',
        //replace: true,
        template: '<div class="dynamic-field" ng-include="\'dyn-field\'"></div>',
        scope: {
            field: "="
        },
        link: function (scope, element, attrs) {
            scope.validationMessage = "";
            scope.innerForm = "frm" + scope.field.FieldId;
            scope.dateOptions = {
                dateDisabled: false,
                formatYear: 'yy',
                maxDate: new Date(2020, 5, 22),
                minDate: new Date(),
                startingDay: 1
            };
            scope.dateFormat = 'dd-MMMM-yyyy';
            scope.getRemoteLookupValue = function (val) {
                return $http.get('//maps.googleapis.com/maps/api/geocode/json', {
                    params: {
                        address: val,
                        sensor: false
                    }
                }).then(function (response) {
                    return response.data.results.map(function (item) {
                        return item.formatted_address;
                    });
                });
            }

            scope.open = function () {
                scope.popup.opened = true;
            };

            scope.popup = {
                opened: false
            };

        },
        controller: function ($scope) {
        }
    };
}]);


mainApp.directive('dynvalid', ['$parse', function dynvalid($parse) {
    return {
        require: '?ngModel',
        restrict: 'A',
        scope: false,
        transclude: false,
        link: function (scope, elem, attrs, ctrl) {
            if (!ctrl) {
                if (console && console.warn) {
                    console.warn('Email match validation requires ngModel to be on the element');
                }
                return;
            }

            function _calculateAge(birthday) { // birthday is a date
                var ageDifMs = Date.now() - birthday.getTime();
                var ageDate = new Date(ageDifMs); // miliseconds from epoch
                return Math.abs(ageDate.getUTCFullYear() - 1970);
            }

            function _monthDiff(d1, d2) {
                var months;
                months = (d2.getFullYear() - d1.getFullYear()) * 12;
                months -= d1.getMonth() + 1;
                months += d2.getMonth();
                return months <= 0 ? 0 : months;
            }

            ctrl.$validators.dynvalid = function (modelValue) {
                for (var i = 0; i < scope.field.FieldValidations.length; i++) {
                    var val = scope.field.FieldValidations[i];
                    switch (val.ValidationType) {
                        case 1:
                            {
                                if (modelValue === "" || modelValue === null || modelValue === undefined) {
                                    scope.validationMessage = val.ValidationMessage;
                                    return false;
                                }
                                break;

                            }
                        case 2:
                            {
                                if (modelValue)
                                    if (modelValue.length < parseInt(val.ValidationValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 3:
                            {
                                if (modelValue)
                                    if (modelValue.length > parseInt(val.ValidationValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 4:
                            {
                                if (modelValue)
                                    if (!new RegExp(val.ValidationValue).test(modelValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 5:
                            {

                                if (modelValue)
                                    if (parseFloat(modelValue) < parseFloat(val.ValidationValue) || isNaN(modelValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 6:
                            {
                                if (modelValue)
                                    if (parseFloat(modelValue) > parseFloat(val.ValidationValue) || isNaN(modelValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 7:
                            {
                                if (modelValue) {
                                    var range = val.ValidationValue.replace("[", "").replace("]", "").split(",");
                                    if (isNaN(modelValue) || parseFloat(modelValue) < parseFloat(range[0]) || parseFloat(modelValue) > parseFloat(range[1])) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                }
                                break;
                            }
                        case 8:
                            {
                                if (modelValue)
                                    if (!/[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}/.test(modelValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 9:
                            {
                                if (modelValue)
                                    if (!/^(http(?:s)?\:\/\/[a-zA-Z0-9\-]+(?:\.[a-zA-Z0-9\-]+)*\.[a-zA-Z]{2,6}(?:\/?|(?:\/[\w\-]+)*)(?:\/?|\/\w+\.[a-zA-Z]{2,4}(?:\?[\w]+\=[\w\-]+)?)?(?:\&[\w]+\=[\w\-]+)*)$/.test(modelValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 10:
                            {
                                if (modelValue)
                                    if (isNaN(Date.parse(modelValue))) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 11:
                            {
                                if (modelValue)
                                    if (isNaN(modelValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 12:
                            {
                                if (modelValue)
                                    if (!/^[0-9]+$/.test(modelValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 13:
                            {
                                if (modelValue)
                                    if (!/((6011)|(4\d{3})|(5[1-5]\d{2}))[ -]?(\d{4}[ -]?){3}/.test(modelValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 14:
                            {
                                if (modelValue)
                                    if (modelValue !== val.ValidationValue) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 15:
                            {
                                if (modelValue)
                                    if (!/(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))/.test(modelValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 16:
                            {
                                if (modelValue)
                                    if (modelValue.length !== parseInt(val.ValidationValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 17:
                            {
                                if (modelValue)
                                    if (isNaN(Date.parse(modelValue)) || _calculateAge(new Date(modelValue)) < parseInt(val.ValidationValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 18:
                            {
                                if (modelValue)
                                    if (isNaN(Date.parse(modelValue)) || _calculateAge(new Date(modelValue)) > parseInt(val.ValidationValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 19:
                            {
                                if (modelValue)
                                    if (isNaN(Date.parse(modelValue)) || _monthDiff(new Date(modelValue), new Date()) < parseInt(val.ValidationValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 20:
                            {
                                if (modelValue)
                                    if (isNaN(Date.parse(modelValue)) || _monthDiff(new Date(modelValue), new Date()) > parseInt(val.ValidationValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                    }
                }
                return true;
            };
        }
    };
}]);
