$(document).ready(function () {
    $("#logGrid").kendoGrid({
        dataSource: dataSource,
        height: 550,
        sortable: true,
        pageable: true,
        columns: [
            {
                field: "action",
                title: "Action"
            },
            {
                field: "dateTime",
                title: "Date Time",
                format: "{0: yyyy-MM-dd HH:mm:ss}"
            },
            {
                field: "carrierName",
                title: "Carrier Name"
            },
            {
                field: "shipmentId",
                title: "Shipment Id"
            }
        ]
    });
});