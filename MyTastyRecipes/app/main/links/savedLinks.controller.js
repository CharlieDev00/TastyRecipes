(function () {
    'use strict';

    angular
        .module('publicApp')
        .controller('savedLinksController', SavedLinksController);

    SavedLinksController.$inject = ['$scope', 'linksService'];

    function SavedLinksController($scope, LinksService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.linksService = LinksService;
        vm.getLinks = _getLinks;
        vm.getLinksSuccess = _getLinksSuccess;
        vm.getLinksError = _getLinksError;
        vm.deleteLink = _deleteLink;
        vm.deleteLinkSuccess = _deleteLinkSuccess;
        vm.deleteLinkError = _deleteLinkError;

        function _onInit() {
            console.log('LinksController');
            vm.getLinks();
        }

        function _getLinks() {
            vm.linksService.getSavedLinks()
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

        function _deleteLink(id) {
            vm.linksService.deleteLink(id)
                .then(vm.deleteLinkSuccess)
                .catch(vm.deleteLinkError);
        }

        function _deleteLinkSuccess(resp) {
            console.log(resp);
            vm.getLinks();
        }

        function _deleteLinkError(err) {
            console.log(err);
        }
    }


})();