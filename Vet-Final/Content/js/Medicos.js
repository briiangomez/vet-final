

$("#ListHorarios").on('click', '.borrar', function () {
    $(this).parent().parent().remove();
    actualizarHorarios();
});

$("#ListEspecialidades").on('click', '.borrar', function () {
    $(this).parent().parent().remove();
    actualizarEspecialidad();
});

function validarHora(dia)
{
    var divs = document.getElementsByClassName("Horarios");

    var total = false;

    for (var i = 0; i < divs.length; i++)
    {
        if(divs[i].id == dia)
        {
            total = true;
        }
    }

    return total;
}

function validarEspecialidad(dia) {
    var divs = document.getElementsByClassName("Especialidad");

    var total = false;

    for (var i = 0; i < divs.length; i++) {
        if (divs[i].id == dia) {
            total = true;
        }
    }

    return total;
}

$(document).ready(function ()
{

});
