angular.module('IocApp')
.directive('ngMatch', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var otherInput = elem.inheritedData("$formController")[attrs.ngMatch];

            ctrl.$parsers.push(function (value) {
                if (value === otherInput.$viewValue) {
                    ctrl.$setValidity("match", true);
                    return value;
                }
                ctrl.$setValidity("match", false);
            });

            otherInput.$parsers.push(function (value) {
                ctrl.$setValidity("match", value === ctrl.$viewValue);
                return value;
            });
        }
    }
}]);