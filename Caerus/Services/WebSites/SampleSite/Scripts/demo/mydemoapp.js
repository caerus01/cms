var mainApp = angular.module('mainApp', ['ngRoute', 'ngResource']);     //Define the main module
mainApp.controller('mainController', ['$scope', '$rootScope', '$location', function ($scope, $rootScope, $location) {
    $scope.MyValue = {
        Field: {
            FieldValue: "my name",
            OwningType: 0,
            OwningEntityType: 1,
            View: 1,
            Sequence: 1,
            FieldId: "FirstName",
            Label: "First Name...",
            ToolTip: "Please provide us with your first name",
            FieldType: 1,
            CssClass: "1",
            FieldRank: 1,
            LookupType: 0,
            SystemDataType: "",
            ReadOnly: false,
            FieldMask: "",
            Placeholder: "your name here",
            FieldValidations: [
                {
                    FieldId: "FirstName",
                    ValidationType: 1,
                    ValidationValue: "",
                    ValidationMessage: "Required"
                },
                //{
                //    FieldId: "FirstName",
                //    ValidationType: 2,
                //    ValidationValue: "5",
                //    ValidationMessage: "Min length of 5"
                //},
                //{
                //    FieldId: "FirstName",
                //    ValidationType: 3,
                //    ValidationValue: "19",
                //    ValidationMessage: "Max length of 10"
                //},
                //{
                //    FieldId: "FirstName",
                //    ValidationType: 4,
                //    ValidationValue: "[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
                //    ValidationMessage: "Regex"
                //},
                {
                    FieldId: "FirstName",
                    ValidationType: 5,
                    ValidationValue: "3",
                    ValidationMessage: "MinValue"
                },
                {
                    FieldId: "FirstName",
                    ValidationType: 6,
                    ValidationValue: "8",
                    ValidationMessage: "MaxValue"
                }
            ]
        }
    }
}]);
mainApp.directive('dynamicctrl', ['$compile', '$location', function ($compile, $location) {
    return {
        restrict: 'E',
        //replace: true,
        template: '<div class="dynamic-field" ng-include="getTemplateUrl()"></div>',
        scope: {
            field: "="
        },
        link: function (scope, element, attrs) {
            scope.validationMessage = "";
            scope.innerForm = "frm" + scope.field.FieldId;
            scope.getTemplateUrl = function () {
                var type = "text";
                switch (scope.field.FieldType) {
                    case 1:
                    default:
                        {
                            type = 'text';
                            break;
                        }
                    case 2:
                        {
                            type = 'select';
                            break;
                        }
                    case 3:
                        {
                            type = 'autocomplete';
                            break;
                        }
                    case 4:
                        {
                            type = 'datepicker';
                            break;
                        }
                    case 5:
                        {
                            type = 'checkbox';
                            break;
                        }
                }
                return 'dyn-field-' + type;
            }
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
                                    if (parseFloat(modelValue) < parseFloat(range[0]) || parseFloat(modelValue) > parseFloat(range[0]))
                                        scope.validationMessage = val.ValidationMessage;
                                    return false;
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
                                    if (isNaN(modelValue) || _calculateAge(modelValue) < parseInt(val.ValidationValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 18:
                            {
                                if (modelValue)
                                    if (isNaN(modelValue) || _calculateAge(modelValue) > parseInt(val.ValidationValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 19:
                            {
                                if (modelValue)
                                    if (isNaN(Date.parse(modelValue)) || _monthDiff(Date.parse(modelValue), new Date()) < parseInt(val.ValidationValue)) {
                                        scope.validationMessage = val.ValidationMessage;
                                        return false;
                                    }
                                break;
                            }
                        case 20:
                            {
                                if (modelValue)
                                    if (isNaN(Date.parse(modelValue)) || _monthDiff(Date.parse(modelValue), new Date()) > parseInt(val.ValidationValue)) {
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
//if (/[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}/.test(modelValue)) {

//}