const uri = "https://localhost:44399/shipment/getall";

kendo.ui.FilterMenu.fn.options.operators.number = {
    gt: "Greater than",
    eq: "Equal to"
};

kendo.ui.FilterMenu.fn.options.operators.string = {
    eq: "Contain"
};

var dataSourceq = new kendo.data.DataSource({
    transport: {
        read: {
            dataType: "json",
            url: "https://localhost:44399/shipment/getall"
        },
        parameterMap: function (data) {
            let countSort = data.sort.length;
            let countFilter = data.filter; 
            let url = ""
            if (countSort != 0) {
                let name = data.sort[0].field;
                let paramSort = data.sort[0].dir;
                if (paramSort == 'asc') {
                    url += "&$OrderBy=" + name;
                } else {
                    url += "&$OrderBy=" + name + ' desc';
                }
            }

            if (countFilter) {
                for (let i = 0; i < countFilter.filters.length; i++) {
                    if ((typeof countFilter.filters[i].value) == "number") {
                        url += "&$Filter=" + countFilter.filters[i].field + " " + countFilter.filters[i].operator + " " + countFilter.filters[i].value;
                    }
                    else {
                        url += "&$Filter=contains(" + countFilter.filters[i].field + `,'` + countFilter.filters[i].value + `')`;
                    }
                    
                }
            }

            return url;
        }
    },
    sort: ({ field: 'senderName' }),
    serverFiltering: true,
    serverSorting: true,
    schema: {
        model: {
            id: "id",
            fields: {
                id: { editable: false, nullable: false, type: "guid" },
                description: { validation: { required: true } },
                senderName: { validation: { required: true } },
                recipientName: { validation: { required: true } },
                weight: {
                    type: "number", validation: { min: 0, required: true }
                }
            }
        },
    },
    batch: false,
    pageSize: 10
});

$(document).ready(function () {
    $("#grid").kendoGrid({
        dataSource: dataSourceq,
        height: 550,
        sortable: true,
        pageable: true,
        filterable: {
            extra: false
        },
        columns: [
            {
                field: "description",
                title: "Description"
            },
            {
                field: "senderName",
                title: "Sender Name"
            },
            {
                field: "recipientName",
                title: "Recipient Name"
            },
            {
                field: "weight",
                title: "Weight",
                width: "120px"
            },
            {
                field: "carrierName",
                title: "Carrier Name"
            },
            {
                command: [
                    {
                        name: "edit", click: function (e) {
                            e.preventDefault();
                            let tr = $(e.target).closest("tr");
                            let data = this.dataItem(tr);
                            window.location.href = "https://localhost:44399/Shipment/Create/" + data.id;
                        }
                    },
                    {
                        name: "details", click: function (e) {
                            e.preventDefault();
                            let tr = $(e.target).closest("tr");
                            let data = this.dataItem(tr);
                            window.location.href = "https://localhost:44399/Shipment/Retrieve/" + data.id;
                        }
                    }],
                width: "170px"
            }
        ]
    });
});