angular.module('IocApp')
.controller('loginCtrl', function ($scope, $state, loginService, cultureService, userAuthenticated) {
    $scope.actions = {};
    $scope.user = {};
    $scope.loginSubmitted = false;
    $scope.labels = {};

    // Event when page load
    $scope.init = function () {

        // Load the resource file to include labels into the html depending on the current culture
        cultureService.getResource('NgApp/Account/Resources/loginResources', function (result) {
            $scope.labels = result;
        });

        // check if the current user is authenticated or not, get its name if authenticated and notify others with that through the UserAuthenticated Service
        loginService.getCurrentUser(function (response) {
            if(response && response.data && response.data.IsSuccess)
            {
                userAuthenticated.setUserName(response.data.Result);
            }
        })
    }


    $scope.actions.register = function () {
        if(!userAuthenticated.get())
            $state.go('main.register');
    }

    $scope.actions.login = function () {
        $scope.loginSubmitted = true;
        loginService.Login($scope.user.userName, $scope.user.password, function (success) {
            userAuthenticated.set(true);
        }, function (error) { });
    }

    $scope.getUserAuth = function () {
        return userAuthenticated.get();
    }

    $scope.getUserName = function () {
        return userAuthenticated.getUserName();
    }

    $scope.actions.signout = function () {
        loginService.signout(function (response) {
            if(response && response.status === 200)
                userAuthenticated.set(false);
        }, function (error) { });
    }


});