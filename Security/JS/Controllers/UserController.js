/// <reference path="../angular.js" />
http://www.dotnetcurry.com/showarticle.aspx?ID=1049

/// <reference path="Module.js" />

//The controller is having 'crudService' dependency.
//This controller makes call to methods from the service
app.controller('crudController', function ($scope, crudService) {

    $scope.IsNewRecord = 1; //The flag for the new record

    //Method to Get Single Employee based on EmpNo
        loadRecords();

        $scope.getNameFromServer = function (pLoginName) {
            var promiseGet = crudService.CheckLoginName(pLoginName); //The MEthod Call from service

            promiseGet.then(function (pl) { $scope.availableMessage = pl.data },
                  function (errorPl) {
                      $log.error('failure loading Users', errorPl);
                  });
        };

        $scope.computeNeeded = function () {
            $scope.needed = $scope.myvalue * 10;
        };

        //Function to load all Employee records
        function loadRecords() {
            var promiseGet = crudService.GetUsers(); //The MEthod Call from service

            promiseGet.then(function (pl) { $scope.Users = pl.data },
                  function (errorPl) {
                      $log.error('failure loading Users', errorPl);
                  });
        }
});