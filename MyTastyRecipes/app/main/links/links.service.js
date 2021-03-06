﻿(function () {
    'use strict';

    angular
        .module('publicApp')
        .factory('linksService', LinksService);

    LinksService.$inject = ['$http', '$q'];

    function LinksService($http, $q) {
        return {
            getAllLinks: _getAllLinks,
            saveLink: _saveLink,
            getSavedLinks: _getSavedLinks,
            getLinkById : _getLinkById,
            updateLink : _updateLink,
            deleteLink: _deleteLink
        };

        function _getAllLinks() {
            return $http.get('/api/scraping/getall').then(success).catch(error);
        }

        function _saveLink(data) {
            return $http.post('/api/links/save', data).then(success).catch(error);
        }

        function _getSavedLinks() {
            return $http.get('/api/links/getall').then(success).catch(error);
        }

        function _getLinkById(id) {
            return $http.get('/api/links/get/' + id).then(success).catch(error);
        }

        function _updateLink(data) {
            return $http.put('/api/links/update', data).then(success).catch(error);
        }

        function _deleteLink(id) {
            return $http.delete('/api/links/delete/' + id ).then(success).catch(error);

        }

        function success(resp) {
            return resp;
        }

        function error(err) {
            return $q.reject(err);
        }
    }


})();