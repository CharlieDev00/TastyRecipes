(function () {
    "use strict";

    angular
        .module("publicApp")
        .component("btnDetails", {
            templateUrl: '/app/main/NAV/btnDetails.html',
            controller: 'btnController'
        });
})();

(function () {
    'use strict';

    angular
        .module('publicApp')
        .controller('btnController', BtnController);

    BtnController.$inject = ['$scope', '$cookies'];

    function BtnController($scope, $cookies) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.$cookies = $cookies;
        vm.onBroadcast = _onBroadcast;
        vm.logout = _logout;
        vm.show = true;
        vm.hide = true;
        

        function _onInit() {
            console.log('Btn_Controller');
            vm.$scope.$on('LoginSuccess', vm.onBroadcast);
            if (vm.$cookies.get('User')) {
                vm.show = false;
                vm.hide = false;
            }
            else {
                vm.show = true;
                vm.hide = true;
            }
        }

        function _logout() {
            vm.$cookies.remove('User');
            vm.hide = true;
            vm.show = true;
        }

        function _onBroadcast(e, data) {
            vm.show = false;
            vm.hide = false;
        }
    }


})();