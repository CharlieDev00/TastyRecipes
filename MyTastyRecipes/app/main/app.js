(function () {
    'use strict';
    window.APP = window.APP || {};
    angular
        .module('publicApp', ['ui.router', 'publicApp.routes', 'ui.bootstrap', 'ngCookies', 'ngRoute', 'ui.tinymce', 'angular-img-cropper']);

})();