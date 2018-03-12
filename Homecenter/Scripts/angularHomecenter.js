var app = angular.module("app", []);

app.controller("asesoresController", function ($scope, $http) {
    $scope.asesores;
    $scope.asesorActual;

    $scope.listarAsesores = function () {
        $http({
            method: "get",
            url: "/Asesores/ListarAsesores?inicio=" + 0 + "&numRegistros=" + 10,
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
                url: "/Asesores/InsertarAsesor",
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
                url: "/Asesores/ActualizarAsesor",
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
                url: "/Asesores/EliminarAsesor",
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

app.controller("ventaController", function ($scope, $http) {
    $scope.asesor;
    $scope.venta = {
        AsesorId: 0,
        SKU: ""
    };
    $scope.reporte = {
        FechaInicio: "",
        FechaFin: ""
    };

    $scope.buscarAsesorXCedula = function () {
        $http({
            method: "post",
            url: "/Asesores/BuscarXCedula",
            data: {
                cedula: $scope.asesor.Cedula
            },
            dataType: "json"
        }).then(function (response) {
            if (!response.data) {
                alert("Asesor no encontrado");
            } else {
                $scope.asesor = response.data;
                $scope.venta.AsesorId = $scope.asesor.AsesorId;
            }
        });
    }

    $scope.buscarGarantiaXSKU = function () {
        $http({
            method: "post",
            url: "/Garantias/BuscarXSKU",
            data: {
                sku: $scope.venta.SKU
            },
            dataType: "json"
        }).then(function (response) {
            if (!response.data) {
                alert("Garantía no encontrada");
            } else {
                $scope.garantia = response.data;
                $scope.garantia.TotalComision = ($scope.garantia.PrecioSinIva * $scope.garantia.PorcentajeComision) / 100;
            }
        });
    }

    $scope.guardarVenta = function () {
        if ($scope.venta.AsesorId != 0) {//Válido
            $http({
                method: "post",
                url: "/Ventas/InsertarVenta",
                data: JSON.stringify($scope.venta),
                dataType: "json"
            }).then(function (response) {
                alert("Venta registrada");
                $scope.venta.AsesorId = 0;
                $scope.venta.SKU = "";

                $scope.asesor.AsesorId = 0;
                $scope.asesor.Cedula = "";
                $scope.asesor.Nombre = "";
            });
        } else {
            alert("Asesor no válido");
        }
    };

    $scope.generarReporte = function () {
        $scope.reporte.FechaInicio = $("#fechaInicio").val();
        $scope.reporte.FechaFin = $("#fechaFin").val();
        $http({
            method: "post",
            url: "/Ventas/GenerarReporte",
            data: {
                fechaInicio: $scope.reporte.FechaInicio,
                fechaFin: $scope.reporte.FechaFin
            },
            dataType: "json"
        }).then(function () {
            window.open("/reportes/reporte.xlsx");
        });
    };
});

app.controller("garantiasController", function ($scope, $http) {
    $scope.garantias;
    $scope.garantiaActual;

    $scope.listarGarantias = function () {
        $http({
            method: "get",
            url: "/Garantias/ListarGarantias?inicio=" + 0 + "&numRegistros=" + 10,
            dataType: "json"
        }).then(function (response) {
            $scope.garantias = response.data;
        });
    };

    $scope.editarGarantia = function (garantia) {
        console.log(garantia);
        $scope.garantiaActual = garantia;
        $("#editarGarantiaModalLabel").html("Editar garantía");
        $("#editarGarantiaModal").modal("show");
    }

    $scope.insertarGarantia = function () {
        $scope.garantiaActual = {
            GarantiaId: 0,
            Categoria: "",
            SKU: "",
            Descripcion: "",
            PrecioSinIva: 0,
            PrecioConIva: 0,
            PorcentajeComision: 0
        };
        $("#editarGarantiaModalLabel").html("Crear garantía");
        $("#editarGarantiaModal").modal("show");
    }

    $scope.guardarGarantia = function () {
        if ($scope.garantiaActual.GarantiaId == 0) {//Insertar
            $http({
                method: "post",
                url: "/Garantias/InsertarGarantia",
                data: JSON.stringify($scope.garantiaActual),
                dataType: "json"
            }).then(function (response) {
                alert("Garantía creada");
                $scope.listarGarantias();
                $("#editarGarantiaModal").modal("hide");
            });
        } else {
            $http({
                method: "post",
                url: "/Garantias/ActualizarGarantia",
                data: JSON.stringify($scope.garantiaActual),
                dataType: "json"
            }).then(function (response) {
                alert("Garantía actualizada");
                $scope.listarGarantias();
                $("#editarGarantiaModal").modal("hide");
            });
        }
    };

    $scope.eliminarGarantia = function (garantia) {
        if (confirm("¿Está seguro que desea eliminar la garantía?")) {
            $scope.garantiaActual = garantia;
            $http({
                method: "post",
                url: "/Garantias/EliminarGarantia",
                data: JSON.stringify($scope.garantiaActual),
                dataType: "json"
            }).then(function (response) {
                alert("Garantía eliminada");
                $scope.listarGarantias();
                $("#editarGarantiaModal").modal("hide");
            });
        }
    }
});
