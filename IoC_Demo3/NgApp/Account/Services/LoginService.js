angular.module('IocApp')
.factory('loginService', function ($http, encryptionService) {
    var loginService = {};

    loginService.Login = function (username, password, success, fail) {
        $http.get('api/Auth/GetKey').then(function (response) {
            if (response && response.data) {
                var encryptedData = encryptionService.encrypt(response.data, username + '\\' + password);
                

                $http.post('/api/Auth/Login', { Credentials: encryptedData }).then(success, fail);
            }
        });
    }

    loginService.getCurrentUser = function (success, fail) {
        $http.get('api/Auth/GetCurrentUser').then(success, fail);
    }

    loginService.signout = function (success, fail) {
        $http.get('api/Auth/Signout').then(success, fail);
    }

    return loginService;
});