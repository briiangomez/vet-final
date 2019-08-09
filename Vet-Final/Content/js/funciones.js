/*Validar Precio*/
+function ($) {
    $.validator.methods.number = function (value, element) {
        value = floatValue(value);
        return this.optional(element) || !isNaN(value);
    }
    $.validator.methods.range = function (value, element, param) {
        value = floatValue(value);
        return this.optional(element) || (value >= param[0] && value <= param[1]);
    }
    function floatValue(value) {
        return parseFloat(value.replace(",", "."));
    }
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || moment(value, "DD.MM.YYYY", true).isValid();
    }
}(jQuery);

/*Selector de Fecha JQuery UI*/
+function ($) {
    $('.fecha').datepicker({
        changeMonth: true,
        changeYear: true,
        maxDate: 0,
        yearRange: "-100:+0",
        dateFormat: "dd/mm/yy"
    });

    $('.fechaT').datepicker({
        changeMonth: true,
        changeYear: true, 
        minDate: 0,
        yearRange: "-100:+0",
        dateFormat: "dd/mm/yy"
    });
    
    $.validator.addMethod('date', function (value, element, params) {
        if (this.optional(element)) {
            return true;
        }
        var ok = true;
        try {
            $.datepicker.parseDate('dd/mm/yyyy', value);
        }
        catch (err) {
            ok = false;
        }
        return ok;
    });
}(jQuery);


/*Ventana Modal*/
+function ($) {
    /*Abriendo Ventana Modal*/
    $("#btnCliente").click(function () {
        $('#modal').modal('show');
    });
    $("#btnOK").click(function () {
        $('#ModalMensaje').modal('hide');
    });
}(jQuery);

/* AutoComplete*/
$.getScript('/Content/js/jquery-ui.min.js', function () {
    // script is now loaded and executed.
    // put your dependent JS here.

    $('#txtCliente').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Clientes/GetByName?term=" + request.term,
                type: "POST",
                dataType: "json",
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Value, value: item.Value, id: item.Key };
                    }))
                }
            });
        },
        select: function (event, ui) {
            $('#ClienteId').val(ui.item.id);
            $('#txtCliente').val(ui.item.value);
        }
    });

    $('#txtRaza').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Mascotas/GetRazaByName?term=" + request.term,
                type: "POST",
                dataType: "json",
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Value, value: item.Value, id: item.Key };
                    }))
                }
            });
        },
        select: function (event, ui) {
            $('#RazaId').val(ui.item.id);
            $('#txtRaza').val(ui.item.value);
        }
    });

    $('#txtMedico').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Medicos/GetByName?term=" + request.term + "&idEspecialidad=" + $('#EspecialidadId').val(),
                type: "POST",
                dataType: "json",
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Value, value: item.Value, id: item.Key };
                    }))
                }
            });
        },
        select: function (event, ui) {
            $('#MedicoId').val(ui.item.id);
            $('#txtMedico').val(ui.item.value);
        }
    });


    $('#txtEspecialidad').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Medicos/GetEspecialidadByName?term=" + request.term,
                type: "POST",
                dataType: "json",
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Value, value: item.Value, id: item.Key };
                    }))
                }
            });
        },
        select: function (event, ui) {
            $('#EspecialidadId').val(ui.item.id);
            $('#txtEspecialidad').val(ui.item.value);
        }
    });
});
