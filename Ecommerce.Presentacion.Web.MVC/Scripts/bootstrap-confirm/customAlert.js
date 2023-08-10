window.alert = function (msj, type) {
    //mensaje = mensaje.split('\n').join('<br/>');
    mostrarMsjServidor(msj, type);
};

window.confirm = function (mensaje, callbackok, callbackcancel) {
    mensaje = mensaje.split('\n').join('<br/>');
    MostrarAlerta(mensaje, 'confirm', callbackok, callbackcancel);
};

//function MostrarAlerta(mensaje, tipo, callbackOk, callbackCancel) {
//    if (tipo == "ALERT") {
        
//        BootstrapDialog.show({
//            title: "Mensaje del Sistema",
//            message: mensaje,
//            type: BootstrapDialog.TYPE_WARNING,
//            closable: true,
//            buttons: [{
//                label: 'Aceptar.',
//                cssClass: 'btn-warning'
//            }],
//            callback: function (result) {
//                if (typeof callbackOk == 'function') { callbackOk(); }
//            }
           
//        });
//    } else {
        
//        title = "Mensaje de confirmación";
        
//        BootstrapDialog.confirm(title, mensaje, function (result) {
//            if (result) {
//                if (callbackOk != undefined) {
//                    if (typeof callbackOk == 'function') {
//                        callbackOk();
//                    }
//                }
//                else {
//                    return true;
//                }
//            } else {
//                if (callbackCancel != undefined) {
//                    if (typeof callbackCancel == 'function') {
//                        callbackCancel();
//                    }
//                } else {
//                    return false;
//                }
//            }
//        });
//    }
//}

function mostrarMsjServidor(msj, type) {
    
    var dialogInstance1 = new BootstrapDialog({
        type: type == 'error' ? BootstrapDialog.TYPE_DANGER : BootstrapDialog.TYPE_PRIMARY,
        message: msj,
        animate: true,
        closeByBackdrop: false,
        closeByKeyboard: false,
        closable: false
    });

    dialogInstance1.open();

    setTimeout(function () {
        dialogInstance1.close();
    }, 2500);

}

function MostrarPopUp(idModal, title, idmodalBody, size) {
    if (idmodalBody != undefined && idmodalBody != "") {
        var $divModalBody = $("<div id=\'" + idmodalBody + "\'></div>");
    } else {
        var $divModalBody = $("<div id='divModalBody'></div>");
    }
    
    var dialogInstance1 = new BootstrapDialog({
        id: idModal,
        closable: true,
        title: $("<h2>" + title + "</h2>"),
        message: $divModalBody,
        draggable: true,
        closeByBackdrop: false
    });
    dialogInstance1.open();
    if (size == 'SIZE_WIDE') {
        BootstrapDialog.dialogs[idModal].setSize(BootstrapDialog.SIZE_WIDE);
    }
}