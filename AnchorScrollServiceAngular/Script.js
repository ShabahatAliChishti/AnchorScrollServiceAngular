var demoApp = angular.module("demoApp", [])
    .controller("demoController", function ($scope, $location, $anchorScroll) {
        debugger;
        $scope.scrollTo = function (scrollLocation) {
            //what ever we provide it appends to url
            //let say id is append to the url #1
            $location.hash(scrollLocation)            
            //browser aur content me space dene ke leye yoffset ki property hai
            $anchorScroll.yOffset = 20;
                        //anchorscroll method it going to read what we have in url and automatically scroll to that page

            $anchorScroll();


        }
    });
