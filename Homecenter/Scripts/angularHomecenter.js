var numRegistrosXPagina = 25;

//Aplicación angular
var app = angular.module("app", ['ngMessages']);

//Controlador para el listado de asesores
app.controller("asesoresController", function ($scope, $http) {
    $scope.asesores;
    $scope.asesorActual;
    $scope.paginaActual = 0;
    $scope.ultimaPagina = 0;

    //Trae la lista de asesores
    $scope.listarAsesores = function (pagina) {
        if (pagina < 0) {
            pagina = 0;
        }
        if (pagina > $scope.ultimaPagina) {
            pagina = $scope.ultimaPagina;
        }
        $http({
            method: "post",
            url: "/Asesores/ContarAsesores",
            dataType: "json"
        }).then(function (response) {
            $scope.ultimaPagina = Math.floor((response.data - 1) / numRegistrosXPagina);
            $http({
                method: "post",
                url: "/Asesores/ListarAsesores",
                data: {
                    inicio: (pagina * numRegistrosXPagina),
                    numRegistros: numRegistrosXPagina
                },
                dataType: "json"
            }).then(function (response) {
                $scope.paginaActual = pagina;
                $scope.asesores = response.data;
            });
        });
    };

    //Muestra el formulario de edición de un asesor
    $scope.editarAsesor = function (asesor) {
        $scope.asesorActual = asesor;
        $("#editarAsesorModalLabel").html("Editar asesor");
        $("#editarAsesorModal").modal("show");
    }

    //Muestra el formulario de inserción de un asesor
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

    //Actualiza o inserta un asesor
    $scope.guardarAsesor = function (valido) {
        if (!valido) {
            alert("Debe diligenciar correctamente el formulario");
        } else {
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
        }
    };

    //Elimina un asesor
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

//Controlador para el listado de ventas
app.controller("ventaController", function ($scope, $http) {
    $scope.asesor;
    $scope.venta = {
        AsesorId: 0,
        SKU: "",
        CedulaCliente: "",
        SKUElectrodomestico: "",
        ValorComision: 0
    };
    $scope.reporte = {
        FechaInicio: "",
        FechaFin: ""
    };
    $scope.ventas;
    $scope.paginaActual = 0;
    $scope.ultimaPagina = 0;

    //Busca un asesor por su cédula
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

    //Busca una garantía por su SKU
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
                $scope.venta.ValorComision = ($scope.garantia.PrecioSinIva * $scope.garantia.PorcentajeComision) / 100;
            }
        });
    }

    //Inserta una nueva venta
    $scope.guardarVenta = function (valido) {
        if (!valido || $scope.venta.ValorComision == 0 || $scope.venta.AsesorId == 0) {
            alert("Debe diligenciar correctamente el formulario");
        } else {
            $http({
                method: "post",
                url: "/Ventas/InsertarVenta",
                data: JSON.stringify($scope.venta),
                dataType: "json"
            }).then(function (response) {
                alert("Venta registrada");
                $scope.venta.AsesorId = 0;
                $scope.venta.SKU = "";
                $scope.venta.CedulaCliente = "";
                $scope.venta.SKUElectrodomestico = "";
                $scope.venta.ValorComision = 0;

                $scope.asesor.AsesorId = 0;
                $scope.asesor.Cedula = "";
                $scope.asesor.Nombre = "";
            });
        }
    };

    //Genera el reporte de venta
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

    //Muestra el listado de comisiones del asesor logueado
    $scope.generarReporteComisiones = function (pagina) {
        if (pagina < 0) {
            pagina = 0;
        }
        if (pagina > $scope.ultimaPagina) {
            pagina = $scope.ultimaPagina;
        }
        $scope.reporte.FechaInicio = $("#fechaInicio").val();
        $scope.reporte.FechaFin = $("#fechaFin").val();

        $http({
            method: "post",
            url: "/Ventas/ContarComisiones",
            data: {
                fechaInicio: $scope.reporte.FechaInicio,
                fechaFin: $scope.reporte.FechaFin
            },
            dataType: "json"
        }).then(function (response) {
            $scope.ultimaPagina = Math.floor((response.data - 1) / numRegistrosXPagina);
            $http({
                method: "post",
                url: "/Ventas/ListarComisiones",
                data: {
                    fechaInicio: $scope.reporte.FechaInicio,
                    fechaFin: $scope.reporte.FechaFin,
                    inicio: (pagina * numRegistrosXPagina),
                    numRegitros: numRegistrosXPagina
                },
                dataType: "json"
            }).then(function (response) {
                $scope.paginaActual = pagina;
                $scope.ventas = response.data;
                $scope.ventas.forEach(function (venta) {
                    console.log(venta);
                    venta.FechaRegistro = new Date(parseInt(venta.FechaRegistro.replace("/Date(", "").replace(")/", ""), 10));
                    console.log(venta);
                });
            });
        });
    };
});

//Controlador para el listado de garantías
app.controller("garantiasController", function ($scope, $http) {
    $scope.garantias;
    $scope.garantiaActual;
    $scope.paginaActual = 0;
    $scope.ultimaPagina = 0;

    //Listado de garantías
    $scope.listarGarantias = function (pagina) {
        if (pagina < 0) {
            pagina = 0;
        }
        if (pagina > $scope.ultimaPagina) {
            pagina = $scope.ultimaPagina;
        }
        $http({
            method: "post",
            url: "/Garantias/ContarGarantias",
            dataType: "json"
        }).then(function (response) {
            $scope.ultimaPagina = Math.floor((response.data - 1) / numRegistrosXPagina);
            $http({
                method: "post",
                url: "/Garantias/ListarGarantias",
                data: {
                    inicio: (pagina * numRegistrosXPagina),
                    numRegistros: numRegistrosXPagina
                },
                dataType: "json"
            }).then(function (response) {
                $scope.garantias = response.data;
            });
        });
    };

    //Muestra el formulario para editar una garantía
    $scope.editarGarantia = function (garantia) {
        $scope.garantiaActual = garantia;
        $("#editarGarantiaModalLabel").html("Editar garantía");
        $("#editarGarantiaModal").modal("show");
    }

    //Muestra el formulario para insertar una garantía
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

    //Actualiza o inserta una garantía
    $scope.guardarGarantia = function (valido) {
        if (!valido) {
            alert("Debe diligenciar correctamente el formulario");
        } else {
            if ($scope.garantiaActual.GarantiaId == 0) {//Insertar
                $http({
                    method: "post",
                    url: "/Garantias/InsertarGarantia",
                    data: JSON.stringify($scope.garantiaActual, function (key, value) {
                        if (key == "PrecioSinIva" || key == "PrecioConIva" || key == "PorcentajeComision") {
                            return ("" + value).replace(".", ",");
                        } else {
                            return value;
                        }
                    }),
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
                    data: JSON.stringify($scope.garantiaActual, function (key, value) {
                        if (key == "PrecioSinIva" || key == "PrecioConIva" || key == "PorcentajeComision") {
                            return ("" + value).replace(".", ",");
                        } else {
                            return value;
                        }
                    }),
                    dataType: "json"
                }).then(function (response) {
                    alert("Garantía actualizada");
                    $scope.listarGarantias();
                    $("#editarGarantiaModal").modal("hide");
                });
            }
        }
    };

    //Elimina una garantía
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
