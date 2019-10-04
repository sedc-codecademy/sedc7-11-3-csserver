$(() => {
    fillTableSelect();

    $("#tableList").on('change', () => {
        const tableName = $("#tableList").val();
        if (tableName !== "") {
            loadTableData(tableName);
        }
    });
});

const fillTableSelect = async () => {
    const data = await loadTables();
    $("#tableList").empty();
    $("#tableList").append(`<option value="">-- select a table --</option>`);
    for (const tableName of data.tableNames) {
        $("#tableList").append(`<option value=${tableName}>${tableName}</option>`);
    }
};

const loadTableHeader = async (tableName) => {
    console.log("Start loading header");
    $("#head").empty();
    const schema = await loadSchema(tableName);
    const tr = $("<tr>");
    for (const column of schema.columnNames) {
        tr.append(`<th>${column}</th>`);
    }
    tr.appendTo("#head");
    console.log("Finish loading header");
};

const loadTableBody = async (tableName) => {
    console.log("Start loading body");
    $("#body").empty();
    const data = await loadData(tableName);
    for (const item of data) {
        const tr = $("<tr>");
        for (const key in item) {
            const element = item[key];
            tr.append(`<td>${element}</td>`);
        }
        tr.appendTo("#body");
    }
    console.log("Finish loading body");
};

const loadTableData = (tableName) => {
    loadTableHeader(tableName);
    loadTableBody(tableName);
}