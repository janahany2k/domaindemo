(function () {
    "use strict";

    angular
        .module("app")
        .controller("domainCtrl", domainCtrl);

    domainCtrl.$inject = ["domainSrvc", "toastr"];

    function domainCtrl(domainSrvc, toastr) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = "Domain";

        vm.domain = null;
        vm.enumerate = "10";
        vm.subdomains = [];

        // define functions available to the interface
        vm.getSubdomains = getSubdomains;
        vm.findIpAddresses = findIpAddresses;

        activate();

        function activate() {
            
        }

        function getSubdomains() {
            if (!angular.isDefined(vm.domain)) {
                toastr.error("Domain is required.");
            }

            domainSrvc.getSubdomains(vm.enumerate, vm.domain)
                .then(function (data) {
                    console.log(data);
                    vm.subdomains = data;
                });
        }

        function findIpAddresses() {
            domainSrvc.getIpAddresses(vm.subdomains)
                .then(function(data) {
                    console.log(data);
                    vm.subdomains = data;
                });}
    }
})();
