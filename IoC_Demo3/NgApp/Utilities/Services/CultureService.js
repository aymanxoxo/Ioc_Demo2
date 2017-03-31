angular.module('IocApp')
.factory('cultureService', function ($http) {
    var service = {};

    service.getResource = function (path, callback) {
        var currentCulture = $http.get('api/Utilities/GetCulture').then(function (response) {
            var fullPath = path + '.' + response.data + '.js';
            $http.get(fullPath).then(function (result) {
                if(result && result.data && result.data.length === 1)
                    callback(result.data[0]);
            });
        })
        
    }

    return service;
});