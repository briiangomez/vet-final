function validarItem(dia) {
    var divs = document.getElementsByClassName("Item");

    var total = false;

    for (var i = 0; i < divs.length; i++) {
        if (divs[i].id == dia) {
            total = true;
        }
    }

    return total;
}

function sumarItems() {
    var divs = document.getElementsByClassName("Item");

    var total = 0;

    for (var i = 0; i < divs.length; i++)
    {
        total += parseFloat(divs[i].cells[3].innerText.replace(',', '.'));
    }
    $("#Total").val(total.toString().replace('.',','));
    $("#Importe").val(total.toString().replace('.', ','));
}