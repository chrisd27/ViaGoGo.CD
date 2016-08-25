var app = angular.module('app', []);

app.controller('appController', function ($scope, $http) {
    $scope.loading = false;
    $scope.search = function () {
        $scope.showTickets = false;
        $scope.showEvents = false;
        $scope.loading = true;
        $http({
            method: "POST",
            url: "/",
            data: {searchText:$scope.searchText}
        }).then(function success(response) {
            $scope.errorMessage = "";
            $scope.data = JSON.parse(response.data);
            if ($scope.data.data != undefined && $scope.data.data.results == "0") {
                $scope.errorMessage = "No results found! Please search again.";
            } else {
                $scope.events = $scope.data;
                $scope.showEvents = true;
            }
            $scope.loading = false;
        }, function error(response) {
            $scope.errorMessage = response.statusText;
            $scope.showEvents = false;
            $scope.showTickets = false;
            $scope.loading = false;
        });
    }

    $scope.getTickets = function (eventId) {
        $scope.showTickets = false;
        $scope.showEvents = false;
        $scope.loading = true;
        $scope.prevShow = false;
        $scope.nextShow = false;
        $http({
            method: "POST",
            url: "Home/Listings",
            data: { "eventId": eventId }
        }).then(function success(response) {
            $scope.errorMessage = "";
            $scope.data = JSON.parse(response.data);
            $scope.prevLink = $scope.data.LastLink.HRef;
            $scope.nextLink = $scope.data.NextLink.HRef;
            $scope.listings = $scope.data.Items;
            $scope.showEvents = false;
            $scope.showTickets = true;
            
            // set page next and prev buttons
            $scope.prevListing = true;
            $scope.nextListing = true;
            var totalPages = $scope.data.totalItems % $scope.data.PageSize != 0 ? Math.ceil($scope.data.totalItems / $scope.data.PageSize) : $scope.data.totalItems / $scope.data.PageSize;
            if ($scope.data.Page > 1 && $scope.data.Page != totalPages) {
                $scope.prevListing = true;
                $scope.nextListing = true;
            } else if ($scope.data.Page == 1 && $scope.data.Page != totalPages) {
                $scope.prevListing = false;
                $scope.nextListing = true;
            } else if ($scope.data.Page > 1 && $scope.data.Page == totalPages) {
                $scope.prevListing = true;
                $scope.nextListing = false;
            }
            $scope.loading = false;
        }, function error(response) {
            // TODO ERROR MESSAGE
            $scope.errorMessage = "There has been an error";
            $scope.showEvents = false;
            $scope.showTickets = false;
            $scope.loading = false;
        });
    }
    $scope.pagination = function (paginationURL) {
        $scope.loading = true;
        $http({
            method: "POST",
            url: "Home/Pagination",
            data: { paginationURL: paginationURL }
        }).then(function success(response) {
            $scope.errorMessage = "";
            $scope.data = JSON.parse(response.data);
         //   $scope.prevLink = $scope.data.LastLink.HRef;
        //    $scope.nextLink = $scope.data.NextLink.HRef;
            $scope.listings = $scope.data.Items;
            $scope.showEvents = false;
            $scope.showTickets = true;
            $scope.loading = false;
        }, function error(response) {
            // TODO ERROR MESSAGE
            $scope.errorMessage = response.statusText;
            $scope.showEvents = false;
            $scope.showTickets = false;
            $scope.loading = false;
        });

    }


})

app.filter("appDate", function () {
    var re = /\/Date\(([0-9]*)\)\//;
    return function (x) {
        var m = x.match(re);
        if (m) return new Date(parseInt(m[1]));
        else return null;
    };
});



