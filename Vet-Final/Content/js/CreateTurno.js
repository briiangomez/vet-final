$.getScript('/Content/js/jquery-ui.min.js', function () {

    $("#txtCliente").change(function () {
        $.get("/Mascotas/MascotasPorCliente", { clienteID: $("#ClienteId").val() }, function (data) {
            $("#MascotaId").empty();
            $.each(data, function (index, row) {
                $("#MascotaId").append("<option value='" + row.id + "'>" + row.name + "</option>");
            });
        });

        
    });

    $("#FechaId").change(function () {
        if ($("#MedicoId").val() != "" && $("#SalaId").val() != "" && $("#FechaId").val() != "" && $("#EspecialidadId").val() != "") {
            $.get("/Turnos/ObtenerHorarios", { medicoID: $("#MedicoId").val(), salaID: $("#SalaId").val(), fecha: $("#FechaId").val(), EspecialidadId: $("#EspecialidadId").val() }, function (data) {
                $("#Fecha").empty();
                $.each(data, function (index, row) {
                    $("#Fecha").append("<option value='" + row.id + "'>" + row.name + "</option>");
                });
            });
        }


    });
});