angular.module('IocApp')
.controller('registerCtrl', function ($scope, $state, registerService, cultureService, $cacheFactory, userAuthenticated) {

    $scope.actions = {};
    $scope.user = {};
    $scope.validationErrors = [];

    $scope.init = function () {
        // Load html labels from the resources file depending on the current culture
        cultureService.getResource('NgApp/Account/Resources/registerResources', function (result) {
            $scope.labels = result;
        });
    }

    // Register user
    // notify UserAuthenticated service if the user is registered successfully, navigate to the main page
    // if failed load validation errors object to show up on the html
    $scope.actions.register = function (thisForm) {

        $scope.registerSubmit = true;
        var form = thisForm;
        if (!form.$valid) return;
        if (userAuthenticated.get()) return;
        registerService.register($scope.user.fullName, $scope.user.email, $scope.user.password, $scope.user.repeatPassword,
            function (response) {
                if (response && response.data && response.data.IsSuccess) {
                    userAuthenticated.setUserName($scope.user.email);
                    $state.go('main.welcome')
                }
                else if (response && response.data && response.data.IsSuccess === false && response.data.ValidationErrors && response.data.ValidationErrors.length > 0) {
                    $scope.validationErrors = response.data.ValidationErrors;
                }
            },
            function (error) {

            });
    }

    $scope.actions.inputChanged = function(control){
        if ($scope.validationErrors) {
            
            angular.forEach($scope.validationErrors, function(e){
                if (e.FieldName === control)
                {
                    $scope.validationErrors.splice($scope.validationErrors.indexOf(e), 1);
                }
            })
        }
    }
});