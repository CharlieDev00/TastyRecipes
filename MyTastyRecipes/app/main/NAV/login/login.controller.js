(function () {
    'use strict';

    angular
        .module('publicApp')
        .controller('loginController', LoginController);

    LoginController.$inject = ['$scope', 'userService', '$cookies', '$location', '$rootScope'];

    function LoginController($scope, UserService, $cookies, $location, $rootScope) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.$cookies = $cookies;
        vm.$location = $location;
        vm.$rootScope = $rootScope;
        vm.userService = UserService;
        vm.login = _login;
        vm.getUserSuccess = _getUserSuccess;
        vm.getUserError = _getUserError;
        vm.userInput = {};

        function _onInit() {
            console.log('Login_Controller');
        }

        function _login() {
            vm.userService.currentUser(vm.userInput)
                .then(vm.getUserSuccess)
                .catch(vm.getUserError);
        }

        function _getUserSuccess(res) {
            vm.$cookies.put('User', 'User is logged in!');
            vm.$location.path('/home');
            vm.$rootScope.$broadcast('LoginSuccess');
            console.log(res);
        }

        function _getUserError(err) {
            console.log(err);
        }
    }


})();