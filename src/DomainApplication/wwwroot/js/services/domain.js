(function () {
    "use strict";

    angular
        .module("app")
        .factory("domainSrvc", domainSrvc);

    domainSrvc.$inject = ["$http", "serverRoot"];

    function domainSrvc($http, serverRoot) {

        var service = {
            getSubdomains: getSubdomains,
            getIpAddresses: getIpAddresses
        };

        return service;

        function getSubdomains(enumerate, domain) {
            return $http({ method: "GET", url: serverRoot + "subdomain/" + enumerate + "/" + domain })
                .then(function (results) {
                    return results.data;
                });
        }

        function getIpAddresses(subdomains) {
            console.log(subdomains);
            return $http({ method: "POST", url: serverRoot + "subdomain/findIPAddresses", data: subdomains })
                .then(function (result) {
                    return result.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    }
})();