var app = angular.module("app", []);

app.controller("appController", function appController($scope, $http) {
    $scope.asesores;
    $scope.asesorActual;

    $scope.listarAsesores = function () {
        $http({
            method: "get",
            url: "/Home/ListarAsesores",
            dataType: "json"
        }).then(function (response) {
            $scope.asesores = response.data;
        });
    };

    $scope.editarAsesor = function (asesor) {
        $scope.asesorActual = asesor;
        $("#editarAsesorModalLabel").html("Editar asesor");
        $("#editarAsesorModal").modal("show");
    }

    $scope.insertarAsesor = function () {
        $scope.asesorActual = {
            AsesorId: 0,
            Nombre: "",
            Cedula: "",
            Codigo: "",
            Tienda: ""
        };
        $("#editarAsesorModalLabel").html("Crear asesor");
        $("#editarAsesorModal").modal("show");
    }

    $scope.guardarAsesor = function () {
        if ($scope.asesorActual.AsesorId == 0) {//Insertar
            $http({
                method: "post",
                url: "/Home/InsertarAsesor",
                data: JSON.stringify($scope.asesorActual),
                dataType: "json"
            }).then(function (response) {
                alert("Asesor creado");
                $scope.listarAsesores();
                $("#editarAsesorModal").modal("hide");
            });
        } else {
            $http({
                method: "post",
                url: "/Home/ActualizarAsesor",
                data: JSON.stringify($scope.asesorActual),
                dataType: "json"
            }).then(function (response) {
                alert("Asesor actualizado");
                $scope.listarAsesores();
                $("#editarAsesorModal").modal("hide");
            });
        }
    };

    $scope.eliminarAsesor = function (asesor) {
        if (confirm("¿Está seguro que desea eliminar el asesor?")) {
            $scope.asesorActual = asesor;
            $http({
                method: "post",
                url: "/Home/EliminarAsesor",
                data: JSON.stringify($scope.asesorActual),
                dataType: "json"
            }).then(function (response) {
                alert("Asesor eliminado");
                $scope.listarAsesores();
                $("#editarAsesorModal").modal("hide");
            });
        }
    }
});