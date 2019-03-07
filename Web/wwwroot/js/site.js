// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function AddManyToManyRelationShip(controller, action, queryString) {
    $.post("/" + controller + "/" + action + queryString, function (data) {
        debugger;
        var row = "<tr>" +
            "<td>" +
                data.name +
            "</td>" +
            "<td>" +
                data.description +
            "</td>" +
        "</tr>";
        $("table tbody").append(row);
    });
}
