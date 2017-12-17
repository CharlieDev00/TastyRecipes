(function () {
    'use strict';

    angular
        .module('publicApp')
        .controller('registerController', RegisterController);

    RegisterController.$inject = ['$scope', 'userService'];

    function RegisterController($scope, UserService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.userService = UserService;
        vm.register = _register;
        vm.createUserSuccess = _createUserSuccess;
        vm.createUserError = _createUserError;
        vm.userInfo = {};

        function _onInit() {
            console.log('Register_Controller');
        }

        function _register() {
            console.log(vm.userInfo);
            vm.userService.newUser(vm.userInfo)
                .then(vm.createUserSuccess)
                .catch(vm.createUserError);
        }

        function _createUserSuccess(resp) {
            console.log(resp);
        }

        function _createUserError(err) {
            console.log(err);
        }
    }


})();