$(document).ready(function () {
    $("#tratar").click(function () {
        event.preventDefault();
        swal({
            title: "¿Seguro que quiere guardar este formulario?",
            text: "Verificar que todo este correcto antes de proseguir!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Guardar",
            cancelButtonText: "Cancelar"
        }).then(
            function (isConfirm) {
                if (isConfirm) {
                    $('#createsubmit').click();
                    var descripcion = $("#descripcion").val();
                    var marca = $("#marca").val();
                    var fechaAdquisicion = $("#fechaAdquisicion").val();

                    if (descripcion === "" && marca === "" && fechaAdquisicion === "") {
                        swal("Error", "Complete los campos requeridos", "error")
                    } else {


                    }
                }
            }, function (dismiss) {
                if (dismiss == 'cancel') {
                    swal(
                        'Cancelado',
                        'Has cancelado el proceso)',
                        'error')
                }
            }
        ).catch(swal.noop);
    });

    $("#ddlAsignarPersona").change(function () {
        $("#txtAsignacion").val($(this).find("option:selected").text());
    });

    function linkaction() {
        alert(1);
    }
    $("#filtrar").click(function (event) {
        event.preventDefault();
        alert(1)
    });
});