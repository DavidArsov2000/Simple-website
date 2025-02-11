$(document).ready(function () {
    const form = $('#personForm');
    const tableBody = $('#personTable tbody');
    const noDataMessage = $('#noDataMessage');
    const personTable = $('#personTable');
    let previusResults = [];

    $('#addLocal').on('click', function () {
        const ime = $('#name').val();
        const starost = $('#age').val();
        const lokalonoDodano = ime + " (vnos je dodan samo lokalno)";
        addLocalPersonToTable({ ime: lokalonoDodano, starost: starost });
        form[0].reset();
        checkTableData();
    });

    form.on('submit', function (event) {
        event.preventDefault();

        const ime = $('#name').val();
        const starost = $('#age').val();

        $.ajax({
            url: 'https://localhost:7108/api/Persons/addPerson',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ ime, starost }),
            success: function (persons) {
                const newPersons = [];
                persons.forEach(function (person) {
                    if (!previusResults.some(entry => entry.id === person.id)) {
                        newPersons.push(person);
                        previusResults.push(person);
                    }
                });
                newPersons.forEach(addPersonToTable);
                newPersons.lenght = 0;
                form[0].reset();
                checkTableData();
            },
            error: function (xhr, status, error) {
                if (xhr.status === 404) {
                    console.error('Error: Not Found');
                } else {
                    console.error('Error adding person:', error);
                }
            }
        });
    });

    /*  metoda uporabljena pri prvem delu naloge
    function loadPersonsFromServer() {
        $.ajax({
            url: 'https://localhost:7108/api/Persons/allPersons',
            method: 'GET',
            success: function (persons) {
                if (persons.length === 0) {
                    console.log('No persons found.');
                } else {
                    persons.forEach(addPersonToTable);
                }
                checkTableData();
            },
            error: function (xhr, status, error) {
                if (xhr.status === 404) {
                    console.error('Error: Not Found');
                } else {
                    console.error('Error loading persons:', error);
                }
            }
        });
    }
    */


    function checkTableData() {
        if (tableBody.children().length === 0) {
            noDataMessage.show();
            personTable.hide();
        } else {
            noDataMessage.hide();
            personTable.show();
        }
    }

    function addPersonToTable(person) {
        const row = `<tr><td>${person.ime}</td><td>${person.starost}</td></tr>`;
        tableBody.append(row);
    }

    function addLocalPersonToTable(person) {
        const row = `<tr style="background-color:yellow"><td>${person.ime}</td><td>${person.starost}</td></tr>`;
        tableBody.append(row);
    }
});