$(document).ready(function () {
    $("#products").kendoDropDownList({
        dataTextField: "name",
        dataValueField: "id",
        dataSource: {
            transport: {
                read: {
                    dataType: "json",
                    url: "https://localhost:44399/carrier/getall",
                }
            }
        }
    });
});