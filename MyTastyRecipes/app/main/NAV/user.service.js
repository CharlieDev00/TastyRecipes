(function () {
    'use strict';

    angular
        .module('publicApp')
        .factory('userService', UserService);

    UserService.$inject = ['$http', '$q'];

    function UserService($http, $q) {
        return {
            newUser: _newUser,
            currentUser: _currentUser
        };

        function _newUser(data) {
            return $http.post('/api/user/create', data).then(success).catch(error);
        }

        function _currentUser(email) {
            return $http.post('/api/user/login', email).then(success).catch(error);
        }

        function success(resp) {
            return resp;
        }

        function error(err) {
            return $q.reject(err);
        }
    }


})();