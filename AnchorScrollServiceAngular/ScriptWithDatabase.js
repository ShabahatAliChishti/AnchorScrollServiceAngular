var demoApp = angular.module("demoApp", [])
    .controller("countryController",
        function ($scope, $location, $anchorScroll, $http) {

            $http.get("CountryService.asmx/GetData")
                //it gives us promise so then
                //when request completes it gives response back
                .then(function (response) {
                    
                    $scope.countries = response.data;
                });

            $scope.scrollTo = function (countryName) {
                $location.hash(countryName);
                $anchorScroll();
            }

        });
