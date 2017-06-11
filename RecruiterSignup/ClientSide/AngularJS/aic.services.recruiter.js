aic.services.recruiter = aic.services.recruiter || {};

(function () {
    "use strict";

    angular.module('aicApp').factory('$recruiterService', recruiterServiceFactory);

    recruiterServiceFactory.$inject = ['$baseService', '$aic'];

    function recruiterServiceFactory($baseService, $aic) {
        var aaicServiceObject = aic.services.recruiter;
        var newService = $baseService.merge(true, {}, aaicServiceObject, $baseService);
        return newService;
    }
})();

var PlacesKey = '<%= System.Configuration.ConfigurationManager.AppSettings["PlacesPublicKey"].ToString() %>';

aic.services.recruiter.PostRecruiter = function (data, onSuccess) {
    aic.page.sendAjax("recruiter", aic.page.ajaxType.POST, data, onSuccess)
}
    
aic.services.recruiter.GetCounty = function (path, onSuccess) {

    var url = "https://maps.googleapis.com/maps/api/" + path;

    var settings = {
        success: onSuccess,
        error: aic.page.handlers.onError,
        type: "GET",
    };
    $.ajax(url, settings);
};

