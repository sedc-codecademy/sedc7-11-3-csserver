const db_name = 'books';

const loadTables = async () => {
    const response = await fetch(`/sql/${db_name}/tables`);
    return await response.json();
};

const loadSchema = async (tableName) => {
    const response = await fetch(`/sql/${db_name}/tables/${tableName}/schema`);
    return await response.json();
};

const loadData = async (tableName) => {
    const response = await fetch(`/sql/${db_name}/tables/${tableName}`);
    return await response.json();
};

const loadDataFiltered = async (tableName, fieldName, value) => {
    const response = await fetch(`/sql/${db_name}/tables/${tableName}?${fieldName}=${value}`);
    return await response.json();
};
