(function () {
    'use strict'

    angular.module('aicApp')
        .factory('$workHistoryService', workHistoryServiceFactory); // creates an instance of our service

    workHistoryServiceFactory.$inject = ['$baseService', '$aic'];

    function workHistoryServiceFactory($baseService, $aic) {

        var aicServiceObject = aic.services.workhistory;
        var newService = $baseService.merge(true, {}, aicServiceObject, $baseService);

        return newService;
    }
})();
