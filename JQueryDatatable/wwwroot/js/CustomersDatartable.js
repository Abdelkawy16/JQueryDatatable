$(document).ready(() => {
    $('#Customers').dataTable({
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/api/customers",
            "type": "POSt",
            "dataType":"json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "id", "autowidth": true },
            { "data": "firstName", "name": "firstName", "autowidth": true },
            { "data": "lastName", "name": "lastName", "autowidth": true },
            { "data": "contact", "name": "contact", "autowidth": true },
            { "data": "email", "name": "email", "autowidth": true },
            { "data": "dateOfBirth", "name": "dateOfBirth", "autowidth": true },
            {
                "render": (data, type, row) =>
                { return `<a href="#" class="btn btn-danger" onclick=DeleteCustomer(${row.id})>Delete</a>` },
                "orderable": false
            },
        ]
    })
})