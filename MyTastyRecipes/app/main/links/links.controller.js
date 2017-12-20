(function () {
    'use strict';

    angular
        .module('publicApp')
        .controller('linksController', LinksController);

    LinksController.$inject = ['$scope', 'linksService'];

    function LinksController($scope, LinksService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.linksService = LinksService;
        vm.getLinks = _getLinks;
        vm.getLinksSuccess = _getLinksSuccess;
        vm.getLinksError = _getLinksError;
        vm.saveLinks = _saveLinks;
        vm.saveLinkSuccess = _saveLinkSuccess;
        vm.saveLinkError = _saveLinkError;
        vm.links = {};
        vm.linkContent = {};

        function _onInit() {
            console.log('LinksController');
            vm.getLinks();
        }

        function _getLinks() {
            vm.linksService.getAllLinks()
                .then(vm.getLinksSuccess)
                .catch(vm.getLinksError);
        }

        function _getLinksSuccess(resp) {
            console.log(resp);
            vm.links = resp.data;
        }

        function _getLinksError(err) {
            console.log(err);
        }

        function _saveLinks(data) {
            vm.linksService.saveLink(data)
                .then(vm.saveLinkSuccess)
                .catch(vm.saveLinkError);
        }

        function _saveLinkSuccess(resp) {
            console.log(resp);
        }

        function _saveLinkError(err) {
            console.log(err);
        }
    }


})();