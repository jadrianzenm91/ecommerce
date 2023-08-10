/// <summary>
/// Script de Vista 
/// </summary>
/// <remarks>
/// </remarks>
try {
    ns('Ecommerce.Web.Dashboard.Index');
    $(document).ready(function () {
        'use strict';
        Ecommerce.Web.Dashboard.Index.Page = new Ecommerce.Web.Dashboard.Controller();
        Ecommerce.Web.Dashboard.Index.Page.Ini();
        
    });
} catch (ex) {
    alert(ex.message);
}

