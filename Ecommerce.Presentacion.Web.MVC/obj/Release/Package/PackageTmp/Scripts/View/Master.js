try {
    $.xAjaxPool = [];
    $.ajaxSetup({
        beforeSend: function (xhr, jqXAjax) {
            $.xAjaxPool.push(jqXAjax);
            $('body .modal-preload').removeClass('d-none');
            $('body .modal-preload').removeClass('hidden');
        },
        complete: function (jqXAjax) {
            var index = $.xAjaxPool.indexOf(jqXAjax);
            if (index > -1) {
                $.xAjaxPool.splice(index, 1);
            }
            $('body .modal-preload').addClass('d-none');
            $('body .modal-preload').addClass('hidden');
            if ($.xAjaxPool.length == 0) {
                $('body .modal-preload').addClass('d-none');
                $('body .modal-preload').addClass('hidden');

                //elimina el drop
                /*var controls = $("input[type=text], input[type=password], textarea");
                controls.bind("drop", function () {
                    return false;
                });
                controls = undefined;*/
                //fin del drop
            }

        },
        error: function (jqXAjax) {
            var index = $.xAjaxPool.indexOf(jqXAjax);
            if (index > -1) {
                $.xAjaxPool.splice(index, 1);
            }
            $('body .modal-preload').addClass('d-none');
            $('body .modal-preload').addClass('hidden');
        }
    });
} catch (e) {
}

function getLocation(obj) {
    //var x = document.getElementById("lblloggps");
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showLoc, showErr);
        //x.innerHTML = 'Getting location...';
    } else {
        //x.innerHTML = "Geolocation is not supported by your browser!!!";
    }
}
function showLoc(position) {
    var x = document.getElementById("direccion_entrega");
    x.innerText = position.coords.latitude + "," + position.coords.longitude;
    document.getElementById("lblloggps").innerText='';

}

function showErr(error) {
    var x = document.getElementById("lblloggps");
    switch (error.code) {
        case error.PERMISSION_DENIED:
            x.innerHTML = "User denied the request for Geolocation.";
            break;
        case error.POSITION_UNAVAILABLE:
            x.innerHTML = "Location information is unavailable.";
            break;
        case error.TIMEOUT:
            x.innerHTML = "The request to get user location timed out.";
            break;
        case error.UNKNOWN_ERROR:
            x.innerHTML = "An unknown error occurred.";
            break;
    }
}
function alerta(type, msj) {
    $('#divAlerta').html(
                '<div class="alert alert-' + type + ' m-2">' +
                    '<a href="#" class="close" data-dismiss="alert">X</a>' +
                    '<strong>' + type + '!</strong><span> ' + msj + '</span>' +
                '</div>').show(0).delay(5000).fadeOut(5000);
}