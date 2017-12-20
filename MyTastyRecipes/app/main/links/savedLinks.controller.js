(function () {
    'use strict';

    angular
        .module('publicApp')
        .controller('savedLinksController', SavedLinksController);

    SavedLinksController.$inject = ['$scope', '$window', 'linksService'];

    function SavedLinksController($scope, $window, LinksService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.$window = $window;
        vm.linksService = LinksService;
        vm.makeLink = _makeLink;
        vm.makeLinkSuccess = _makeLinkSuccess;
        vm.getLinks = _getLinks;
        vm.getLinksSuccess = _getLinksSuccess;
        vm.getLinksError = _getLinksError;
        vm.getLink = _getLink;
        vm.getByIdSuccess = _getByIdSuccess;
        vm.getByIdError = _getByIdError;
        vm.updateLink = _updateLink;
        vm.updateLinkSuccess = _updateLinkSuccess;
        vm.updateLinkError = _updateLinkError;
        vm.deleteLink = _deleteLink;
        vm.deleteLinkSuccess = _deleteLinkSuccess;
        vm.deleteLinkError = _deleteLinkError;
        vm.setViews = _setViews;
        vm.linkContent = {};
        vm.make = true;
        vm.edit = true;

        function _onInit() {
            console.log('SavedlinksController');
            vm.getLinks();
        }

        function _setViews() {
            vm.make = true;
            vm.edit = true;
            vm.linkContent = {};
        }

        function _makeLink() {
            vm.linksService.saveLink(vm.linkContent)
                .then(vm.makeLinkSuccess)
                .catch(vm.makeLinkError);
        }

        function _makeLinkSuccess(resp) {
            console.log(resp);
            //vm.getLinks();
            vm.linksService = {};
            $window.location.reload();
        }

        function _makeLinkError(err) {
            console.log(err);
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

        function _getLink(id) {
            vm.make = false;
            vm.edit = false;
            vm.linksService.getLinkById(id)
                .then(vm.getByIdSuccess)
                .catch(vm.getByIdError);
        }

        function _getByIdSuccess(resp) {
            console.log(resp);
            vm.linkContent = resp.data;
        }

        function _getByIdError(err) {
            console.log(err);
        }

        function _updateLink() {
            vm.linksService.updateLink(vm.linkContent)
                .then(vm.updateLinkSuccess)
                .catch(vm.updateLinkError);
        }

        function _updateLinkSuccess(resp) {
            console.log(resp);
            vm.getLinks();
        }

        function _updateLinkError(err) {
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