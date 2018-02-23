$(document).ready(function () {
    Render_PIParty = new PI_Party();
    Render_PIParty.PartyUsers();
});

function PI_Party() {

    //This is the Main View Jquery function
    this.PartyUsers = function () {
        
        var url = "../GetPartyUsers";
        //prepares the data of the List of party Users
        var sourceOfPartyUsers =
        {
            datatype: "json",
            datafields: [
                { name: 'FirstName', type: 'string' },
                { name: 'LastName', type: 'string' },
                { name: 'Email', type: 'string' },
                { name: 'AccountNumber', type: 'string' },
                { name: 'LoginCreatedDate', type: "date", format: 'MM/dd/yyyy' },
                { name: 'IsAuthenticated', type: 'bool' },
                { name: 'PartyId', type: 'string' },

            ],
            id: 'PartyId',
            url: url,
            updaterow: function (rowID, newdata, commit) {
                commit(true);
            }
        };

        var dataAdapter = new $.jqx.dataAdapter(sourceOfPartyUsers);
        var editrow = -1;
        // initializing jqxGrid
        $("#partyUserList").jqxGrid(
        {
            width: '100%',
            source: dataAdapter,
            height: '500',
            columnsresize: true,
            filterable: true,
            showfilterrow: true,
            autoshowfiltericon: false,
            pageable: true,
            pagesize: 20,
            sortable: true,
            editable: true,
            enabletooltips: true,
            altrows: true,
            rowsheight: 30,
            columns: [
             { text: 'First Name', datafield: 'FirstName', width: '15%', editable: false },
             { text: 'Last Name', datafield: 'LastName', width: '15%', editable: false },
             { text: 'Email', datafield: 'Email', width: '20%', editable: false },
             {
                 text: 'Login Created Date', datafield: 'LoginCreatedDate', width: '20%', editable: false, cellsformat: 'D', columntype: 'datetimeinput',
                 filtertype: 'date'
             },
             {
                 text: 'Account Number', datafield: 'AccountNumber', width: '15%', editable: false, cellsrenderer: function (row, datafield, columntype, value) {
                     if (columntype == 0)
                         return "";
                 }
             },
            {
                text: 'Action', datafield: 'Edit', width: '15%', editable: false, cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
                    if (isAuthorisedToMap == 'True') {
                        return '<button class="editAccount btn-map-user btn btn-primary" rowfield=' + row + ' data-toggle="tooltip" title="Map account number to party user">Edit Account</button>';
                    }
                    else {
                        return '<button class="anchor-disabled btn-map-user btn btn-primary" data-toggle="tooltip" title="You are not authorized to edit account">Edit Account</button>';
                    }
                }
            }]
        });

        $('#partyUserList').on('click', '.editAccount', function () {
            editrow = parseInt($(this).attr('rowfield'));
            var dataRecord = $("#partyUserList").jqxGrid('getrowdata', editrow);
            if (dataRecord.IsAuthenticated) {
                $("#firstName").text(dataRecord.FirstName);
                $("#lastName").text(dataRecord.LastName);
                $("#email").text(dataRecord.Email);
                $("#accountNumber").jqxNumberInput({ decimal: 0, decimalDigits: 0, groupSeparator: '', width: '120', min: 0 }).val(dataRecord.AccountNumber);
                createPopupWindow('#popupWindow', 300, 400).init();
                $('#popupWindow').jqxWindow('open');
            }
            else {
                ShowMessage(dataRecord.FirstName + "'s login registration has not been activated");
            }
        })
        // update the edited row when the user clicks the 'Save' button.

        $("#Save").click(function () {
            if (editrow >= 0) {
                var rowID = $('#partyUserList').jqxGrid('getrowid', editrow);
                $.httpGet({
                    url: '../UpdateAccountNumber',
                    data: { partyid: rowID, accountnumber: $("#accountNumber").val() },
                    success: function (data) {
                        if (data.success) {
                            $("#popupWindow").jqxWindow('hide');
                            $("#partyUserList").jqxGrid('setcellvalue', editrow, "AccountNumber", $("#accountNumber").val());
                            editrow = -1;
                            ShowMessage(data.responseText);
                        }
                        else {
                            ShowMessage(data.responseText);
                        }
                    }
                });
            }
        });        
    }
}