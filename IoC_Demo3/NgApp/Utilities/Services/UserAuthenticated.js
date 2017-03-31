angular.module('IocApp')
.factory('userAuthenticated', function () {
    var service = {}

    var isAuthenticated = false;
    var userName = "";

    service.set = function (val) {
        isAuthenticated = val;
        if (!val)
            userName = '';
    }

    service.get = function () {
        return isAuthenticated;
    }

    service.setUserName = function (username) {
        userName = username;
        if(username)
            isAuthenticated = true;
    }

    service.getUserName = function () {
        return userName;
    }

    return service;
})