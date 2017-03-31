angular.module('IocApp')
.factory('registerService', function ($http, encryptionService) {
    var service = {};

    service.register = function (name, email, password, repassword, success, fail) {
        $http.get('api/Auth/GetKey').then(function (response) {
            if (response && response.data) {
                var regModel = { FullName: name, Email: email, Password: password, RepeatPassword: repassword };
                var strRegModel = JSON.stringify(regModel);
                var encryptedModel = encryptionService.encrypt(response.data, strRegModel);
                $http.post('api/Auth/Register', { Credentials: encryptedModel }).then(success, fail);
            }
        })
    }

    return service;
})